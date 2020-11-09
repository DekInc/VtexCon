using log4net;
using System;
using System.Collections.Generic;
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
            List<Brand> ListBrands = new List<Brand>();
            ListBrands.Add(new Brand() {
                Active = true,
                Name = "PepsiCo",
                Text = "PepsiCo",
                Keywords = "Pepsi PepsiCo veneno",
                SiteTitle = "PepsiCo",
                MenuHome = true,
                AdWordsRemarketingCode = "",
                LomadeeCampaignCode = "",
                Score = null,
                LinkId = "PepsiCo"
            });
            ListBrands.Add(new Brand() {
                Active = true,
                Name = "The Coca-Cola Company",
                Text = "The Coca-Cola Company",
                Keywords = "The Coca cola veneno",
                SiteTitle = "The Coca-Cola Company",
                MenuHome = true,
                AdWordsRemarketingCode = "",
                LomadeeCampaignCode = "",
                Score = null,
                LinkId = "TheCocaColaCompany"
            });
            return ListBrands;
            //return HttpClientApiO.GetDynamicList<Brand>(@"api/catalog_system/pvt/brand/list");
        }
        //public GetUsersModel GetUsers() {
        //    return HttpClientApiO.GetDynamic<GetUsersModel>("users/");
        //}        
    }
}
