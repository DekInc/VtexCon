using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vital.Oracle.Services;
using Vital.Oracle.Services.Model;
using VtexCon;
using VtexCon.Models;
using VtexCon.ModelsOra;
using VtexCon.ModelsOra.Model;

namespace Vital.VtexMainSync {
    public partial class MainSync : ServiceBase {
        TcpListener Listener;
        Thread SyncProductsThr { set; get; }
        DateTime LastTimeProductsSynced { set; get; }
        public bool Continue { get; set; } = true;
        static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MainSync() {
            InitializeComponent();
            LastTimeProductsSynced = DateTime.Now.AddDays(-2);
        }
        public void OnDebug() {
            OnStart(null);
        }
        protected override void OnStart(string[] args) {            
            BuildSyncProductsThr();
            SyncProductsThr.Start();
            TcpServerStart();
            Log.Info(ServiceName + " started");
        }
        string GetVersion() {
            Assembly ThisAssembly = Assembly.GetExecutingAssembly();
            FileVersionInfo Fvi = FileVersionInfo.GetVersionInfo(ThisAssembly.Location);
            return Fvi.FileVersion;
        }
        #region ServiceControl
        private void TcpServerStart() {
            try {
                IPAddress LocalIp = IPAddress.Parse("127.0.0.1");
                Listener = new TcpListener(LocalIp, 50007);
                Listener.Start();
                Thread WhileThread = new Thread(() => {
                    while (Continue) {
                        TcpClient Client = Listener.AcceptTcpClient();
                        Thread ChildThread = new Thread(() => {
                            NetworkStream NetStream = Client.GetStream();
                            StreamReader NetStreamreader = new StreamReader(Client.GetStream(), Encoding.ASCII);
                            string Line = null;
                            while ((Line = NetStreamreader.ReadLine()) != "<EOF>") {
                                Log.Info(Line);
                                if (string.IsNullOrEmpty(Line) || Line.Equals("quit", StringComparison.InvariantCultureIgnoreCase))
                                    break;
                                if (Line.Equals("S", StringComparison.InvariantCultureIgnoreCase)) {
                                    StreamWriter NetStreamWriter = new StreamWriter(Client.GetStream(), Encoding.ASCII);
                                    NetStreamWriter.Write($"{ServiceName}=OK");
                                    NetStreamWriter.Flush();
                                }
                                if (Line.Equals("Version", StringComparison.InvariantCultureIgnoreCase)) {
                                    StreamWriter NetStreamWriter = new StreamWriter(Client.GetStream(), Encoding.ASCII);
                                    NetStreamWriter.WriteLine($"{ServiceName}={GetVersion()}");
                                    NetStreamWriter.Flush();
                                }
                                if (Line.Equals("Shutdown", StringComparison.InvariantCultureIgnoreCase)) {
                                    Continue = false;
                                    NetStream.Close();
                                    Client.Close();
                                    Environment.Exit(0);
                                }
                                if (Line.Equals("GetLog", StringComparison.InvariantCultureIgnoreCase)) {
                                    string LogFile = ((log4net.Appender.FileAppender)(Log.Logger.Repository.GetAppenders())[0]).File;
                                    if (!string.IsNullOrEmpty(LogFile)) {
                                        if (File.Exists(LogFile)) {
                                            StreamWriter NetStreamWriter = new StreamWriter(Client.GetStream(), Encoding.ASCII);
                                            using (FileStream FileStream = new FileStream(LogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                            using (StreamReader TextReader = new StreamReader(FileStream)) {
                                                NetStreamWriter.WriteLine(TextReader.ReadToEnd());
                                            }
                                            NetStreamWriter.Flush();
                                        }
                                    }
                                }
                            }
                            NetStream.Close();
                            Client.Close();
                        });
                        ChildThread.Start();
                    }
                });
                WhileThread.Start();
                WhileThread.Join();
            } catch (Exception E1) {
                Log.Error(E1.ToString() + E1.StackTrace);
            }
        } 
        #endregion
        private void BuildSyncProductsThr() {
            SyncProductsThr = new Thread(() => {
                Common.LogInfo("VtexMainSync SyncProductsThr start");
                while (Continue) {
                    try {
                        if ((DateTime.Now - LastTimeProductsSynced).TotalDays > 0
                        && LastTimeProductsSynced.Day != DateTime.Now.Day) {
                            Common.LogInfo("VtexMainSync SyncProductsThr Comienza sincronización");                            
                            ApiOracle ApiOracleO = new ApiOracle();
                            List<GetTokens> ListMainTokens = ApiOracleO.GetTokens(true, true);
                            if (ListMainTokens == null) {
                                Common.LogError("VtexMainSync SyncProductsThr Error al obtener los tokens de conexión");
                                continue;
                            }
                            if (ListMainTokens.Count == 0) {
                                Common.LogError("VtexMainSync SyncProductsThr Error al obtener los tokens de conexión");
                                continue;
                            }
                            ApiVtex ApiVtexO = new ApiVtex(0, "arvitalqa", ListMainTokens.FirstOrDefault().USERAPI, ListMainTokens.FirstOrDefault().TOKEN);
                            //Obtener de Oracle productos, sku's y propiedades
                            List<VtexProduct> ListProductOracle = ApiOracleO.GetProducts().ToList();
                            List<VtexNewSku> ListNewSkusOracle = ApiOracleO.GetSkus().ToList();
                            Common.LogInfo($"VtexMainSync SyncProductsThr. Productos obtenidos de Oracle: {ListProductOracle.Count}");
                            List<Product> ListCrudProducts = new List<Product>();
                            if (ListProductOracle.Count > 0) {
                                LastTimeProductsSynced = DateTime.Now;
                                Common.LogInfo($"VtexMainSync SyncProductsThr. Chequeando {ListProductOracle.Count} de vtex");
                                foreach (VtexProduct ProductFromOra in ListProductOracle) {
                                    //Buscar en Vtex si el producto existe
                                    Product ProductFromVtex = ApiVtexO.GetProductByRefId(ProductFromOra.RefId);
                                    if (!string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                                        Common.LogError($"VtexMainSync SyncProductsThr. Error en método ApiVtex.GetProductByRefId para Product.RefId = {ProductFromOra.RefId}. Detalle: {ApiVtexO.LastHttpError}");
                                    else {
                                        if (ProductFromVtex == null) {
                                            Product ToInsertProduct = Common.CreateProductFromOraInfo(ProductFromOra);
                                            if (ToInsertProduct.DepartmentId > 0)
                                                ListCrudProducts.Add(Common.CreateProductFromOraInfo(ProductFromOra));
                                        } else {
                                            Product ToUpdateProduct = Common.UpdateProductFromOraInfo(ProductFromVtex, ProductFromOra);
                                            if (ToUpdateProduct.Id.HasValue)
                                                ListCrudProducts.Add(ToUpdateProduct);
                                        }
                                    }
                                }
                            }
                            Common.LogInfo($"VtexMainSync SyncProductsThr. Fin chequeo, procedo a CRUD en vtex");
                            //Procesar productos, sku's y propiedades
                            List<CategorySpecification> ListCatProps = ApiVtexO.GetSpecificationsTreeByCategoryId(0);
                            if (!string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                                Common.LogInfo($"VtexMainSync SyncProductsThr. Error en GetSpecificationsTreeByCategoryId, no se pueden obtener las propiedades de productos especificadas en categorias.");
                            string StatusProduct = "";
                            foreach (Product CrudProduct in ListCrudProducts) {
                                StatusProduct = "";
                                Product ResProduct = new Product();
                                if (CrudProduct.Update) {
                                    //if (CrudProduct.Id.Value != 4332) continue;
                                    //Actualizar
                                    ResProduct = ApiVtexO.UpdateProduct(CrudProduct, CrudProduct.Id.Value);
                                    if (string.IsNullOrEmpty(ApiVtexO.LastHttpError)) {                                        
                                        StatusProduct = $"VtexMainSync SyncProductsThr. Producto actualizado. Product.Id = {ResProduct.Id}. Product.RefId = {CrudProduct.RefId}";
                                        Common.LogInfo(StatusProduct);
                                    } else {
                                        StatusProduct = $"VtexMainSync SyncProductsThr. Error en método ApiVtex.UpdateProduct para Product.RefId = {CrudProduct.RefId}. Detalle: {ApiVtexO.LastHttpError}";
                                        Common.LogError(StatusProduct);
                                    }
                                    //Obtener sku's existentes
                                    List<GetSkuByProductId> ListSkusFromVtex = ApiVtexO.GetSkusByProductId(CrudProduct.Id.Value);
                                    if (string.IsNullOrEmpty(ApiVtexO.LastHttpError)) {
                                        Common.LogInfo($"VtexMainSync SyncProductsThr. Skus obtenidos. Product.RefId = {CrudProduct.RefId}. Cant.: {ListSkusFromVtex.Count}");
                                        //Procesar sku's de Oracle
                                        List<VtexNewSku> ListNewUpdSkusOracle = ListNewSkusOracle.Where(OSku => OSku.RefId == CrudProduct.RefId).ToList();
                                        if (ListNewUpdSkusOracle.Count > 0) {
                                            foreach (VtexNewSku NewUpdSku in ListNewUpdSkusOracle) {
                                                //Se usa RefId porque solo habra 1 product = 1 sku, si lo cambian tiene que ser .Id
                                                if (ListSkusFromVtex.Where(OSv => OSv.RefId.Equals(NewUpdSku.RefId.Trim(), StringComparison.InvariantCultureIgnoreCase)).Count() > 0) {
                                                    NewSku SearchedSku = ApiVtexO.GetSku(ListSkusFromVtex.Where(OSv => OSv.RefId.Equals(NewUpdSku.RefId.Trim(), StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault().Id.Value);
                                                    if (string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                                                        UpdateSku(CrudProduct, SearchedSku, NewUpdSku, ref ApiVtexO, ref ApiOracleO);
                                                    else
                                                        Common.LogError($"VtexMainSync SyncProductsThr. Error en método ApiVtex.GetSku para Product.RefId = {CrudProduct.RefId}, no se puede actualizar el sku porque no se puede obtener el sku existente de Vtex. Detalle: {ApiVtexO.LastHttpError}");                                                    
                                                } else {
                                                    CreateSku(null, CrudProduct, NewUpdSku, ref ApiVtexO, ref ApiOracleO);
                                                }
                                            }
                                        }
                                    } else {
                                        if (ApiVtexO.LastHttpError.Contains("SKU not found")) {
                                            CreateSku(ListNewSkusOracle, CrudProduct, null, ref ApiVtexO, ref ApiOracleO);
                                        } else {
                                            Common.LogError($"VtexMainSync SyncProductsThr. Error en método ApiVtex.GetSkusByProductId para Product.RefId = {CrudProduct.RefId}. No se pudieron actulizar los sku. Detalle: {ApiVtexO.LastHttpError}");
                                        }
                                    }
                                } else {
                                    //Insertar
                                    ResProduct = ApiVtexO.CreateProduct(CrudProduct);
                                    if (string.IsNullOrEmpty(ApiVtexO.LastHttpError)) {
                                        StatusProduct = $"VtexMainSync SyncProductsThr. Producto creado. Product.Id = {ResProduct.Id}. Product.RefId = {CrudProduct.RefId}";
                                        Common.LogInfo(StatusProduct);
                                        CrudProduct.Id = ResProduct.Id;
                                        CreateSku(ListNewSkusOracle, CrudProduct, null, ref ApiVtexO, ref ApiOracleO);
                                    } else {
                                        StatusProduct = $"VtexMainSync SyncProductsThr. Error en método ApiVtex.CreateProduct para Product.RefId = {CrudProduct.RefId}. Detalle: {ApiVtexO.LastHttpError}";
                                        Common.LogError(StatusProduct);
                                    }
                                }
                                if (ResProduct == null) {
                                    ResProduct = ApiVtexO.GetProductByRefId(CrudProduct.RefId);
                                }
                                if (ResProduct != null) {
                                    if (ResProduct.Id.HasValue) {
                                        if (ResProduct.Id.Value > 0 && ListCatProps.Count > 0) {
                                            VtexProduct CurrOraProduct = ListProductOracle.Where(Po => Po.RefId == ResProduct.RefId).FirstOrDefault();
                                            //Obtengo def de propiedad
                                            //Crear UxB
                                            CategorySpecification PropUxb = ListCatProps.Where(Cp => Cp.Name.Equals("uxb", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                                            if (PropUxb == null) {
                                                StatusProduct = $"VtexMainSync SyncProductsThr. Errror al obtener la propiedad UxB del sku de oracle. Cant. Lista de propiedades: {ListCatProps.Count}";
                                                Common.LogInfo(StatusProduct);
                                            } else {
                                                //Obtengo si el producto ya tiene propiedad
                                                List<GetProductSpecification> ListOldProps = ApiVtexO.GetProductSpecification(ResProduct.Id.Value);
                                                if (!string.IsNullOrEmpty(ApiVtexO.LastHttpError)) {
                                                    StatusProduct = $"VtexMainSync SyncProductsThr. Errror al obtener la lista de propiedades existentes del producto. ProdId: {ResProduct.Id}";
                                                    Common.LogInfo(StatusProduct);
                                                }
                                                if (ListOldProps.Count != 0) {
                                                    ApiVtexO.DeleteProductSpecification(ResProduct.Id.Value);
                                                    if (!string.IsNullOrEmpty(ApiVtexO.LastHttpError)) {
                                                        StatusProduct = $"VtexMainSync SyncProductsThr. Errror al borrar la lista de propiedades existentes del producto. ProdId: {ResProduct.Id}";
                                                        Common.LogInfo(StatusProduct);
                                                    }
                                                }
                                                CreateProp(PropUxb, ResProduct, CurrOraProduct.uxb.ToIntS(), ref ApiVtexO, ref StatusProduct);
                                            }
                                            //Crear Factor
                                            CategorySpecification PropFactor = ListCatProps.Where(Cp => Cp.Name.Equals("factor", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                                            if (PropFactor == null) {
                                                StatusProduct = $"VtexMainSync SyncProductsThr. Errror al obtener la propiedad Factor del sku de oracle. Cant. Lista de propiedades: {ListCatProps.Count}";
                                                Common.LogInfo(StatusProduct);
                                            } else {
                                                CreateProp(PropFactor, ResProduct, CurrOraProduct.factor.ToIntS(), ref ApiVtexO, ref StatusProduct);
                                            }
                                        }
                                        //Guardo el estado de la creación del producto
                                        SetProduct RefIdOra = new SetProduct();
                                        RefIdOra.RefId.V = ResProduct.RefId;
                                        RefIdOra.Status.V = StatusProduct.Contains("Error") ? "2" : "1";
                                        RefIdOra.ProductId.V = ResProduct.Id.Value.ToString();
                                        RefIdOra.Detail.V = StatusProduct; //StatusProduct.Contains("Error") ? StatusProduct.Replace("'", "") : "";
                                        SpRet SpRetO = ApiOracleO.SetProduct(RefIdOra);
                                        if (SpRetO != null) {
                                            if (SpRetO.Ok != 0) {
                                                Common.LogInfo($"VtexMainSync SyncProductsThr. SetProduct ejecutado correctamente. Product.RefId = {CrudProduct.RefId}");
                                            } else {                                                
                                                Common.LogError($"VtexMainSync SyncProductsThr. Error al ejecutar SetProduct. Product.RefId = {CrudProduct.RefId}. Det: {SpRetO.Error}");
                                            }
                                        }
                                    }
                                }
                            }
                            Common.LogInfo($"VtexMainSync SyncProductsThr going sleep");
                        }                        
                        Thread.Sleep(23000);
                    } catch (Exception E1) {
                        Common.LogError(E1.ToString() + E1.StackTrace);
                    }
                }
                Common.LogInfo("VtexMainSync SyncProductsThr Ended");
            });
        }
        private void CreateProp(CategorySpecification Prop, Product ResProduct, string PropVal, ref ApiVtex ApiVtexO, ref string StatusProduct) {
            NewProductSpecification NewProp = new NewProductSpecification() {
                FieldId = Prop.FieldId.ToInt(),
                Text = PropVal
            };
            NewProp = ApiVtexO.CreateProductSpecification(ResProduct.Id.Value, NewProp);
            if (string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                Common.LogInfo($"VtexMainSync SyncProductsThr. Se creo la propiedad {Prop.Name} de ProductId = {ResProduct.Id}. Det: {PropVal}");
            else {
                StatusProduct = $"VtexMainSync SyncProductsThr. Errror al crear propiedad {Prop.Name} de ProductId = {ResProduct.Id} Det:{ApiVtexO.LastHttpError}";
                Common.LogError(StatusProduct);
            }
        }
        private void CreateSkuFile(NewSku CrudSku, VtexNewSku SkuFromOra, string RefId, ref ApiVtex ApiVtexO, ref string StatusSku, ref string StateCodeSku) {
            NewSkuFile NewSkuFileO = new NewSkuFile() {
                IsMain = true,
                Label = CrudSku.Name,
                Text = CrudSku.Name,
                Url = SkuFromOra.ImageUrl
            };
            NewSkuFileO = ApiVtexO.CreateSkuFile(CrudSku.Id.Value, NewSkuFileO);
            if (string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                Common.LogInfo($"VtexMainSync SyncProductsThr. SkuFile creado. Product.RefId = {RefId}, Sku.Id = {SkuFromOra.Id}");
            else {
                StateCodeSku = "2";
                StatusSku += $"VtexMainSync SyncProductsThr. Error en método ApiVtex.CreateSkuFile para Product.RefId = {RefId}, Sku.Id = {SkuFromOra.Id}. Detalle: {ApiVtexO.LastHttpError}";
                Common.LogError($"VtexMainSync SyncProductsThr. Error en método ApiVtex.CreateSkuFile para Product.RefId = {RefId}, Sku.Id = {SkuFromOra.Id}. Detalle: {ApiVtexO.LastHttpError}");
            }
        }
        private void UpdateSku(Product CrudProduct, NewSku OldSku, VtexNewSku SkuFromOra, ref ApiVtex ApiVtexO, ref ApiOracle ApiOracleO) {
            string StatusSku = "", StateCodeSku = "1";
            NewSku CrudSku = Common.UpdateSkuFromOraInfo(OldSku, SkuFromOra);
            CrudSku = ApiVtexO.UpdateSku(CrudSku);
            if (string.IsNullOrEmpty(ApiVtexO.LastHttpError)) {
                Common.LogInfo($"VtexMainSync SyncProductsThr. Sku Actualizado. Product.RefId = {CrudProduct.RefId}, Sku.Id = {SkuFromOra.Id}");
                //Actualizar Ean
                string ActualEan = ApiVtexO.GetEanBySku(CrudSku.Id.Value);
                if (!string.IsNullOrEmpty(ActualEan)) {
                    ApiVtexO.DeleteSkuEan(CrudSku.Id.Value);
                    if (string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                        Common.LogInfo($"VtexMainSync SyncProductsThr. SkuEan borrado. Product.RefId = {CrudProduct.RefId}, Sku.Id = {SkuFromOra.Id}");
                    else {
                        StateCodeSku = "3";
                        StatusSku += $"VtexMainSync SyncProductsThr. Error en método ApiVtex.DeleteSkuEan para Product.RefId = {CrudProduct.RefId}, Sku.Id = {SkuFromOra.Id}. Detalle: {ApiVtexO.LastHttpError}";
                        Common.LogError($"VtexMainSync SyncProductsThr. Error en método ApiVtex.DeleteSkuEan para Product.RefId = {CrudProduct.RefId}, Sku.Id = {SkuFromOra.Id}. Detalle: {ApiVtexO.LastHttpError}");
                    }
                }
                ApiVtexO.CreateSkuEan(CrudSku.Id.Value, SkuFromOra.Ean);
                if (string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                    Common.LogInfo($"VtexMainSync SyncProductsThr. SkuEan creado. Product.RefId = {CrudProduct.RefId}, Sku.Id = {SkuFromOra.Id}");
                else {
                    StateCodeSku = "3";
                    StatusSku += $"VtexMainSync SyncProductsThr. Error en método ApiVtex.CreateSkuEan para Product.RefId = {CrudProduct.RefId}, Sku.Id = {SkuFromOra.Id}. Detalle: {ApiVtexO.LastHttpError}";
                    Common.LogError($"VtexMainSync SyncProductsThr. Error en método ApiVtex.CreateSkuEan para Product.RefId = {CrudProduct.RefId}, Sku.Id = {SkuFromOra.Id}. Detalle: {ApiVtexO.LastHttpError}");
                }
                //Actualizar image files
                List<GetSkuFile> ListImages = ApiVtexO.GetSkuFile(CrudSku.Id.Value);
                if (string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                    Common.LogInfo($"VtexMainSync SyncProductsThr. SkuFile obtenido. Product.RefId = {CrudProduct.RefId}, Sku.Id = {SkuFromOra.Id}");
                else if (!ApiVtexO.LastHttpError.Contains("sku archives not found")) {
                    StatusSku += $"VtexMainSync SyncProductsThr. Error en método ApiVtex.GetSkuFile para Product.RefId = {CrudProduct.RefId}, Sku.Id = {SkuFromOra.Id}. Detalle: {ApiVtexO.LastHttpError}";
                    Common.LogError($"VtexMainSync SyncProductsThr. Error en método ApiVtex.GetSkuFile para Product.RefId = {CrudProduct.RefId}, Sku.Id = {SkuFromOra.Id}. Detalle: {ApiVtexO.LastHttpError}");
                }
                if (ListImages == null && ApiVtexO.LastHttpError.Contains("sku archives not found"))
                    CreateSkuFile(CrudSku, SkuFromOra, CrudProduct.RefId, ref ApiVtexO, ref StatusSku, ref StateCodeSku);
                else if (ListImages.Count == 0) {
                    CreateSkuFile(CrudSku, SkuFromOra, CrudProduct.RefId, ref ApiVtexO, ref StatusSku, ref StateCodeSku);
                }
            } else {
                StatusSku = $"VtexMainSync SyncProductsThr. Error en método ApiVtex.UpdateSku para Product.RefId = {CrudProduct.RefId}, Sku.Id = {SkuFromOra.Id}. Detalle: {ApiVtexO.LastHttpError}";
                Common.LogError(StatusSku);
            }
            //Guardo el estado de la creación del sku
            SetProduct RefIdOra = new SetProduct();
            RefIdOra.RefId.V = OldSku.RefId;
            RefIdOra.Status.V = StateCodeSku;
            RefIdOra.ProductId.V = OldSku.Id.Value.ToString();
            RefIdOra.Detail.V = StatusSku;
            SpRet SpRetO = ApiOracleO.SetSku(RefIdOra);
            if (SpRetO != null) {
                if (SpRetO.Ok != 0) {
                    Common.LogInfo($"VtexMainSync SyncProductsThr. SetSku ejecutado correctamente. Product.RefId = {CrudProduct.RefId}");
                } else {
                    Common.LogError($"VtexMainSync SyncProductsThr. Error al ejecutar SetSku. Product.RefId = {CrudProduct.RefId}. Det: {SpRetO.Error}");
                }
            }
        }        
        private void CreateSku(List<VtexNewSku> ListNewSkusOracle, Product CrudProduct, VtexNewSku SkuFromOra, ref ApiVtex ApiVtexO, ref ApiOracle ApiOracleO) {
            string StatusSku = "", StateCodeSku = "1";
            VtexNewSku NewSkuFromOra = new VtexNewSku();
            if (SkuFromOra == null)
                NewSkuFromOra = ListNewSkusOracle.Where(Os => Os.RefId == CrudProduct.RefId).FirstOrDefault();
            else
                NewSkuFromOra = SkuFromOra;
            NewSkuFromOra.ProductId = CrudProduct.Id.Value.ToDecimal();
            NewSku CrudSku = Common.CreateSkuFromOraInfo(NewSkuFromOra);
            CrudSku = ApiVtexO.CreateSku(CrudSku);
            if (string.IsNullOrEmpty(ApiVtexO.LastHttpError) && CrudSku != null) {
                Common.LogInfo($"VtexMainSync SyncProductsThr. Sku Creado. Product.RefId = {CrudProduct.RefId}");
                //Crear Ean
                ApiVtexO.CreateSkuEan(CrudSku.Id.Value, NewSkuFromOra.Ean);
                if (string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                    Common.LogInfo($"VtexMainSync SyncProductsThr. SkuEan creado. Product.RefId = {CrudProduct.RefId}, Sku.Id = {NewSkuFromOra.Id}");
                else {
                    StateCodeSku = "3";
                    StatusSku += $"VtexMainSync SyncProductsThr. Error en método ApiVtex.CreateSkuEan para Product.RefId = {CrudProduct.RefId}, Sku.Id = {NewSkuFromOra.Id}. Detalle: {ApiVtexO.LastHttpError}";
                    Common.LogError(StatusSku);
                }
                //Subir imagen
                CreateSkuFile(CrudSku, NewSkuFromOra, CrudProduct.RefId, ref ApiVtexO, ref StatusSku, ref StateCodeSku);
            } else {
                StateCodeSku = "4";
                StatusSku = $"VtexMainSync SyncProductsThr. Error en método ApiVtex.CreateSku para Product.RefId = {CrudProduct.RefId}. Detalle: {ApiVtexO.LastHttpError}";
                Common.LogError(StatusSku);
            }
            //Guardo el estado de la creación del sku
            SetProduct RefIdOra = new SetProduct();
            RefIdOra.RefId.V = NewSkuFromOra.Id.ToString();
            RefIdOra.Status.V = StateCodeSku;
            RefIdOra.ProductId.V = NewSkuFromOra.Id.ToIntS();
            RefIdOra.Detail.V = StatusSku.Replace("'", "");
            SpRet SpRetO = ApiOracleO.SetSku(RefIdOra);
            if (SpRetO != null) {
                if (SpRetO.Ok != 0) {
                    Common.LogInfo($"VtexMainSync SyncProductsThr. SetSku ejecutado correctamente. Product.RefId = {CrudProduct.RefId}");
                } else {
                    Common.LogError($"VtexMainSync SyncProductsThr. Error al ejecutar SetSku. Product.RefId = {CrudProduct.RefId}. Det: {SpRetO.Error}");
                }
            }
            //Trato de activar SKU
            if (StateCodeSku == "1") {
                CrudSku.IsActive = true;
                CrudSku = ApiVtexO.UpdateSku(CrudSku);
                if (!string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                    Common.LogError($"VtexMainSync SyncProductsThr. Error al ejecutar UpdateSku para activar sku. Product.RefId = {CrudProduct.RefId}. Det: {ApiVtexO.LastHttpError}");
            }
        }

        protected override void OnStop() {
            Log.Info("SyncProductsThr end proc");
            if (SyncProductsThr.ThreadState == System.Threading.ThreadState.Running)
                SyncProductsThr.Join();
            else
                SyncProductsThr.Abort();
            Log.Info("SyncProductsThr ended");
        }
    }
}
