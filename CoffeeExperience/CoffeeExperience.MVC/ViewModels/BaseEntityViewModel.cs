using CoffeeExperience.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CoffeeExperience.Infra.CrossCutting.Util;

namespace CoffeeExperience.MVC.ViewModels
{
    public class BaseEntityViewModel
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreationDate { get;set; }
        [ScaffoldColumn(false)]
        public EnmStatus Status { get; set; }

        #endregion

        #region ViewProperties
        public string StatusStr
        {
            get
            {
                return EnumHelper.GetDescription(Status);
            }
        }

        #endregion

    }
}