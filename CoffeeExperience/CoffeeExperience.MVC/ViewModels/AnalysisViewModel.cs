using CoffeeExperience.Domain.Entities;
using CoffeeExperience.MVC.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CoffeeExperience.Infra.CrossCutting.Util;

namespace CoffeeExperience.MVC.ViewModels
{
    public class AnalysisViewModel : BaseEntityViewModel
    {
        #region Properties
        [Required(ErrorMessage = "O campo tipo é obrigatório")]
        [Display(Name = "Tipo")]
        public EnmType Type { get; set; }
        //[Required(ErrorMessage = "O campo lote é obrigatório")]
        //[Display(Name = "Lote")]
        public virtual LotViewModel Lot { get; set; }
        [Required(ErrorMessage = "O campo lote é obrigatório")]
        [Display(Name = "Lote")]
        public int? LotId { get; set; }
        //[Required(ErrorMessage = "O campo laboratório é obrigatório")]
        //[Display(Name = "Laboratório")]
        public virtual LaboratoryViewModel Laboratory { get; set; }
        [Required(ErrorMessage = "O campo laboratório é obrigatório")]
        [Display(Name = "Laboratório")]
        public int? LaboratoryId { get; set; }
        public virtual ICollection<QualityResultViewModel> ListQualityResult { get; set; }
        [Required(ErrorMessage = "O campo código é obrigatório")]
        [Display(Name = "Código")]
        public string Code { get; set; }
        #endregion

        #region View Properties

        public List<SelectListItem> Laboratories { get; set; }
        public List<SelectListItem> Lots { get; set; }

        public string TypeStr
        {
            get
            {
                return EnumHelper.GetDescription(Type);
            }
        }
        #endregion

        #region Mapping
        public Analysis ToDomain()
        {
            var config = new MapConfig().ToDomain();
            var mapper = config.CreateMapper();
            Analysis Analysis = mapper.Map<Analysis>(this);
            return Analysis;
        }

        public AnalysisViewModel ToViewModel(Analysis Analysis)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<AnalysisViewModel>(Analysis);
        }

        public IEnumerable<AnalysisViewModel> ToListViewModel(IEnumerable<Analysis> Analysiss)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<Analysis>, IEnumerable<AnalysisViewModel>>(Analysiss);
        }

        #endregion
    }
}