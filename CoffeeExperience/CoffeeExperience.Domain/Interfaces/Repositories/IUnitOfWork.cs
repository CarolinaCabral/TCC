namespace CoffeeExperience.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        
        IUserRepository UserRepository { get; }
        ILotRepository LotRepository { get; }
        ILaboratoryRepository LaboratoryRepository { get; }
        IAnalysisRepository AnalysisRepository { get;}
        IProductRepository ProductRepository { get; }
        IQualityResultRepository QualityResultRepository { get; }
        void Commit();
        void Dispose();
    }
}
