using AutoMapper;
using CoffeeExperience.Domain.Entities;
using CoffeeExperience.MVC.ViewModels;

namespace CoffeeExperience.MVC.AutoMapper
{
    public class MapConfig
    {
        public MapConfig()
        {

        }
        public MapperConfiguration ToViewModel()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<BaseEntity, BaseEntityViewModel>().MaxDepth(3);
                cfg.CreateMap<User, UserViewModel>().MaxDepth(3);
                cfg.CreateMap<Lot, LotViewModel>().MaxDepth(3);
                cfg.CreateMap<Laboratory, LaboratoryViewModel>().MaxDepth(3);
                cfg.CreateMap<Analysis, AnalysisViewModel>().MaxDepth(3);
                cfg.CreateMap<Product, ProductViewModel>().MaxDepth(3);
                cfg.CreateMap<QualityResult, QualityResultViewModel>().MaxDepth(3);

            });

            return config;
        }

        public MapperConfiguration ToDomain()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<BaseEntityViewModel, BaseEntity>().MaxDepth(3);
                cfg.CreateMap<UserViewModel, User>().MaxDepth(3);
                cfg.CreateMap<LotViewModel, Lot>().MaxDepth(3);
                cfg.CreateMap<LaboratoryViewModel, Laboratory>().MaxDepth(3);
                cfg.CreateMap<AnalysisViewModel, Analysis>().MaxDepth(3);
                cfg.CreateMap<ProductViewModel, Product>().MaxDepth(3);
                cfg.CreateMap<QualityResultViewModel, QualityResult>().MaxDepth(3);

            });

            return config;
        }

    }

}