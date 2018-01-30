using CoffeeExperience.Domain.Entities;

namespace CoffeeExperience.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User Validate(string Email, string Password);
    }
}
