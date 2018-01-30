using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeExperience.Domain.Portable;
using CoffeeExperience.Domain.Portable.Entities;
using CoffeeExperience.App.WebService.Interfaces;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace CoffeeExperience.App.WebService
{
    public class AnalysisService : IAnalysisService
    {
        public async Task<AnalysisMobile> GetByAnalysisCode(string AnalysisCode)
        {
            try
            {
                if (string.IsNullOrEmpty(AnalysisCode))
                {
                    return await Task.FromResult(new AnalysisMobile(false, "Análise inválido"));
                }
                else
                {
                    using (var client = new HttpClient())
                    {

                        var response = client.GetAsync(string.Format(Constants.WebServiceEndPoint + Constants.AnalysisEndPoint + "getbyanalysiscode/" + AnalysisCode)).Result;
                        var json = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode.Equals(HttpStatusCode.OK))
                        {
                            AnalysisMobile analysisResponse = JsonConvert.DeserializeObject<AnalysisMobile>(json);
                            analysisResponse.Success = true;
                            return await Task.FromResult(analysisResponse);
                        }
                        else
                        {
                            MobileException exception = JsonConvert.DeserializeObject<MobileException>(json);
                            return await Task.FromResult(new AnalysisMobile(false, exception.Message));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
