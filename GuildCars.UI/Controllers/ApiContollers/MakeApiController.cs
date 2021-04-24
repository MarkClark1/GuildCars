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

namespace GuildCars.UI.Controllers.ApiContollers
{
    public class MakeApiController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {                  
            var response = new HttpResponseMessage();
            try
            {
                IMakeRepository repo = RepoFactory.CreateMakeRepo();
                var makes = repo.GetAll();
                response.Content = new StringContent(new JavaScriptSerializer().Serialize(makes));
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
