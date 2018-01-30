using System;

namespace CoffeeExperience.Domain.Entities
{
    public class QualityResult : BaseEntity
    {
        #region Properties
        public virtual Analysis Analysis { get; protected set; }
        public int AnalysisId { get; protected set; }
        public EnmType Type { get; protected set; }
        public string Observation { get; protected set; }
        public bool QuebrouXicara { get; protected set; }
        #endregion

        #region Methods
        public void Delete()
        {
            this.Status = EnmStatus.Deleted;
        }
        #endregion


    }
}
