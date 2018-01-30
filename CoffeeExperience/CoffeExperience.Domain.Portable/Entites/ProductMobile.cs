using System;

namespace CoffeeExperience.Domain.Portable.Entities
{
    public class ProductMobile : BaseEntityMobile
    {
        #region Constructor
        public ProductMobile()
        {

        }
        public ProductMobile(bool success, string details) : base(success, details)
        {

        }
        #endregion

        #region Properties
        public string Name { get;  set; }
        
        public double Weight { get;  set; }
        public virtual LotMobile Lot { get;  set; }
        public int LotId { get;  set; }
        #endregion
        #region Methods
        public void Delete()
        {
            this.Status = EnmStatus.Deleted;
        }
        #endregion
    }
}
