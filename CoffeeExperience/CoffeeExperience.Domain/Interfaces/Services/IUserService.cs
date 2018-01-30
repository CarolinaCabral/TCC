using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.TO;
using System.Collections.Generic;

namespace CoffeeExperience.Domain.Interfaces.Services
{
    public interface IUserService : IGenericService<User>
    {
        User Validate(string email, string password);
        Response Save(User user);
        void SetLastAccess(User user);
        bool RememberPassword(string email);
        
    }
}
