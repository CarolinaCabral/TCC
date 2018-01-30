using CoffeeExperience.Domain.Portable.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.App.WebService.Interfaces
{
    interface IAnalysisService
    {
        Task<AnalysisMobile> GetByAnalysisCode(string LotCode);
    }
}
