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
        public ApiVtex() {
            HttpClientApiO = new HttpClientApi();
        }
        public List<Brand> GetBrands() {            
            string GetBrandsApiStr = ConfigurationManager.AppSettings.Get("GetBrandsApiStr");
            return HttpClientApiO.GetDynamicList<Brand>(GetBrandsApiStr);
        }
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
            return HttpClientApiO.PutDynamic<Category>(CreateCategoryApiStr, V);
        }
    }
}
