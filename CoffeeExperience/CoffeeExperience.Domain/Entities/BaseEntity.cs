using System;
using System.ComponentModel;

namespace CoffeeExperience.Domain.Entities
{
    public class BaseEntity
    {
        #region Properties
        public int Id { get;  set; }
        public DateTime CreationDate { get;  set; }
        public EnmStatus Status { get;  set; }
        #endregion

    }

    public enum EnmStatus 
    {
        [Description("Desabilitado")]
        Disabled,
        [Description("Habilitado")]
        Enabled,
        [Description("Excluído")]
        Deleted
    }
}
