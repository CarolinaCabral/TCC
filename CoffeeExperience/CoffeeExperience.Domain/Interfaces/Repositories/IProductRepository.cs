using CoffeeExperience.Domain.Entities;

namespace CoffeeExperience.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //object Update { get; set; }
    }
}
