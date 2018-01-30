using CoffeeExperience.Domain.Entities;

namespace CoffeeExperience.Domain.Interfaces.Repositories
{
    public interface ILotRepository : IGenericRepository<Lot>
    {
        Lot GetByCode(string LotCode);
        Lot GetByCodeAsNoTracking(string LotCode);
    }
}
