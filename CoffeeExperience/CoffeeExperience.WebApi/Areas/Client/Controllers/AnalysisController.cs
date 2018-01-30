using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeExperience.WebApi.Areas.Client.Controllers
{
    [RoutePrefix("api/v1/analysisv1")]
    public class AnalysisController : ApiController
    {
        // GET: Client/Analysis
        ServiceManager services;

        public AnalysisController()
        {
            UnitOfWork unit = new UnitOfWork();
            services = new ServiceManager(unit);
        }

        [HttpGet]
        [Route("getbyanalysiscode/{code}")]
        public HttpResponseMessage GetByCode(string code)
        {
            try
            {
                var analysis = services.AnalysisService.GetByCode(code);
                return Request.CreateResponse(HttpStatusCode.OK, analysis);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.Gone, e);
            }
        }


    }
}