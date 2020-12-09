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
using VtexCon.ModelsOra.Model;

namespace Vital.VtexBranchSync {
    public partial class BranchsSync : ServiceBase {
        TcpListener Listener;
        Thread SyncBranchsThr { set; get; }
        DateTime LastTimeBranchsSynced { set; get; }
        public bool Continue { get; set; } = true;
        static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public BranchsSync() {
            InitializeComponent();
            LastTimeBranchsSynced = DateTime.Now.AddDays(-2);
        }
        public void OnDebug() {
            OnStart(null);
        }
        protected override void OnStart(string[] args) {
            BuildSyncBranchsThr();
            SyncBranchsThr.Start();
            TcpServerStart();
            Common.LogInfo(ServiceName + " started");
        }
        protected override void OnStop() {
            Log.Info("SyncBranchsThr end proc");
            if (SyncBranchsThr.ThreadState == System.Threading.ThreadState.Running)
                SyncBranchsThr.Join();
            else
                SyncBranchsThr.Abort();
            Log.Info("SyncBranchsThr ended");
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
                Listener = new TcpListener(LocalIp, 50008);
                Listener.Start();
                Thread WhileThread = new Thread(() => {
                    while (Continue) {
                        TcpClient Client = Listener.AcceptTcpClient();
                        Thread ChildThread = new Thread(() => {
                            NetworkStream NetStream = Client.GetStream();
                            StreamReader NetStreamreader = new StreamReader(Client.GetStream(), Encoding.ASCII);
                            string Line = null;
                            while ((Line = NetStreamreader.ReadLine()) != "<EOF>") {
                                Common.LogInfo(Line);
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
        private void BuildSyncBranchsThr() {
            SyncBranchsThr = new Thread(() => {
                Common.LogInfo("VtexBranchsSync SyncBranchsThr start");
                while (Continue) {
                    try {
                        if ((DateTime.Now - LastTimeBranchsSynced).TotalDays > 0
                        && LastTimeBranchsSynced.Day != DateTime.Now.Day) {
                            Common.LogInfo("VtexBranchsSync SyncBranchsThr Comienza sincronización");
                            LastTimeBranchsSynced = DateTime.Now;
                            ApiOracle ApiOracleO = new ApiOracle();
                            List<GetTokens> ListMainTokens = ApiOracleO.GetTokens(false, true);
                            if (ListMainTokens == null) {
                                Common.LogError("VtexBranchsSync SyncBranchsThr Error al obtener los tokens de conexión");
                                continue;
                            }
                            if (ListMainTokens.Count == 0) {
                                Common.LogError("VtexBranchsSync SyncBranchsThr Error al obtener los tokens de conexión");
                                continue;
                            }
                            foreach (GetTokens BranchToken in ListMainTokens) {
                                ApiVtex ApiVtexO = new ApiVtex(0, BranchToken.Politica_Comercial, BranchToken.USERAPI, BranchToken.TOKEN);
                                ApiVtex ApiVtexO2 = new ApiVtex(1, BranchToken.Politica_Comercial, BranchToken.USERAPI, BranchToken.TOKEN);
                                //Actualizar stock para cada sucursal
                                List<VtexStock> ListStock = ApiOracleO.GetStock(BranchToken.CdSucursal).ToList();
                                if (!string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                                    Common.LogError($"VtexBranchsSync SyncBranchsThr. Error en método ApiVtex.GetStock para CdSucursal = {BranchToken.CdSucursal}. Detalle: {ApiVtexO.LastHttpError}");
                                else {                                    
                                    List<GetWarehouses> ListWarehouses = ApiVtexO.GetWarehouses();
                                    if (!string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                                        Common.LogError($"VtexBranchsSync SyncBranchsThr. Error al listar en método ApiVtex.GetWarehouses para CdSucursal = {BranchToken.CdSucursal}. Detalle: {ApiVtexO.LastHttpError}");
                                    else if(1 != 1) {
                                        if (ListWarehouses == null)
                                            Common.LogError($"VtexBranchsSync SyncBranchsThr. Error no hay almacenes en método ApiVtex.GetWarehouses para CdSucursal = {BranchToken.CdSucursal}. Detalle: {ApiVtexO.LastHttpError}");
                                        else {
                                            if (ListWarehouses.Count == 0)
                                                Common.LogError($"VtexBranchsSync SyncBranchsThr. Error no hay almacenes2 en método ApiVtex.GetWarehouses para CdSucursal = {BranchToken.CdSucursal}. Detalle: {ApiVtexO.LastHttpError}");
                                            else {
                                                foreach (VtexStock ArtStock in ListStock) {
                                                    int? SearchedSkuId = ApiVtexO.GetSkuIdByRefId(ArtStock.Sku_RefId.Trim());
                                                    if (!string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                                                        Common.LogError($"VtexBranchsSync SyncBranchsThr. Error en método ApiVtex.GetSkuByRefId para CdSucursal = {BranchToken.CdSucursal} y SkuId = {ArtStock.Sku_RefId}. Detalle: {ApiVtexO.LastHttpError}");
                                                    else {
                                                        if (SearchedSkuId == null)
                                                            Common.LogError($"VtexBranchsSync SyncBranchsThr. Error no existe sku en método ApiVtex.GetSkuByRefId para CdSucursal = {BranchToken.CdSucursal} y SkuId = {ArtStock.Sku_RefId}. Detalle: {ApiVtexO.LastHttpError}");
                                                        else {
                                                            if (!SearchedSkuId.HasValue)
                                                                Common.LogError($"VtexBranchsSync SyncBranchsThr. Error sku con información inválida en método ApiVtex.GetSkuByRefId para CdSucursal = {BranchToken.CdSucursal} y SkuId = {ArtStock.Sku_RefId}. Detalle: {ApiVtexO.LastHttpError}");
                                                            else {
                                                                UpdateInventoryBySkuIdWarehouse NewStock = new UpdateInventoryBySkuIdWarehouse() {
                                                                    UnlimitedQuantity = false,
                                                                    DateUtcOnBalanceSystem = null,
                                                                    Quantity = ArtStock.QtStock.ToInt(),
                                                                    TimeToRefill = null
                                                                };
                                                                bool Done = ApiVtexO.UpdateInventoryBySkuIdWarehouse(SearchedSkuId.Value, ListWarehouses.FirstOrDefault().Id, NewStock);
                                                                if (!string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                                                                    Common.LogError($"VtexBranchsSync SyncBranchsThr. Error en método ApiVtex.UpdateInventoryBySkuIdWarehouse para CdSucursal = {BranchToken.CdSucursal},  SkuId = {ArtStock.Sku_RefId} y Stock = {ArtStock.QtStock}. Detalle: {ApiVtexO.LastHttpError}");
                                                                else if (Done)
                                                                    Common.LogInfo($"VtexBranchsSync SyncBranchsThr. Se actualizo stock, método ApiVtex.UpdateInventoryBySkuIdWarehouse para CdSucursal = {BranchToken.CdSucursal},  SkuId = {ArtStock.Sku_RefId} y Stock = {ArtStock.QtStock}");
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //Actualizar precios
                                foreach (VtexStock ArtStock in ListStock) {
                                    int? SearchedSkuId = ApiVtexO.GetSkuIdByRefId(ArtStock.Sku_RefId.Trim());
                                    if (!string.IsNullOrEmpty(ApiVtexO.LastHttpError))
                                        Common.LogError($"VtexBranchsSync SyncBranchsThr. Error en método ApiVtex.GetSkuIdByRefId para CdSucursal = {BranchToken.CdSucursal},  SkuId = {ArtStock.Sku_RefId}. Detalle: {ApiVtexO.LastHttpError}");
                                    else {
                                        GetPrice SkuPrice = ApiVtexO2.GetPriceBySkuId(BranchToken.Politica_Comercial, SearchedSkuId.Value);
                                        if (!string.IsNullOrEmpty(ApiVtexO2.LastHttpError) && ApiVtexO2.LastHttpError != "The resource you are looking for cannot be found: Price not found")
                                            Common.LogError($"VtexBranchsSync SyncBranchsThr. Error en método ApiVtex.GetPriceBySkuId para CdSucursal = {BranchToken.CdSucursal},  SkuId = {ArtStock.Sku_RefId}. Detalle: {ApiVtexO.LastHttpError}");
                                        else if (ApiVtexO2.LastHttpError.Contains("The resource you are looking for cannot be found: Price not found")) {
                                            //Insertar precios
                                        } else {
                                            //Actualizar precios
                                        }
                                    }
                                }
                            }                            
                            Common.LogInfo($"VtexBranchsSync SyncBranchsThr going sleep");
                        }
                        Thread.Sleep(23000);
                    } catch (Exception E1) {
                        Common.LogError(E1.ToString() + E1.StackTrace);
                    }
                }
                Common.LogInfo("VtexBranchsSync SyncBranchsThr Ended");
            });
        }
    }
}
