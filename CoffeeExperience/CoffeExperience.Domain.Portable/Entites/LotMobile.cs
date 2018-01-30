using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoffeeExperience.Domain.Portable.Entities
{
    public class LotMobile : BaseEntityMobile
    {
        #region Constructor
        public LotMobile()
        {

        }
        public LotMobile(bool success, string details) : base(success, details)
        {

        }
        #endregion

        #region Properties
        public string Code { get;  set; }
        public EnmLotStatus LotStatus { get;  set; }
        public double Weight { get; set; }
        public DateTime Vality { get; set; }
        public virtual UserMobile User { get;  set; } //mapeamento lazy loading herança
        public int? UserId { get;  set; }
        public ObservableCollection<AnalysisMobile> ListAnalysis { get;  set; }
        public ObservableCollection<ProductMobile> ListProduct { get;  set; }
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
                foreach (ProductMobile product in this.ListProduct)
                {
                    CountWeight += product.Weight;
                }
                this.Weight = CountWeight;
            }
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
            ICollection<ProductMobile> ListProductToRemove = new List<ProductMobile>();

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
