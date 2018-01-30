using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoffeeExperience.Domain.Entities
{
    public class Analysis : BaseEntity
    {
        #region Properties
        public EnmType Type { get; protected set; }
        public virtual Lot Lot { get; protected set; }
        public int LotId { get; protected set; }
        public virtual Laboratory Laboratory { get; protected set; }
        public int LaboratoryId { get; protected set; }
        public virtual ICollection<QualityResult> ListQualityResult { get; protected set; }
        public string Code { get; set; }
        #endregion

        #region Methods
        public void Delete()
        {
            this.Status = EnmStatus.Deleted;
        }
        #endregion

    }
    public enum EnmType
    {
        [Description("Tipo 6 Duro")]
        [Display(Name = "Tipo 6 Duro")]
        Tipo6Duro,
        [Description("Tipo 7 Duro")]
        [Display(Name = "Tipo 7 Duro")]
        Tipo7Duro,
        [Description("Tipo 6 Mole")]
        [Display(Name = "Tipo 6 Mole")]
        Tipo6Mole,
        [Description("Tipo 7 Mole")]
        [Display(Name = "Tipo 7 Mole")]
        Tipo7Mole,
        [Description("Rio")]
        [Display(Name = "Rio")]
        Rio,
        [Description("Rio Riado")]
        [Display(Name = "Rio Riado")]
        RioRiado,
        [Description("Riado Rio")]
        [Display(Name = "Riado Rio")]
        RiadoRio,
        [Description("Xicara(s) Quebrada(s)")]
        [Display(Name = "Xicara(s) Quebrada(s)")]
        QuebrouXicara
    }
}
