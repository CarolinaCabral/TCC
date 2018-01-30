using System;
using System.Collections.Generic;

namespace CoffeeExperience.Domain.Portable.Entities
{
    public class UserMobile : BaseEntityMobile
    {
        public UserMobile()
        {

        }
        public UserMobile(string _Name, string _Email, string _Password, string _CPF)
        {
            Name = _Name;
            Email = _Email;
            Password = _Password;
            CPF = _CPF;
        }

        public UserMobile (string _Email, string _Password)
        {
            Email = _Email;
            Password = _Password;
        }

        public UserMobile(bool success, string details) : base(success,details)
        {

        }


        #region Properties

        #endregion
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FacebookId { get; set; }
        public string Token { get; set; }
        public string CPF { get; set; }
        public DateTime? LastAccess { get; set; }
        public virtual ICollection<LotMobile> ListLots { get; set; }
        public virtual ICollection<LaboratoryMobile> ListLaboratory { get; set; }

        #region Methods
        public void SetLastAccess()
        {
            LastAccess = DateTime.Now;
        }

        #endregion
    }
}
