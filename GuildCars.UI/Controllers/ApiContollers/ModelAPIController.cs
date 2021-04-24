using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
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
    public class ModelAPIController : ApiController
    {
        [Route("api/ModelApi/{id}")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            IMakeRepository makerepo = RepoFactory.CreateMakeRepo();
            IModelRepository repo = RepoFactory.CreateModelRepo();
            var response = new HttpResponseMessage();
            try
            {
                var makes = makerepo.GetAll().Where(x => x.MakeId == id).ToList();
                var models = repo.GetAll().Where(x => x.MakeName == makes[0].Name).ToList();                
                response.Content = new StringContent(new JavaScriptSerializer().Serialize(models));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            }         
            catch (Exception)
            {
                response.StatusCode = (HttpStatusCode)400;
            }
            return response;
        }
    }
}
