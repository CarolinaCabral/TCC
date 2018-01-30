using CoffeeExperience.Infra.CrossCutting.System;
using System;
using System.Collections.Generic;

namespace CoffeeExperience.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {

        }
        public User(string _Name,string _Email, string _Password, string _CPF)
        {
            Name = _Name;
            Email = _Email;
            Password = _Password;
            CPF = _CPF;
        }
        #region Properties

        #endregion
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string FacebookId { get; protected set; }
        public string Token { get; protected set; }
        public string CPF { get; protected set; }
        public DateTime? LastAccess { get; protected set; }
        public virtual ICollection<Lot> ListLots { get; protected set; }
        public virtual ICollection<Laboratory> ListLaboratory { get; protected set; }

        #region Methods
        public void SetLastAccess()
        {
            LastAccess = DateTime.Now;
        }

        public void setPassword(string newPassword)
        {
            Password = newPassword.ToCriptografa();
        }

        public void CripgraphyPassword()
        {
            Password = Password.ToCriptografa();
        }
        #endregion
    }
}
