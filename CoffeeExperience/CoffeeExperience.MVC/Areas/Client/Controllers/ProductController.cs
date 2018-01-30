using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Services;
using CoffeeExperience.Domain.TO;
using CoffeeExperience.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeExperience.MVC.Areas.Client.Controllers
{
    [RouteArea("Client")]
    [Authorize]
    public class ProductController : Controller
    {
        public ServiceManager services;

        public ProductController()
        {
            UnitOfWork unit = new UnitOfWork();
            services = new ServiceManager(unit);
        }
        [HttpGet]
        public ActionResult Index(string LotCode)
        {
            ProductViewModel product = new ProductViewModel();
            product.LotCode = LotCode;
            return View(product);
        }
        
        public ActionResult List(string LotCode)
        {
            try
            {
                var produtos = services.ProductService.GetAllByLot(LotCode.Trim(), false).ToList();
                return View(new ProductViewModel().ToListViewModel(produtos).ToList());
            }
            catch (Exception e)
            {
                return Json(new { Msg = e.Message, Erro = true }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult Create(string LotCode)
        {
            ProductViewModel product = new ProductViewModel();
            product.LotCode = LotCode;
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.LotId = services.LotService.GetByCode(model.LotCode).Id;
                    Product product = model.ToDomain();
                    Response response = services.ProductService.Save(product);
                    return Json(new { Msg = response.Message, Erro = response.Error });
                }
                else
                {
                    return Json(new { Msg = "Preencha todos os campos!", Erro = true });
                }
            }
            catch (Exception e)
            {

                return Json(new { Msg = e.Message, Erro = true });
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                Product product = services.ProductService.GetById(id);
                return View(new ProductViewModel().ToViewModel(product));

            }
            catch (Exception e)
            {

                return Json(new { Msg = e.Message, Erro = true }, JsonRequestBehavior.AllowGet);
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                Product product = model.ToDomain();
                Response response = services.ProductService.Save(product);
                return Json(new { Msg = response.Message, Erro = response.Error });
            }
            else
            {
                return Json(new { Msg = "Preencha todos os campos!", Erro = true });
            }

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Response response = services.ProductService.Exclude(id);
            return Json(new { Msg = response.Message, Erro = !response.Error });
        }
    }
}