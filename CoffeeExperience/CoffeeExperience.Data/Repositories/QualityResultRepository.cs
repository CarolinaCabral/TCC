using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;

namespace CoffeeExperience.Data.Repositories
{
    public class QualityResultRepository : GenericRepository<QualityResult>, IQualityResultRepository
    {
        public QualityResultRepository(IGetContext getContext) : base(getContext)
        {

        }
    }
}
