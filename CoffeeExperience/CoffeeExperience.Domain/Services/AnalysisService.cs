using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;
using CoffeeExperience.Domain.Interfaces.Services;
using CoffeeExperience.Domain.TO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeExperience.Domain.Services
{
    public class AnalysisService : GenericService<Analysis>, IAnalysisService
    {
        private readonly IUnitOfWork unitOfWork;
        public AnalysisService(IUnitOfWork unitOfWork)
            : base(unitOfWork.AnalysisRepository)
        {
            this.unitOfWork = unitOfWork;
        }

        public ICollection<Analysis> GetAllByClient(int UserId)
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
                        List<Laboratory> laboratories = unitOfWork.LaboratoryRepository.GetMany(p => p.UserId == UserId && p.Status == EnmStatus.Enabled).ToList();
                        if (laboratories == null || laboratories.Count() == 0)
                        {
                            throw new Exception("Não existe laboratório cadastrado para esse usuário");
                        }


                        List<Analysis> analysisList = new List<Analysis>();
                        foreach (Laboratory item in laboratories)
                        {
                            analysisList.AddRange(item.ListAnalysis.Where(p => p.Status == EnmStatus.Enabled));
                        }

                        if (analysisList.Count() == 0)
                        {
                            throw new Exception("Não existe análise cadastrada para esse usuário");
                        }

                        return analysisList;
                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public ICollection<Analysis> GetAllForAnalysisPage(int UserId, int? LaboratoryId, int? LotId)
        {
            try
            {
                if (LaboratoryId != null)
                {
                    if (!unitOfWork.LaboratoryRepository.Any(LaboratoryId.Value))
                    {
                        throw new Exception("Laborátorio não encontrado");
                    }

                    return unitOfWork.LaboratoryRepository.GetById(LaboratoryId.Value).ListAnalysis.Where(p => p.Status == EnmStatus.Enabled).ToList();
                }

                if(LotId != null)
                {
                    if (!unitOfWork.LotRepository.Any(LotId.Value))
                    {
                        throw new Exception("Lote não encontrado");
                    }
                    return unitOfWork.LotRepository.GetById(LotId.Value).ListAnalysis.Where(p => p.Status == EnmStatus.Enabled).ToList();
                }
                return GetAllByClient(UserId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public Response Save(Analysis analysis)
        {
            Analysis AnalysisExists = unitOfWork.AnalysisRepository.Get(p => p.Code == analysis.Code && p.Id != analysis.Id && p.Status == EnmStatus.Enabled);

            if (analysis.Id > 0)
            {

                if (AnalysisExists == null)
                {
                    Update(analysis);
                }
                else
                {
                    return new Response(true, "Análise com este código já cadastrada");
                }

            }
            else
            {
                if (AnalysisExists == null)
                {
                    unitOfWork.AnalysisRepository.Add(analysis);

                    string LotCode = unitOfWork.LotRepository.GetById(analysis.LotId).Code;

                    PushNotificationService pushNotificationService = new PushNotificationService();

                    pushNotificationService.SendNotification("Análise cadastrada","O Lote [" + LotCode + "] recebeu uma anáise, por enquanto ainda sem resultado de qualidade no dia: " + DateTime.Now.ToString("dd/MM/yyyy"), "follow." + LotCode );

                }
                else
                {
                    return new Response(true, "Análise já cadastrada");
                }

            }
            unitOfWork.Commit();
            return new Response(false);
        }

        public Analysis GetByCode(string Code)
        {
            try
            {
                Analysis analysis = unitOfWork.AnalysisRepository.GetWithIncludes(p => p.Code == Code, v => v.Laboratory, c => c.Lot, d => d.ListQualityResult);

                if(analysis == null)
                {
                    throw new Exception("Análise não encontrada!");
                }

                return analysis;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Response Exclude(int AnalysisId)
        {
            try
            {
                Analysis analysis = unitOfWork.AnalysisRepository.Get(x => x.Id.Equals(AnalysisId));

                if (analysis == null)
                {
                    return new Response(false, "Resulado de qualidade não encontrado!");
                }
                else
                {

                    if (analysis.Status != EnmStatus.Deleted)
                    {
                        analysis.Delete();
                        foreach (var qualityResult in analysis.ListQualityResult)
                        {
                            qualityResult.Delete();
                        }

                        Update(analysis);
                        unitOfWork.Commit();
                        return new Response(true);
                    }
                    else
                    {
                        return new Response(false, "Este resultado de qualidade já foi excluído!");
                    }
                }

            }
            catch (Exception e)
            {

                return new Response(false, e.Message);
            }
        }



    }
}
