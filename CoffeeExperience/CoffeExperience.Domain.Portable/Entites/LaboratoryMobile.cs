using System.Collections.Generic;

namespace CoffeeExperience.Domain.Portable.Entities
{
    
    public class LaboratoryMobile : BaseEntityMobile
    {
        #region Constructor

        public LaboratoryMobile()
        {

        }
        public LaboratoryMobile(bool success, string details) : base(success, details)
        {

        }
        #endregion
        #region Properties
        public string Name { get;  set; }
        public string City { get;  set; }
        public string CNPJ { get;  set; }
        public virtual UserMobile User { get;  set; }
        public int? UserId { get;  set; }
        public virtual ICollection<AnalysisMobile> ListAnalysis { get;  set; }
        public string Code { get;  set; }
        #endregion
        #region Methods
        public void Delete()
        {
            this.Status = EnmStatus.Deleted;
        }

        public void SetUser(int userId)
        {
            UserId = userId;
        }
        #endregion
    }
}

