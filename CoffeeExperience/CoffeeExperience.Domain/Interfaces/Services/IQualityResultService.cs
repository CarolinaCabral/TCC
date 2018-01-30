using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.TO;
using System.Collections.Generic;

namespace CoffeeExperience.Domain.Interfaces.Services
{
    public interface IQualityResultService : IGenericService<QualityResult>
    {
        IList<QualityResult> GetByAnalysisId(int analysisId);

        Response Save(QualityResult qualityResult);

        Response Exclude(int QualityResultId);
    }
}
