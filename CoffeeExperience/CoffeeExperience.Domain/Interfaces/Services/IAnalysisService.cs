using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.TO;
using System.Collections.Generic;

namespace CoffeeExperience.Domain.Interfaces.Services
{
    public interface IAnalysisService : IGenericService<Analysis>
    {
        ICollection<Analysis> GetAllByClient(int UserId);
        Response Save(Analysis analysis);
        ICollection<Analysis> GetAllForAnalysisPage(int UserId, int? LaboratoryId, int? LotId);
        Analysis GetByCode(string Code);

        Response Exclude(int AnalysisId);
    }
}
