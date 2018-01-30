using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Infra.CrossCutting.Util;
using CoffeeExperience.MVC.AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeExperience.MVC.ViewModels
{
    public class QualityResultViewModel : BaseEntityViewModel
    {
        #region Properties
        public virtual AnalysisViewModel Analysis { get; set; }

        [Required(ErrorMessage = "O campo análise é obrigatório")]
        public int? AnalysisId { get;  set; }

        [Required(ErrorMessage = "O campo tipo é obrigatório")]
        [Display(Name ="Tipo")]
        public EnmType? Type { get;  set; }

        [Display(Name = "Observação (Quantidade de xícara(s) quebrada(s) e Número da Análise")]
        [StringLength(100, ErrorMessage = "O Número máximo de caractéres são 100")]
        public string Observation { get; set; }

        [Display(Name = "Quebrou Xícara?")]
        [ScaffoldColumn(false)]
        public bool QuebrouXicara { get; set; }
        #endregion

        #region View Properties
        public string TypeStr
        {
            get
            {
                return EnumHelper.GetDescription(Type);
            }
        }
        #endregion

        #region Mapping
        public QualityResult ToDomain()
        {
            var config = new MapConfig().ToDomain();
            var mapper = config.CreateMapper();
            QualityResult QualityResult = mapper.Map<QualityResult>(this);
            return QualityResult;
        }

        public QualityResultViewModel ToViewModel(QualityResult QualityResult)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<QualityResultViewModel>(QualityResult);
        }

        public IEnumerable<QualityResultViewModel> ToListViewModel(IEnumerable<QualityResult> QualityResults)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<QualityResult>, IEnumerable<QualityResultViewModel>>(QualityResults);
        }

        #endregion
    }
}