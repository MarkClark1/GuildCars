using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.JsonModels;
using GuildCars.Models.Queries;
using GuildCars.Models.Queries.Reports;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace GuildCars.UI.Controllers.ApiContollers
{
    public class InventoryAPIController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SearchNewVehicles(JObject searchInput)
        {            
            JavaScriptSerializer jr = new JavaScriptSerializer();
            IVehicleRepository repo = RepoFactory.CreateVehicleRepo();
            var response = new HttpResponseMessage();
           

            try
            {
                SearchVehicleJsonModel searchData = jr.Deserialize<SearchVehicleJsonModel>(searchInput.ToString());
                var vehicles = repo.GetNewVehiclesSorted(searchData.MakeModel, searchData.MinPrice, searchData.MaxPrice, searchData.MinYear, searchData.MaxYear);
                response.Content = new StringContent(jr.Serialize(vehicles));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            }
            catch(Exception)
            {
                response.StatusCode = (HttpStatusCode)400;
            };
            return response;
        }

        [HttpPost]
        public HttpResponseMessage SearchUsedVehicles(JObject searchInput)
        {
            JavaScriptSerializer jr = new JavaScriptSerializer();
            IVehicleRepository repo = RepoFactory.CreateVehicleRepo();
            var response = new HttpResponseMessage();
            try
            {
                SearchVehicleJsonModel searchData = jr.Deserialize<SearchVehicleJsonModel>(searchInput.ToString());
                var vehicles = repo.GetUsedVehiclesSorted(searchData.MakeModel, searchData.MinPrice, searchData.MaxPrice, searchData.MinYear, searchData.MaxYear);
                response.Content = new StringContent(jr.Serialize(vehicles));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            }
            catch (Exception)
            {
                response.StatusCode = (HttpStatusCode)400;
            }
            return response;
        }

        [HttpPost]
        public HttpResponseMessage SearchAllVehicles(JObject searchInput)
        {
            JavaScriptSerializer jr = new JavaScriptSerializer();
            IVehicleRepository repo = RepoFactory.CreateVehicleRepo();
            var response = new HttpResponseMessage();
            try
            {
                SearchVehicleJsonModel searchData = jr.Deserialize<SearchVehicleJsonModel>(searchInput.ToString());
                var vehicles = repo.GetAllVehiclesSorted(searchData.MakeModel, searchData.MinPrice, searchData.MaxPrice, searchData.MinYear, searchData.MaxYear);
                response.Content = new StringContent(jr.Serialize(vehicles));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            }
            catch (Exception)
            {
                response.StatusCode = (HttpStatusCode)400;
            }

            return response;
        }


        [HttpPost]
        public HttpResponseMessage GetSalesReport(JObject searchInput)
        {
            JavaScriptSerializer jr = new JavaScriptSerializer();
            ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();
            var response = new HttpResponseMessage();
            try
            {
                SalesReportJsonModel searchData = jr.Deserialize<SalesReportJsonModel>(searchInput.ToString());
                List<SaleReport> reports = repo.Sales(searchData.UserId, searchData.FromDate, searchData.ToDate);
                response.Content = new StringContent(jr.Serialize(reports));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            }
            catch(Exception)
            {
                response.StatusCode = (HttpStatusCode)400;
            }  
            return response;
        }
    }
}
