using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Services;
using CoffeeExperience.WebApi.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CoffeeExperience.WebApi.Areas.Client.Controllers
{
    [RoutePrefix("api/v1/userv1")]
    public class UserController : ApiController
    {
        ServiceManager services;
        public UserController()
        {
            UnitOfWork unit = new UnitOfWork();
            services = new ServiceManager(unit);
        }

        [Route("login")]
        [HttpPost]
        [ResponseType(typeof(UserModel))]
        public HttpResponseMessage Login(UserModel user)
        {
            try
            {
                User localUser = services.UserService.Validate(user.Email, user.Password);
                return Request.CreateResponse(HttpStatusCode.OK, localUser);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e);
            }
        }
    }
}