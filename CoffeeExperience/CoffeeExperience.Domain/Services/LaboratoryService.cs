using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;
using CoffeeExperience.Domain.Interfaces.Services;
using CoffeeExperience.Domain.TO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeExperience.Domain.Services
{
    public class LaboratoryService : GenericService<Laboratory>, ILaboratoryService
    {
        private readonly IUnitOfWork unitOfWork;
        public LaboratoryService(IUnitOfWork unitOfWork)
            : base(unitOfWork.LaboratoryRepository)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Laboratory> GetAllByClient(int UserId)
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
                        IEnumerable<Laboratory> laboratories = unitOfWork.LaboratoryRepository.GetMany(p => p.UserId == UserId && p.Status == EnmStatus.Enabled);
                        if (laboratories == null || laboratories.Count() == 0)
                        {
                            throw new Exception("Não existe laboratório cadastrado para esse usuário");
                        }
                        return laboratories;
                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Response Save(Laboratory laboratory)
        {
            if (laboratory.Id > 0)
            {
                Laboratory LaboratoryExists = unitOfWork.LaboratoryRepository.Get(p => p.CNPJ == laboratory.CNPJ && p.Id != laboratory.Id && p.Status == EnmStatus.Enabled); //checando cnpj

                if (LaboratoryExists == null)
                {
                    Update(laboratory);
                }
                else
                {
                    return new Response(true, "Laboratório já cadastrado");
                }
                
            }
            else
            {
                Laboratory LaboratoryExists = unitOfWork.LaboratoryRepository.Get(p => p.CNPJ == laboratory.CNPJ && p.Status == EnmStatus.Enabled); //checando cnpj

                if (LaboratoryExists == null)
                {
                    unitOfWork.LaboratoryRepository.Add(laboratory);
                }
                else
                {
                    return new Response(true, "Laboratório já cadastrado");
                }

            }
            unitOfWork.Commit();
            return new Response(false);
        }

        public Response Exclude(int LaboratoryId)
        {
            try
            {
                Laboratory laboratory = unitOfWork.LaboratoryRepository.Get(x => x.Id.Equals(LaboratoryId));

                if (laboratory == null)
                {
                    return new Response(false, "Laboratório não encontrado!");
                }
                else
                {
                    
                    if (laboratory.Status != EnmStatus.Deleted)
                    {
                        
                        laboratory.Delete();
                        Update(laboratory);
                        unitOfWork.Commit();
                        return new Response(true);
                    }
                    else
                    {
                        return new Response(false, "Este laboratório já foi excluído!");
                    }
                }

            }
            catch (Exception e)
            {

                return new Response(false, e.Message);
            }
        }

        public Laboratory GetByCode(string code)
        {
            try
            {
                Laboratory laboratory = unitOfWork.LaboratoryRepository.Get(p => p.Code == code);

                if(laboratory == null)
                {
                    throw new Exception("Laboratório não encontrado");
                }

                return laboratory;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


    }
}
