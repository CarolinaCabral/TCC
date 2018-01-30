using System.Collections.Generic;

namespace CoffeeExperience.Domain.Portable.Entities
{
    public class AnalysisMobile : BaseEntityMobile
    {
        #region Constructor
        public AnalysisMobile()
        {

        }
        public AnalysisMobile(bool success, string details) : base(success, details)
        {

        }
        #endregion

        #region Properties
        public EnmType Type { get;  set; }
        public virtual LotMobile Lot { get;  set; }
        public int LotId { get;  set; }
        public virtual LaboratoryMobile Laboratory { get;  set; }
        public int LaboratoryId { get;  set; }
        public virtual ICollection<QualityResultMobile> ListQualityResult { get;  set; }
        public string Code { get; set; }
        #endregion

        #region ViewProperties
        public string TypeStr
        {
            get
            {
                switch (Type)
                {
                    case EnmType.Tipo6Duro:
                        return "Tipo 6 Duro";
                    case EnmType.Tipo7Duro:
                        return "Tipo 7 Duro";
                    case EnmType.Tipo6Mole:
                        return "Tipo 6 Mole";
                    case EnmType.Tipo7Mole:
                        return "Tipo 7 Mole";
                    case EnmType.Rio:
                        return "Rio";
                    case EnmType.RioRiado:
                        return "Rio Riado";
                    case EnmType.RiadoRio:
                        return "Riado Rio";
                    case EnmType.QuebrouXicara:
                        return "Quebrou Xícara";
                    default:
                        return "";
                }
            }
        }
        #endregion

    }
    public enum EnmType
    {
        Tipo6Duro,
        Tipo7Duro,
        Tipo6Mole,
        Tipo7Mole,
        Rio,
        RioRiado,
        RiadoRio,
        QuebrouXicara
    }
}
