using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Services;
using CoffeeExperience.Domain.TO;
using CoffeeExperience.MVC.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeExperience.MVC.Areas.Client.Controllers
{
    [RouteArea("Client")]
    [Authorize]
    public class LaboratoryController : Controller
    {
        public ServiceManager services;
        public LaboratoryController()
        {
            UnitOfWork unit = new UnitOfWork();
            services = new ServiceManager(unit);
        }
        // GET: Client/Laboratory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.User.Identity.Name);
                var laboratories = services.LaboratoryService.GetAllByClient(userId).ToList();
                return View(new LaboratoryViewModel().ToListViewModel(laboratories).ToList());
            }
            catch (Exception e)
            {

                return Json(new { Msg = e.Message, Erro = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id.HasValue)
            {
                Laboratory laboratory = this.services.LaboratoryService.GetById(id.Value);
                if (laboratory != null)
                {
                    return View(new LaboratoryViewModel().ToViewModel(laboratory));
                }
                else
                {
                    return Json(new { Msg = "Preencha todos os campos", Erro = true });
                }
            }
            return View(new LaboratoryViewModel());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Response response = services.LaboratoryService.Exclude(id);
            return Json(new { Msg = response.Message, Erro = !response.Error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LaboratoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Laboratory laboratorio = model.ToDomain();
                laboratorio.SetUser(Convert.ToInt32(HttpContext.User.Identity.Name));
                Response response = services.LaboratoryService.Save(laboratorio);
                return Json(new { Msg = response.Message, Erro = response.Error });
            }
            else
            {
                return Json(new { Msg = "CNPJ inválido", Erro = true });
            }

        }



    }
}