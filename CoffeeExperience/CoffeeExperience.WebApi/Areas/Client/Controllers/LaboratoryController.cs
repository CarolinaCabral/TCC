using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace CoffeeExperience.WebApi.Areas.Client.Controllers
{
    [RoutePrefix("api/v1/laboratoryv1")]
    public class LaboratoryController : ApiController
    {
        ServiceManager services;
        public LaboratoryController()
        {
            UnitOfWork unit = new UnitOfWork();
            services = new ServiceManager(unit);
        }

        [HttpGet]
        [Route("getbylaboratorycode/{code}")]
        public HttpResponseMessage GetByCode(string code)
        {
            try
            {
                var laboratory = services.LaboratoryService.GetByCode(code);
                return Request.CreateResponse(HttpStatusCode.OK, laboratory);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e);
            }
        }



    }
}