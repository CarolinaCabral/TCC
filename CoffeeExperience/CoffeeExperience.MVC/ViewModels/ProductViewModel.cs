using CoffeeExperience.Domain.Entities;
using CoffeeExperience.MVC.AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeExperience.MVC.ViewModels
{
    public class ProductViewModel : BaseEntityViewModel
    {
        #region Constructor
        public ProductViewModel()
        {

        }
        public ProductViewModel(int lotId)
        {
            this.LotId = lotId;
        }
        #endregion

        #region Properties
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo peso é obrigatório")]
        [Display(Name = "Peso")]
        public double? Weight { get; set; }
        [ScaffoldColumn(false)]
        public virtual LotViewModel Lot { get; set; }
        [ScaffoldColumn(false)]
        public int LotId { get; set; }

        #endregion

        #region View Properties
        [Display(Name = "Código do lote")]
        public string LotCode { get; set; } //utilizado apenas na view
        #endregion

        public Product ToDomain()
        {
            var config = new MapConfig().ToDomain();
            var mapper = config.CreateMapper();
            Product Product = mapper.Map<Product>(this);
            return Product;
        }

        public ProductViewModel ToViewModel(Product Product)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<ProductViewModel>(Product);
        }

        public IEnumerable<ProductViewModel> ToListViewModel(IEnumerable<Product> Products)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(Products);
        }

    }
}