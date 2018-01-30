using System;
using System.Globalization;

namespace CoffeeExperience.Domain.Portable.Entities
{
    public class BaseEntityMobile
    {
        #region Properties
        public int Id { get;  set; }
        public DateTime CreationDate { get;  set; }
        public EnmStatus Status { get;  set; }

        public bool Success { get; set; }
        public string Details { get; set; }
        #endregion

        #region ViewProperties
        public string CreationDateStr
        {
            get
            {
                CultureInfo ci = new CultureInfo("pt-BR");
                return CreationDate.ToString("G", ci);
            }
        }
        #endregion

        #region Constructors
        public BaseEntityMobile(bool success, string details)
        {
            Success = success;
            Details = details;
        }
        public BaseEntityMobile()
        {

        }
        #endregion

    }

    public enum EnmStatus 
    {
        Disabled,
        Enabled,
        Deleted
    }
}
