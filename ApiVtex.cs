using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VtexCon.Models;

namespace VtexCon {
    public class ApiVtex {
        static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        HttpClientApi HttpClientApiO { get; set; }
        public string LastHttpError { get { return HttpClientApiO.LastHttpError; } }
        public ApiVtex(short Typ, string BaseEndPoint, string VTexMainApiAppKey, string VTexMainApiAppPassword) {
            HttpClientApiO = new HttpClientApi(Typ, BaseEndPoint, VTexMainApiAppKey, VTexMainApiAppPassword);
        }
        public List<Brand> GetBrands() {            
            string GetBrandsApiStr = ConfigurationManager.AppSettings.Get("GetBrandsApiStr");
            return HttpClientApiO.GetDynamicList<Brand>(GetBrandsApiStr);
        }
        //Categories
        public List<CategoryTree> GetCatTree(int Level) {
            string GetCatTreeApiStr = $"{ConfigurationManager.AppSettings.Get("GetCatTreeApiStr")}{Level}";
            return HttpClientApiO.GetDynamicList<CategoryTree>(GetCatTreeApiStr);
        }
        public Category GetCategoryInfo(string Id) {
            string GetCategoryApiStr = $"{ConfigurationManager.AppSettings.Get("GetCategoryApiStr")}{Id}";
            return HttpClientApiO.GetDynamic<Category>(GetCategoryApiStr);
        }
        public Category UpdateCategory(Category V) {
            string UpdateCategoryApiStr = $"{ConfigurationManager.AppSettings.Get("UpdateCategoryApiStr")}{V.Id}";
            return HttpClientApiO.PutDynamic<Category>(UpdateCategoryApiStr, V);
        }
        public Category CreateCategory(Category V) {
            string CreateCategoryApiStr = $"{ConfigurationManager.AppSettings.Get("CreateCategoryApiStr")}";
            return HttpClientApiO.PostDynamic<Category>(CreateCategoryApiStr, V);
        }
        //Products
        public List<Product> GetProductAndSkuIds(int CatId, int From, int To) {
            string GetProductAndSkuIds = $"{ConfigurationManager.AppSettings.Get("GetProductAndSkuIds")}?categoryId={CatId}&_from={From}&_to={To}";
            return HttpClientApiO.GetDynamicList<Product>(GetProductAndSkuIds);
        }
        public Product GetProductById(int Id) {
            string GetProductById = $"{ConfigurationManager.AppSettings.Get("GetProductById")}{Id}";
            return HttpClientApiO.GetDynamic<Product>(GetProductById);
        }
        public Product GetProductByRefId(string RefId) {
            string GetProductByRefId = $"{ConfigurationManager.AppSettings.Get("GetProductByRefId")}{RefId}";
            return HttpClientApiO.GetDynamic<Product>(GetProductByRefId);
        }
        public Product UpdateProduct(Product V, int ProductId) {
            string UpdateProduct = $"{ConfigurationManager.AppSettings.Get("UpdateProduct")}{ProductId}";
            //V.Id = null;
            return HttpClientApiO.PutDynamic<Product>(UpdateProduct, V);
        }
        public Product CreateProduct(Product V) {
            string CreateProduct = $"{ConfigurationManager.AppSettings.Get("CreateProduct")}";
            return HttpClientApiO.PostDynamic<Product>(CreateProduct, V);
        }
        //Product Specifications
        public List<GetProductSpecification> GetProductSpecification(int ProductId) {
            string GetProductSpecification = string.Format(ConfigurationManager.AppSettings.Get("GetProductSpecification"), ProductId);
            return HttpClientApiO.GetDynamicList<GetProductSpecification>(GetProductSpecification);
        }
        public NewProductSpecification CreateProductSpecification(int ProductId, NewProductSpecification V) {
            string CreateProductSpecification = string.Format(ConfigurationManager.AppSettings.Get("CreateProductSpecification"), ProductId);
            return HttpClientApiO.PostDynamic<NewProductSpecification>(CreateProductSpecification, V);
        }
        public NewProductSpecification UpdateProductSpecification(int ProductId, NewProductSpecification V) {
            string UpdateProductSpecification = string.Format(ConfigurationManager.AppSettings.Get("UpdateProductSpecification"), ProductId);
            return HttpClientApiO.PostDynamic<NewProductSpecification>(UpdateProductSpecification, V);
        }
        public void DeleteProductSpecification(int ProductId) {
            string DeleteProductSpecification = string.Format(ConfigurationManager.AppSettings.Get("DeleteProductSpecification"), ProductId);
            HttpClientApiO.Delete(DeleteProductSpecification);
        }
        //Sku
        public List<GetSkuByProductId> GetSkusByProductId(int ProductId) {
            string GetSkusByProductId = $"{ConfigurationManager.AppSettings.Get("GetSkusByProductId")}{ProductId}";
            return HttpClientApiO.GetDynamicList<GetSkuByProductId>(GetSkusByProductId);
        }
        public NewSku GetSku(int Id) {
            string GetSku = $"{ConfigurationManager.AppSettings.Get("GetSku")}{Id}";
            return HttpClientApiO.GetDynamic<NewSku>(GetSku);
        }
        public int? GetSkuIdByRefId(string RefId) {
            string GetSkuIdByRefId = $"{ConfigurationManager.AppSettings.Get("GetSkuIdByRefId")}{RefId}";
            return HttpClientApiO.GetDynamic<int?>(GetSkuIdByRefId);
        }
        public NewSku CreateSku(NewSku V) {
            string CreateSku = $"{ConfigurationManager.AppSettings.Get("CreateSku")}";
            return HttpClientApiO.PostDynamic<NewSku>(CreateSku, V);
        }
        public NewSku UpdateSku(NewSku V) {
            string UpdateSku = $"{ConfigurationManager.AppSettings.Get("UpdateSku")}{V.Id}";
            return HttpClientApiO.PutDynamic<NewSku>(UpdateSku, V);
        }
        public string GetEanBySku(int SkuId) {
            string GetEanBySkuId = string.Format(ConfigurationManager.AppSettings.Get("GetEanBySkuId"), SkuId);
            List<string> ListEans = HttpClientApiO.GetDynamicList<string>(GetEanBySkuId);
            if (ListEans == null) return string.Empty;
            if (ListEans.Count > 0) return ListEans.FirstOrDefault();
            return string.Empty;
        }
        public string CreateSkuEan(int SkuId, string Ean) {
            string CreateSkuEan = string.Format(ConfigurationManager.AppSettings.Get("CreateSkuEan"), SkuId, Ean);
            return HttpClientApiO.PostDynamic<string>(CreateSkuEan, "");
        }
        public void DeleteSkuEan(int SkuId) {
            string DeleteSkuEan = string.Format(ConfigurationManager.AppSettings.Get("DeleteSkuEan"), SkuId);
            HttpClientApiO.Delete(DeleteSkuEan);
        }
        public List<GetSkuFile> GetSkuFile(int SkuId) {
            string GetSkuFile = string.Format(ConfigurationManager.AppSettings.Get("GetSkuFile"), SkuId);
            return HttpClientApiO.GetDynamicList<GetSkuFile>(GetSkuFile);
        }
        public NewSkuFile CreateSkuFile(int SkuId, NewSkuFile V) {
            string CreateSkuFile = string.Format(ConfigurationManager.AppSettings.Get("CreateSkuFile"), SkuId);
            return HttpClientApiO.PostDynamic<NewSkuFile>(CreateSkuFile, V);
        }
        //Category Specification
        public List<CategorySpecification> GetSpecificationsByCategoryId(int CatId) {
            string GetSpecificationsByCategoryId = $"{ConfigurationManager.AppSettings.Get("GetSpecificationsByCategoryId")}{CatId}";
            return HttpClientApiO.GetDynamicList<CategorySpecification>(GetSpecificationsByCategoryId);
        }
        public List<CategorySpecification> GetSpecificationsTreeByCategoryId(int CatId) {
            string GetSpecificationsTreeByCategoryId = $"{ConfigurationManager.AppSettings.Get("GetSpecificationsTreeByCategoryId")}{CatId}";
            return HttpClientApiO.GetDynamicList<CategorySpecification>(GetSpecificationsTreeByCategoryId);
        }
        //Warehouses, stock
        public List<GetWarehouses> GetWarehouses() {
            string GetWarehouses = ConfigurationManager.AppSettings.Get("GetWarehouses");
            return HttpClientApiO.GetDynamicList<GetWarehouses>(GetWarehouses);
        }
        public List<GetInventoryPerWarehouse> GetInventoryPerWarehouse() {
            string GetInventoryPerWarehouse = ConfigurationManager.AppSettings.Get("GetInventoryPerWarehouse");
            return HttpClientApiO.GetDynamicList<GetInventoryPerWarehouse>(GetInventoryPerWarehouse);
        }
        public bool UpdateInventoryBySkuIdWarehouse(int SkuId, string WarehouseId, UpdateInventoryBySkuIdWarehouse V) {
            string UpdateInventoryBySkuIdWarehouse = string.Format(ConfigurationManager.AppSettings.Get("UpdateInventoryBySkuIdWarehouse"), SkuId, WarehouseId);
            return HttpClientApiO.PutDynamic<UpdateInventoryBySkuIdWarehouse, bool>(UpdateInventoryBySkuIdWarehouse, V);
        }
        //Prices
        public GetPrice GetPriceBySkuId(string Branch, int SkuId) {
            string GetPrice = string.Format(ConfigurationManager.AppSettings.Get("GetPrice"), Branch, SkuId);
            return HttpClientApiO.GetDynamic<GetPrice>(GetPrice);
        }
    }
}
