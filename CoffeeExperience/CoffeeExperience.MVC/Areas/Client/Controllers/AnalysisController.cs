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
    [Authorize]
    public class AnalysisController : BaseController
    {
        public AnalysisController() : base()
        {
        
            
        }
        // GET: Client/Analysis
        public ActionResult Index(int? LaboratoryId, string LotCode)
        { 

            AnalysisViewModel analysis = new AnalysisViewModel();

            if(!string.IsNullOrEmpty(LotCode))
            {
                analysis.LotId = services.LotService.GetByCode(LotCode).Id;
            }
            
            analysis.LaboratoryId = LaboratoryId;

             return View(analysis);
        }

        public ActionResult List(int? LaboratoryId, int? LotId)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.User.Identity.Name);
                var analisys = services.AnalysisService.GetAllForAnalysisPage(userId, LaboratoryId, LotId).ToList();
                return View(new AnalysisViewModel().ToListViewModel(analisys).ToList());
            }
            catch (Exception e)
            {

                return Json(new { Msg = e.Message, Erro = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult CreateOrEdit(int? id)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.User.Identity.Name);
                AnalysisViewModel analysisViewModel = new AnalysisViewModel();

                if (id.HasValue) //quando o id tem valor é uma edição, pois ele vai na serviço procurar a analise para retornar a view
                {
                   
                    Analysis analysis = services.AnalysisService.GetById(id.Value);
                    if (analysis != null)
                    {
                        analysisViewModel = new AnalysisViewModel().ToViewModel(analysis);
                        analysisViewModel.Lots = services.LotService.GetAllByClient(userId).OrderBy(p => p.Code).ToSelectList(t => t.Code, x => x.Id.ToString(), analysis.LotId.ToString());
                        analysisViewModel.Laboratories = services.LaboratoryService.GetAllByClient(userId).OrderBy(p => p.Name).ToSelectList(t => t.Name, x => x.Id.ToString(), analysis.LaboratoryId.ToString());
                        return View(analysisViewModel);
                    }
                    else
                    {
                        return Json(new { Msg = "Análise não encontrada", Erro = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                analysisViewModel.Lots = services.LotService.GetAllByClient(userId).OrderBy(p => p.Code).ToSelectList(t => t.Code, x => x.Id.ToString(), "Lote", "");
                analysisViewModel.Laboratories = services.LaboratoryService.GetAllByClient(userId).OrderBy(p => p.Name).ToSelectList(t => t.Name, x => x.Id.ToString(), "Laboratório", "");
                return View(analysisViewModel);
            }
            catch (Exception e)
            {

                return Json(new { Msg = e.Message, Erro = false });
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(AnalysisViewModel model)
        {
            if (ModelState.IsValid)
            {
                Analysis analysis = model.ToDomain();
                Response response = services.AnalysisService.Save(analysis);
                return Json(new { Msg = response.Message, Erro = response.Error });
            }
            else
            {
                return Json(new { Msg = "Campos inválidos", Erro = true });
            }

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Response response = services.AnalysisService.Exclude(id);
            return Json(new { Msg = response.Message, Erro = !response.Error });
        }



    }
}