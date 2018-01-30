using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;
using CoffeeExperience.Domain.Interfaces.Services;
using CoffeeExperience.Domain.TO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeExperience.Domain.Services
{
    public class LotService : GenericService<Lot>, ILotService
    {
        private readonly IUnitOfWork unitOfWork;
        ServiceManager services;
        public LotService(IUnitOfWork unitOfWork)
            : base(unitOfWork.LotRepository)
        {
            this.unitOfWork = unitOfWork;
            services = new ServiceManager(unitOfWork);
        }

        public IEnumerable<Lot> GetAllForAnalysis()
        {
            try
            {
                IList<Lot> lots = unitOfWork.LotRepository.GetMany(p => p.Status == EnmStatus.Enabled).ToList();
                if (lots == null || lots.Count() == 0)
                {
                    throw new Exception("Não há lotes cadastrados");
                }


                return lots;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Lot> GetAllByClient(int UserId)
        {
            try
            {
                if (UserId <= 0)
                {
                    throw new Exception("Usuário inexistente");
                }
                else
                {
                    User user = unitOfWork.UserRepository.GetById(UserId);
                    if (user == null)
                    {
                        throw new Exception("Usuário não encontrado");
                    }
                    else
                    {
                        IEnumerable<Lot> lot = unitOfWork.LotRepository.GetMany(p => p.UserId == UserId && p.Status == EnmStatus.Enabled);
                        if (lot == null || lot.Count() == 0)
                        {
                            throw new Exception("Não existe lote cadastrado para esse usuário");
                        }
                        return lot;
                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Lot GetByCode(string LotCode)
        {
            try
            {
                if (string.IsNullOrEmpty(LotCode))
                {
                    throw new Exception("Código inválido!");
                }
                else
                {
                    string _LotCode = LotCode.Trim();
                    Lot lot = unitOfWork.LotRepository.GetByCode(_LotCode);
                    if (lot == null)
                    {
                        throw new Exception("Lote não encontrado!");
                    }
                    else
                    {
                        return lot;
                    }
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Response Save(Lot lot)
        {
            lot.CalculateWeight();
            lot.ValidateProducts();

            if (lot.Id > 0)
            {

                Lot LotExists = unitOfWork.LotRepository.Get(p => p.Code == lot.Code && p.Id != lot.Id && p.Status == EnmStatus.Enabled);

                if (LotExists == null)
                {
                    UpdateLot(lot);
                }

                else
                {
                    return new Response(true, "Lote já cadastrado!");
                }

            }
            else
            {
                Lot LotExists = unitOfWork.LotRepository.Get(p => p.Code == lot.Code && p.Status == EnmStatus.Enabled);

                if (LotExists == null)
                {
                    lot.setLotStatus(EnmLotStatus.Waiting);
                    unitOfWork.LotRepository.Add(lot);

                    if (lot.Vality < DateTime.Now)
                    {
                        return new Response(true, "Verificar a data de vencimento do lote");
                    }
                }
                else
                {
                    return new Response(true, "Lote já cadastrado");
                }

            }
            unitOfWork.Commit();
            return new Response(false);
        }

        public Response Exclude(int LotId)
        {
            try
            {
                Lot Lot = unitOfWork.LotRepository.Get(x => x.Id.Equals(LotId));

                if (Lot == null)
                {
                    return new Response(false, "Lote não encontrado!");
                }
                else
                {

                    if (Lot.Status != EnmStatus.Deleted)
                    {

                        Lot.Delete();
                        Update(Lot);
                        unitOfWork.Commit();
                        return new Response(true);
                    }
                    else
                    {
                        return new Response(false, "Este lote já foi excluído!");
                    }
                }

            }
            catch (Exception e)
            {

                return new Response(false, e.Message);
            }
        }

        public void UpdateLot(Lot lot)
        {
            //lógica para verificação dos filhos do lot, no caso produto, se será atualizado, ou adicionado.
            
            if (lot.ListProduct != null && lot.ListProduct.Count > 0)
            {
                foreach (var item in lot.ListProduct)
                {
                    services.ProductService.Save(item);
                }
            }

            Update(lot);
        }
    }
}
