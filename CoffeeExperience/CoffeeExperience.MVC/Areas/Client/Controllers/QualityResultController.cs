using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.TO;
using CoffeeExperience.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeExperience.MVC.Areas.Client.Controllers
{
    public class QualityResultController : BaseController
    {
        public QualityResultController() : base()
        {

        }

        public ActionResult Index(int AnalysisId)
        {

            QualityResultViewModel qualityResult = new QualityResultViewModel();
            qualityResult.AnalysisId = AnalysisId;


            return View(qualityResult);
        }

        public ActionResult List(int? AnalysisId)
        {
            try
            {
                var analisys = services.QualityResultService.GetByAnalysisId(AnalysisId.Value).ToList();
                return View(new QualityResultViewModel().ToListViewModel(analisys).ToList());
            }
            catch (Exception e)
            {
                return Json(new { Msg = e.Message, Erro = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult CreateOrEdit(int? AnalysisId, int? QualityResultId)
        {
            try
            {
                if (QualityResultId.HasValue) //quando o id tem valor é uma edição, pois ele vai na serviço procurar a analise para retornar a view
                {
                    QualityResult qualityResult = services.QualityResultService.GetById(QualityResultId.Value);
                    if (qualityResult != null)
                    {
                        var qualityResultViewModel = new QualityResultViewModel().ToViewModel(qualityResult);                        
                        return View(qualityResultViewModel);
                    }
                    else
                    {
                        return Json(new { Msg = "Resultado da qualidade não encontrada", Erro = true }, JsonRequestBehavior.AllowGet );
                    }
                }

                return View(new QualityResultViewModel());
            }
            catch (Exception e)
            {

                return Json(new { Msg = e.Message, Erro = false });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(QualityResultViewModel model)
        {
            if (ModelState.IsValid)
            {
                QualityResult quality = model.ToDomain();
                Response response = services.QualityResultService.Save(quality);
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
            Response response = services.QualityResultService.Exclude(id);
            return Json(new { Msg = response.Message, Erro = !response.Error });
        }
    }
}