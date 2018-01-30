using CoffeeExperience.Domain.Entities;
using CoffeeExperience.MVC.AutoMapper;
using CoffeeExperience.MVC.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeeExperience.MVC.ViewModels
{
    public class LaboratoryViewModel : BaseEntityViewModel
    {
        #region Properties
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo cidade é obrigatório")]
        [Display(Name = "Cidade")]
        public string City { get; set; }
        [Required(ErrorMessage = "O campo CNPJ é obrigatório")]
        [Display(Name = "CNPJ")]
        [CustomValidationCNPJ(ErrorMessage = "CNPJ inválido")]
        [StringLength(18)]
        public string CNPJ { get; set; }
        public virtual UserViewModel User { get; set; }
        
        public int? UserId { get; set; }
        public virtual ICollection<AnalysisViewModel> ListAnalysis { get; set; }
        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        #endregion

        #region Mapping
        public Laboratory ToDomain()
        {
            var config = new MapConfig().ToDomain();
            var mapper = config.CreateMapper();
            Laboratory Laboratory = mapper.Map<Laboratory>(this);
            return Laboratory;
        }

        public LaboratoryViewModel ToViewModel(Laboratory Laboratory)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<LaboratoryViewModel>(Laboratory);
        }

        public IEnumerable<LaboratoryViewModel> ToListViewModel(IEnumerable<Laboratory> Laboratorys)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<Laboratory>, IEnumerable<LaboratoryViewModel>>(Laboratorys);
        }

        #endregion
        
    }
}