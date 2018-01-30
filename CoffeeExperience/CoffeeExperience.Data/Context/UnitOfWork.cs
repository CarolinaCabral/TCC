using CoffeeExperience.Data.Repositories;
using CoffeeExperience.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.Data.Context
{
   public  class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly IGetContext getContext;
        public UnitOfWork()
        {
            if (getContext == null)
                this.getContext = new GetContext();
        }

        private IUserRepository userRepository;
        private ILotRepository lotRepository;
        private ILaboratoryRepository laboratoryRepository;
        private IAnalysisRepository analysisRepository;
        private IProductRepository productRepository;
        private IQualityResultRepository qualityResultRepository;

        public ILotRepository LotRepository
        {
            get
            {
                //if (this.lotRepository == null)
                    this.lotRepository = new LotRepository(this.getContext);
                return lotRepository;
            }
        }

        public ILaboratoryRepository LaboratoryRepository
        {
            get
            {
                if (this.laboratoryRepository == null)
                    this.laboratoryRepository = new LaboratoryRepository(this.getContext);
                return laboratoryRepository;
            }
        }

        public IAnalysisRepository AnalysisRepository
        {
            get
            {
                if (this.analysisRepository == null)
                    this.analysisRepository = new AnalysisRepository(this.getContext);
                return analysisRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                //if (this.productRepository == null)
                    this.productRepository = new ProductRepository(this.getContext);
                return productRepository;
            }
        }

        public IQualityResultRepository QualityResultRepository
        {
            get
            {
                if (this.qualityResultRepository == null)
                    this.qualityResultRepository = new QualityResultRepository(this.getContext);
                return qualityResultRepository;
            }

        }
        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new UserRepository(this.getContext);
                return userRepository;
            }
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.getContext.Get().Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            try
            {
                this.getContext.Get().SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                StringBuilder str = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    string Msg = "Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:\n";

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Msg = Msg + "- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage + "\n";
                    }
                    str.Append(Msg);
                }
                try
                {
                    //EmailSender.EnviarEmailTecnico("[ChamadosECT Produção] Exceção no sistema", "primeinterwaytickets@gmail.com", "DbEntityValidationException", str.ToString(), false);
                }
                catch (Exception ex)
                {

                }
                throw;
            }
        }
    }
}
