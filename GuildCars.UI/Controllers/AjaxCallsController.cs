using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace GuildCars.UI.Controllers
{
    public class AjaxCallsController : ApiController
    {
        [Route("ajaxCalls/makes")]
        [HttpGet]
        public HttpResponseMessage Makes()
        {
            IMakeRepository repo = RepoFactory.CreateMakeRepo();
            var makes = repo.GetAll();

            var response = new HttpResponseMessage();
            response.Content = new StringContent(new JavaScriptSerializer().Serialize(makes));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");

            return response;
        }
        [HttpGet]
        public HttpResponseMessage Get()
        {
            IModelRepository repo = RepoFactory.CreateModelRepo();
            var models = repo.GetAll();

            var response = new HttpResponseMessage();
            response.Content = new StringContent(new JavaScriptSerializer().Serialize(models));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");

            return response;
        }

        [Route("ajaxcalls/models")]
        [HttpGet]
        public HttpResponseMessage models()
        {
            IModelRepository repo = RepoFactory.CreateModelRepo();
            var models = repo.GetAll();

            var response = new HttpResponseMessage();
            response.Content = new StringContent(new JavaScriptSerializer().Serialize(models));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");

            return response;
        }

       
    }
}
