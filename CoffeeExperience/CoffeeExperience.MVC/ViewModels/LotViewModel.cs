using CoffeeExperience.Domain.Entities;
using CoffeeExperience.MVC.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeeExperience.MVC.ViewModels
{
    public class LotViewModel : BaseEntityViewModel
    {
        #region Constructor
        public LotViewModel()
        {
        //    this.ListProduct = new List<ProductViewModel>();
            this.CreationDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
        }
        #endregion

        #region Properties
        [Required(ErrorMessage = "O campo código é obrigatório")]
        [Display(Name = "Código")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Status")]
        public EnmLotStatus LotStatus { get; set; }
        [Required]
        [Display(Name = "Peso")]
        public double Weight { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Validade")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Vality { get; set; }
        public virtual UserViewModel User { get; set; } //mapeamento lazy loading herança
        public int? UserId { get; set; }
        public virtual ICollection<AnalysisViewModel> ListAnalysis { get; set; }
        public List<ProductViewModel> ListProduct { get; set; }

        public ProductViewModel Product { get; set; }
        #endregion

        public Lot ToDomain()
        {
            var config = new MapConfig().ToDomain();
            var mapper = config.CreateMapper();
            Lot Lot = mapper.Map<Lot>(this);
            return Lot;
        }
        public IEnumerable<LotViewModel> ToListViewModel(IEnumerable<Lot> Lots)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<Lot>, IEnumerable<LotViewModel>>(Lots);
        }

        public LotViewModel ToViewModel(Lot lot)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<LotViewModel>(lot);
        }

        public void ValidateProducts()
        {
            ICollection<ProductViewModel> ListProductToRemove = new List<ProductViewModel>();

            if (ListProduct != null)
            {
                foreach (var item in ListProduct)
                {
                    if (string.IsNullOrEmpty(item.Name) || item.Weight == 0)
                    {
                        ListProductToRemove.Add(item);
                    }
                }

                foreach (var item in ListProductToRemove)
                {
                    ListProduct.Remove(item);
                }
            }
        }

    }


}