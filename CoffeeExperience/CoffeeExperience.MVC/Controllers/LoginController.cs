using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Services;
using CoffeeExperience.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace CoffeeExperience.MVC.Controllers
{
    public class LoginController : Controller
    {
        public ServiceManager services;
        public LoginController()
        {
            UnitOfWork unit = new UnitOfWork();
            services = new ServiceManager(unit);
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Entrar(LoginViewModel loginModel, FormCollection collection)
        {
            try
            {
                User user = services.UserService.Validate(loginModel.Email, loginModel.Password);
                services.UserService.SetLastAccess(user);
                FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
                return Json(new { Url = Url.Action("Index", "HomeClient", new { Area = "Client" }), Erro = false });
            }
            catch (Exception e)
            {

                return Json(new { Msg = e.Message, Erro = true });
            }
        }
    }
}