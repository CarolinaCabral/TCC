using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;

namespace CoffeeExperience.Data.Repositories
{
    public class LaboratoryRepository : GenericRepository<Laboratory>, ILaboratoryRepository
    {
        public LaboratoryRepository(IGetContext getContext) : base(getContext)
        {

        }
    }
}
