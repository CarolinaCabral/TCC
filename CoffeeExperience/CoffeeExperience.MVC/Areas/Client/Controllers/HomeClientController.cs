using CoffeeExperience.Domain.Entities;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace CoffeeExperience.MVC.Areas.Client.Controllers
{
    [Authorize]
    public class HomeClientController : BaseController
    {
        public HomeClientController() : base()
        {
                
        }
        // GET: Client/HomeClient
        public ActionResult Index()
        { 
            User user = services.UserService.GetById(Convert.ToInt32(HttpContext.User.Identity.Name));
            ViewBag.User = user;

            ViewBag.PermiteAtalho = true;
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login", new { Area = "" });
        }
    }
}