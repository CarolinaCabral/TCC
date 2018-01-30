using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Services;
using CoffeeExperience.Domain.TO;
using CoffeeExperience.MVC.ViewModels;
using System;
using System.Web.Mvc;

namespace CoffeeExperience.MVC.Controllers
{
    public class UserController : Controller
    {
        public ServiceManager services;
        public UserController()
        {
            UnitOfWork unit = new UnitOfWork();
            services = new ServiceManager(unit);
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create(UserViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Response response = services.UserService.Save(model.ToDomain());
                    return Json(new { Msg = response.Message, Erro = response.Error });
                }
                else
                {
                    return Json(new { Msg = "CPF inválido", Erro = true });
                }
            }
            catch (Exception e)
            {
                return Json(new { Msg = e.Message, Erro = true });
            }
        }
    }
}