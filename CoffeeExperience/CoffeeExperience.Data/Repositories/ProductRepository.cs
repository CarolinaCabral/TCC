using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;

namespace CoffeeExperience.Data.Repositories
{
    public class ProductRepository : GenericRepository <Product>, IProductRepository
    {
        public ProductRepository(IGetContext getContext) : base(getContext)
        {

        }
    }
}
