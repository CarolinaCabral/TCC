using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;
using CoffeeExperience.Domain.Interfaces.Services;
using CoffeeExperience.Domain.TO;
using CoffeeExperience.Infra.CrossCutting.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeExperience.Domain.Services
{
    public class QualityResultService : GenericService<QualityResult>, IQualityResultService
    {
        private readonly IUnitOfWork unitOfWork;
        public QualityResultService(IUnitOfWork unitOfWork)
            : base(unitOfWork.QualityResultRepository)
        {
            this.unitOfWork = unitOfWork;
        }

        public IList<QualityResult> GetByAnalysisId(int analysisId)
        {
            if(analysisId == 0 || !unitOfWork.AnalysisRepository.Any(analysisId))
            {
                throw new Exception("Análise não encontrada!");
            }

            if(unitOfWork.QualityResultRepository.Count(p => p.AnalysisId == analysisId && p.Status == EnmStatus.Enabled) <= 0)
            {
                throw new Exception("Nenhum resultado de qualidade encontrado para esta análise");
            }

            return unitOfWork.QualityResultRepository.GetMany(p => p.AnalysisId == analysisId && p.Status == EnmStatus.Enabled).ToList();
        }

        public Response Save(QualityResult qualityResult)
        {
            QualityResult QualityResultExists = unitOfWork.QualityResultRepository.Get(p => p.Observation == qualityResult.Observation && p.Type == qualityResult.Type && p.AnalysisId == qualityResult.AnalysisId && p.Id != qualityResult.Id && p.Status == EnmStatus.Enabled);

            if (qualityResult.Id > 0)
            {

                if (QualityResultExists == null)
                {
                    Update(qualityResult);
                }
                else
                {
                    return new Response(true, "Resultado de qualidade já cadastrado para essa análise");
                }

            }
            else
            {
                if (QualityResultExists == null)
                {
                    unitOfWork.QualityResultRepository.Add(qualityResult);
                }
                else
                {
                    return new Response(true, "Resultado de qualidade já cadastrado para essa análise");
                }

            }

            string LotCode = unitOfWork.LotRepository.GetById(unitOfWork.AnalysisRepository.GetById(qualityResult.AnalysisId).LotId).Code;

            PushNotificationService pushNotificationService = new PushNotificationService();

            pushNotificationService.SendNotification("Resultado de qualidade", "O Lote [" + LotCode + "] recebeu um resultado de qualidade, da análise: " + qualityResult.AnalysisId + ", o resultado foi: " + EnumHelper.GetDescription(qualityResult.Type), "follow."+LotCode);

            unitOfWork.Commit();
            return new Response(false);
        }

        public Response Exclude(int QualityResultId)
        {
            try
            {
                QualityResult qualityResult = unitOfWork.QualityResultRepository.Get(x => x.Id.Equals(QualityResultId));

                if (qualityResult == null)
                {
                    return new Response(false, "Resulado de qualidade não encontrado!");
                }
                else
                {

                    if (qualityResult.Status != EnmStatus.Deleted)
                    {

                        qualityResult.Delete();
                        Update(qualityResult);
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
