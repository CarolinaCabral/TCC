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
    public class UserViewModel : BaseEntityViewModel
    {
        #region Properties
        [Required(ErrorMessage ="O campo nome é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        [ScaffoldColumn(false)]
        public string FacebookId { get; set; }
        [ScaffoldColumn(false)]
        public string Token { get; set; }
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [Display(Name = "CPF")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        [StringLength(14)]
        public string CPF { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? LastAccess { get; set; }
        public virtual ICollection<LotViewModel> ListLots { get; set; }
        public virtual ICollection<LaboratoryViewModel> ListLaboratory { get; set; }

        #endregion

        #region Mapping

        public User ToDomain()
        {
            var config = new MapConfig().ToDomain();
            var mapper = config.CreateMapper();
            User User = mapper.Map<User>(this);
            return User;
        }

        public UserViewModel ToViewModel(User User)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<UserViewModel>(User);
        }

        public IEnumerable<UserViewModel> ToListViewModel(IEnumerable<User> Users)
        {
            var config = new MapConfig().ToViewModel();
            var mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(Users);
        }
        #endregion
    }
}