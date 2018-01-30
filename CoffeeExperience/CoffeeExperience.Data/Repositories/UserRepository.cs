using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;

namespace CoffeeExperience.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IGetContext getContext) : base(getContext)
        {
        }

        public User Validate(string email, string password)
        {
            setLazyLoading(false);
            var user = Get(x => x.Email == email && x.Password == password);
            return user;
        }
    }
}
