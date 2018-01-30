using CoffeeExperience.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.Domain.Interfaces.Services
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        ILotService LotService { get; }
        ILaboratoryService LaboratoryService { get; }
        IAnalysisService AnalysisService { get; }
        IProductService ProductService { get; }
        IQualityResultService QualityResultService { get; }


    }
}
