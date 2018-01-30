using System;
using System.Collections.Generic;

namespace CoffeeExperience.Domain.Entities
{
    public class Lot : BaseEntity
    {
        #region Properties
        public string Code { get; protected set; }
        public EnmLotStatus LotStatus { get; protected set; }
        public double Weight { get; protected set; }
        public DateTime Vality { get; protected set; }
        public virtual User User { get; protected set; } //mapeamento lazy loading herança
        public int? UserId { get; protected set; }
        public virtual ICollection<Analysis> ListAnalysis { get; protected set; }
        public virtual ICollection<Product> ListProduct { get; protected set; }
        #endregion

        #region Methods
        public void CalculateWeight()
        {
            if (this.ListProduct == null)
            {
                Weight = 0;
            }
            else if (this.ListProduct.Count == 0)
            {
                Weight = 0;
            }
            else
            {
                double CountWeight = 0;
                foreach (Product product in this.ListProduct)
                {
                    CountWeight += product.Weight;
                }
                this.Weight = CountWeight;
            }
        }
        public void Delete()
        {
            this.Status = EnmStatus.Deleted;
        }


        public void setLotStatus(EnmLotStatus status)
        {
            this.LotStatus = status;
        }

        public void SetUser(int userId)
        {
            UserId = userId;
        }

        public void ValidateProducts()
        {

            
            ICollection<Product> ListProductToRemove = new List<Product>();

            if (ListProduct != null)
            {
                foreach (var item in ListProduct)
                {
                    if (string.IsNullOrEmpty(item.Name) || item.Weight == 0)
                    {
                        ListProductToRemove.Add(item);
                    }
                }

                foreach (var item in ListProductToRemove)
                {
                    ListProduct.Remove(item);
                }
            }
        }
        #endregion
    }
    public enum EnmLotStatus
    {
        Waiting,
        InAnalysis,
        AnalysisFinished,
        Sold
    }

    

}
