using System;

namespace CoffeeExperience.Domain.Portable.Entities
{
    public class QualityResultMobile : BaseEntityMobile
    {
        #region Properties
        public virtual AnalysisMobile Analysis { get; protected set; }
        public int AnalysisId { get; protected set; }
        public EnmType Type { get; protected set; }
        public string Observation { get; protected set; }
        public bool QuebrouXicara { get; protected set; }
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
}
