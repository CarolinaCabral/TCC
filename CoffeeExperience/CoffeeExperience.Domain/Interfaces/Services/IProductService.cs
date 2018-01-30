using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.TO;
using System.Collections.Generic;

namespace CoffeeExperience.Domain.Interfaces.Services
{
    public interface IProductService : IGenericService<Product>
    {
        List<Product> GetAllByLot(string LotCode, bool isAsNoTracking);
        Response Save(Product product);
        Response Exclude(int productId);
    }

}
