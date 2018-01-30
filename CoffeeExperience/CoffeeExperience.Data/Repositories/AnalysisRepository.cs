using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;

namespace CoffeeExperience.Data.Repositories
{
    public class AnalysisRepository : GenericRepository<Analysis>, IAnalysisRepository
    {
        public AnalysisRepository(IGetContext getContext) : base(getContext)
        {

        }
    }
}
