using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.TO;
using CoffeeExperience.MVC.Helpers;
using CoffeeExperience.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeExperience.MVC.Areas.Client.Controllers
{
    [Authorize]
    public class LotController : BaseController
    {

        public LotController() : base()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.User.Identity.Name);
                var lots = services.LotService.GetAllByClient(userId).ToList();
                return View(new LotViewModel().ToListViewModel(lots).ToList());
            }
            catch (Exception e)
            {

                return Json(new { Msg = e.Message, Erro = true }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = GetLotModel();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Form", model); //Retorna apenas uma parte da página para ser recarregada com Ajax no "UpdateTargetId"
            }
            else
            {
                return View(model);
            }
        }

        private LotViewModel GetLotModel()
        {
            var model = (LotViewModel)this.TempData["LotModel"];

            this.TempData.Remove("LotModel");

            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CommandName("Create", "command:Salvar")]
        public ActionResult Create(LotViewModel model)
        {            
            Lot lot = model.ToDomain();
            lot.SetUser(Convert.ToInt32(HttpContext.User.Identity.Name));
            Response response = services.LotService.Save(lot);

            if (response.Error)
            {
                this.ErrorMessage = response.Message;
                return PartialView("_Form", model);
            }
            else
            {
                this.SuccessMessage = response.Message;
                return PartialView("_Form", model);
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var model = GetLotModel();

                if (model == null || model.Id != id)
                {
                    var lot = services.LotService.GetById(id);

                    model = new LotViewModel().ToViewModel(lot);
                }

                return PartialView("_Form", model);

            }
            catch (Exception e)
            {
                return Json(new { Msg = e.Message, Erro = true });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CommandName("Edit", "command:Salvar")]
        public ActionResult Edit(LotViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Lot lot = model.ToDomain();
                    lot.SetUser(Convert.ToInt32(HttpContext.User.Identity.Name));
                    Response response = services.LotService.Save(lot);

                    if (response.Error)
                    {
                        this.ErrorMessage = response.Message;

                    }
                    else
                    {
                        this.SuccessMessage = response.Message;
                    }

                    return PartialView("_Form", model);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }

            return PartialView("_Form", model);
        }

        [HttpPost]
        [CommandName("Create,Edit", "command:AddProduct")]
        public ActionResult AddProduct(LotViewModel model)
        {
            ModelState.Clear(); // necessário para limpar eventuais validações
            this.ClearMessages();

            model.Product = new ProductViewModel(model.Id);

            if (model.ListProduct == null)
            {
                model.ListProduct = new List<ProductViewModel>();
            }

            return PartialView("_Form", model);
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            var model = GetLotModel();

            return View(model);
        }

        [HttpPost]
        [CommandName("Create,Edit", "command:EditProduct;index:{index}")]
        public ActionResult EditProduct(LotViewModel model)
        {
            ModelState.Clear();

            int index;

            if (int.TryParse((string)RouteData.Values["index"], out index))
            {
                model.Product = model.ListProduct[index];

                ViewBag.Index = index;
            }

            return PartialView("_Form", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProduct(LotViewModel lot, int? index = null)
        {
            // limpar todo os erros exceto aqueles relacionados a produto
            ModelState.Where(m => !m.Key.StartsWith("Product.")).ToList().ForEach(m => m.Value.Errors.Clear());

            if (ModelState.IsValid)
            {
                if (index.HasValue)
                {
                    lot.ListProduct[index.Value] = lot.Product;
                }
                else
                {
                    lot.ListProduct.Add(lot.Product);
                }

                lot.Product = null;

                TempData["LotModel"] = lot;

                string url = lot.Id == 0
                    ? this.Url.Action("Create")
                    : this.Url.Action("Edit", new { id = lot.Id });

                string update = lot.Id == 0 ? "#ajax-tab-acao-criar-lot" : "#ajax-tab-acao-editar-lot-" + lot.Id;

                return Json(new { redirect = url, update = update });

            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_FormProduct", lot);
            }
            else
            {
                if (index.HasValue)
                {
                    return View("EditProduct", lot);
                }
                else
                {
                    return View("CreateProduct", lot);
                }
            }
        }

        [HttpPost]
        [CommandName("Create,Edit", "command:DeleteProduct;index:{index}")]
        public ActionResult DeleteProduct(LotViewModel model)
        {
            try
            {
                ModelState.Clear();

                int index;

                if (int.TryParse((string)RouteData.Values["index"], out index))
                {
                    model.ListProduct.RemoveAt(index);

                    ViewBag.Index = index;
                }

                return PartialView("_Form", model);

            }
            catch (Exception)
            {
                return Json(new { Msg = "Não foi possível excluir!", Erro = true });
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Response response = services.LotService.Exclude(id);
            return Json(new { Msg = response.Message, Erro = !response.Error });
        }


    }
}