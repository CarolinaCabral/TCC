using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace CoffeeExperience.WebApi.Areas.Client.Controllers
{
    [RoutePrefix("api/v1/lotv1")]
    public class LotController : ApiController
    {
        public ServiceManager services;
        public LotController()
        {
            UnitOfWork unit = new UnitOfWork();
            services = new ServiceManager(unit);
        }
        [HttpGet]
        [Route("list/{id:int}")]
        // GET: Client/Lot
        public HttpResponseMessage ListByUser(int id)
        {
            try
            {
                var lots = services.LotService.GetAllByClient(id).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, lots);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.NotFound, e);
            }
           
        }

        [HttpGet]
        [Route("getbylotcode/{code}")]
        // GET: Client/Lot
        public HttpResponseMessage GetByLotCode(string code)
        {
            try
            {
                var lot = services.LotService.GetByCode(code);
                return Request.CreateResponse(HttpStatusCode.OK, lot);
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.NotFound, e);
            }

        }
    }
}