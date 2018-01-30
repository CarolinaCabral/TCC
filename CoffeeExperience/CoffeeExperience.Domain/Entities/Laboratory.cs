using System.Collections.Generic;

namespace CoffeeExperience.Domain.Entities
{

    public class Laboratory : BaseEntity
    {
        #region Properties
        public string Name { get; protected set; }
        public string City { get; protected set; }
        public string CNPJ { get; protected set; }
        public virtual User User { get; protected set; }
        public int? UserId { get; protected set; }
        public virtual ICollection<Analysis> ListAnalysis { get; protected set; }
        public string Code { get; protected set; }
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
