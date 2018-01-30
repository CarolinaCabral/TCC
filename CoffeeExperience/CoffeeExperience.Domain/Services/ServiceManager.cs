using CoffeeExperience.Domain.Interfaces.Repositories;
using CoffeeExperience.Domain.Interfaces.Services;

namespace CoffeeExperience.Domain.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork unitOfWork;
        public ServiceManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUserService userService;
        private ILotService lotService;
        private ILaboratoryService laboratoryService;
        private IAnalysisService analysisService;
        private IProductService productService;
        private IQualityResultService qualityResultService;

        public IUserService UserService
        {
            get
            {
                if (this.userService == null)
                    this.userService = new UserService(unitOfWork);
                return userService;
            }
        }
        public ILotService LotService
        {
            get
            {
                if (this.lotService == null)
                    this.lotService = new LotService(unitOfWork);
                return lotService;
            }
        }
        public ILaboratoryService LaboratoryService
        {
            get
            {
                if (this.laboratoryService == null)
                    this.laboratoryService = new LaboratoryService(unitOfWork);
                return laboratoryService;
            }
        }
        public IAnalysisService AnalysisService
        {
            get
            {
                if (this.analysisService == null)
                    this.analysisService = new AnalysisService(unitOfWork);
                return analysisService;
            }
        }
        public IProductService ProductService
        {
            get
            {
                if (this.productService == null)
                    this.productService = new ProductService(unitOfWork);
                return productService;
            }
        }
        public IQualityResultService QualityResultService
        {
            get
            {
                if (this.qualityResultService == null)
                    this.qualityResultService = new QualityResultService(unitOfWork);
                return qualityResultService;
            }

        }
    }
}
