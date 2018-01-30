using System;

namespace CoffeeExperience.Domain.Entities
{
    public class Product : BaseEntity
    {
        #region Properties
        public string Name { get; protected set; }
        
        public double Weight { get; protected set; }
        public virtual Lot Lot { get; protected set; }
        public int LotId { get; protected set; }
        #endregion
        #region Methods
        public void Delete()
        {
            this.Status = EnmStatus.Deleted;
        }
        #endregion
    }
}
