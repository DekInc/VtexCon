using log4net;
using System;
using System.Diagnostics;
using System.Threading;
using Vital.Oracle.Services;
using Vital.Oracle.Services.Model;
using VtexCon.Models;
using VtexCon.ModelsOra.Model;

namespace Vital.VtexMainSync {
    static public class Common {
        static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static public void LogInfo(string Msj) {
            string ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            Log.Info($"{ThreadId} -  {Msj}");
            Debug.WriteLine($"{ThreadId} -  {Msj}");
            ApiOracle ApiOracleO = new ApiOracle();
            ApiOracleO.WriteOraLog(2, (new String2($"{ThreadId} - {Msj}", 4000)).Get());
        }
        static public void LogError(string Msj) {
            string ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            Log.Error($"{ThreadId} -  {Msj}");
            Debug.WriteLine($"{ThreadId} -  {Msj}");
            ApiOracle ApiOracleO = new ApiOracle();
            ApiOracleO.WriteOraLog(2, (new String2($"{ThreadId} - {Msj}", 4000)).Get());
        }
        static public Product CreateProductFromOraInfo(VtexProduct ProductFromOra) {
            try {
                return new Product() {
                    Id = ProductFromOra.RefId.ToInt(),
                    Name = ProductFromOra.Name,
                    DepartmentId = ProductFromOra.DepartmentId.ToInt(),
                    CategoryId = ProductFromOra.CategoryId.ToInt(),
                    BrandId = ProductFromOra.BrandId.ToInt(),
                    LinkId = ProductFromOra.LinkId,
                    RefId = ProductFromOra.RefId.Trim(),
                    IsVisible = ProductFromOra.IsVisible,
                    Description = ProductFromOra.Description,
                    DescriptionShort = ProductFromOra.DescriptionShort,
                    ReleaseDate = ProductFromOra.ReleaseDate,
                    KeyWords = ProductFromOra.KeyWords,
                    Title = ProductFromOra.Title,
                    IsActive = ProductFromOra.IsActive,
                    MetaTagDescription = ProductFromOra.MetaTagDescription,
                    ShowWithoutStock = ProductFromOra.ShowWithoutStock,
                    Score = ProductFromOra.Score.ToInt(),
                    SupplierId = 1,
                    Update = false
                };
            } catch (Exception E1) {
                LogError("Common, error al mapear entidad producto de oracle a entidad Vtex. Det: " + E1.ToString() + E1.StackTrace);
                return default(Product);
            }
        }

        static public Product UpdateProductFromOraInfo(Product OldProduct, VtexProduct ProductFromOra) {
            try {
                return new Product() {
                    Id = OldProduct.Id,
                    Name = ProductFromOra.Name,
                    DepartmentId = ProductFromOra.DepartmentId.ToInt(),
                    CategoryId = ProductFromOra.CategoryId.ToInt(),
                    BrandId = ProductFromOra.BrandId.ToInt(),
                    LinkId = ProductFromOra.LinkId,
                    RefId = ProductFromOra.RefId.Trim(),
                    IsVisible = ProductFromOra.IsVisible,
                    Description = ProductFromOra.Description,
                    DescriptionShort = ProductFromOra.DescriptionShort,
                    ReleaseDate = ProductFromOra.ReleaseDate,
                    KeyWords = ProductFromOra.KeyWords,
                    Title = ProductFromOra.Title,
                    IsActive = ProductFromOra.IsActive,
                    MetaTagDescription = ProductFromOra.MetaTagDescription,
                    ShowWithoutStock = true, //OldProduct.ShowWithoutStock,
                    Score = ProductFromOra.Score.ToInt(),
                    TaxCode = null,
                    Update = true,
                    //AdWordsRemarketingCode = OldProduct.AdWordsRemarketingCode,
                    //LomadeeCampaignCode = OldProduct.LomadeeCampaignCode,
                    SupplierId = OldProduct.SupplierId
                };
            } catch (Exception E1) {
                LogError("Common, error al mapear entidad producto de oracle a entidad Vtex. Det: " + E1.ToString() + E1.StackTrace);
                return default(Product);
            }
        }

        static public NewSku CreateSkuFromOraInfo(VtexNewSku SkuFromOra) {
            try {
                return new NewSku() {
                    Id = SkuFromOra.RefId.ToInt(),
                    ProductId = SkuFromOra.RefId.ToInt(),
                    IsActive = false,
                    Name = SkuFromOra.Name,
                    RefId = SkuFromOra.RefId.Trim(),
                    MeasurementUnit = SkuFromOra.MeasurementUnit.ToUpper(),
                    UnitMultiplier = SkuFromOra.UnitMultiplier.ToInt(),
                    KitItensSellApart = false,
                    Height = 10,
                    Width = 10,
                    Length = 10,
                    WeightKg = 10,
                    CubicWeight = 1,
                    CommercialConditionId = 1,
                    ManufacturerCode = "",
                    PackagedHeight = 0.02,
                    PackagedLength = 0.02,
                    PackagedWeightKg = 0.02,
                    PackagedWidth = 0.02,
                };
            } catch (Exception E1) {
                LogError("Common, error al mapear entidad Sku de oracle a entidad Vtex. Det: " + E1.ToString() + E1.StackTrace);
                return default(NewSku);
            }
        }

        static public NewSku UpdateSkuFromOraInfo(NewSku OldSku, VtexNewSku SkuFromOra) {
            try {
                return new NewSku() {
                    ProductId = OldSku.ProductId,
                    IsActive = SkuFromOra.IsActive,
                    Name = SkuFromOra.Name,
                    RefId = OldSku.RefId.Trim(),
                    MeasurementUnit = SkuFromOra.MeasurementUnit.ToUpper(),
                    UnitMultiplier = SkuFromOra.UnitMultiplier.ToInt(),
                    KitItensSellApart = OldSku.KitItensSellApart,
                    CreationDate = OldSku.CreationDate,
                    EstimatedDateArrival = OldSku.EstimatedDateArrival,
                    Id = OldSku.Id,
                    IsKit = OldSku.IsKit,
                    ModalType = OldSku.ModalType,
                    PackagedHeight = OldSku.PackagedHeight > 0.01? OldSku.PackagedHeight : 0.02,
                    PackagedLength = OldSku.PackagedLength > 0.01? OldSku.PackagedLength : 0.02,
                    PackagedWeightKg = OldSku.PackagedWeightKg > 0.01? OldSku.PackagedWeightKg : 0.02,
                    PackagedWidth = OldSku.PackagedWidth > 0.01? OldSku.PackagedWidth : 0.02,
                    RewardValue = OldSku.RewardValue,
                    Height = 10,
                    Width = 10,
                    Length = 10,
                    WeightKg = 10,
                    CubicWeight = 1,
                    CommercialConditionId = 1,
                    ManufacturerCode = ""
                };
            } catch (Exception E1) {
                LogError("Common, error al mapear entidad Sku de oracle a entidad Vtex. Det: " + E1.ToString() + E1.StackTrace);
                return default(NewSku);
            }
        }
    }
}
