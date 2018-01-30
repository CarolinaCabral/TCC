using CoffeeExperience.App.WebService.Interfaces;
using CoffeeExperience.Domain.Portable.Entities;
using CoffeExperience.Domain.Portable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.App.WebService
{
    public class LaboratoryService : ILaboratoryService
    {
        public async Task<LaboratoryMobile> GetByLaboratoryCode(string LaboratoryCode)
        {
            try
            {
                if (string.IsNullOrEmpty(LaboratoryCode))
                {
                    return await Task.FromResult(new LaboratoryMobile(false, "Laboratório inválido"));
                }
                else
                {
                    using (var client = new HttpClient())
                    {

                        var response = client.GetAsync(string.Format(Constants.WebServiceEndPoint + Constants.LaboratoryEndPoint + "getbylaboratorycode/" + LaboratoryCode)).Result;
                        var json = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode.Equals(HttpStatusCode.OK))
                        {
                            LaboratoryMobile laboratoryResponse = JsonConvert.DeserializeObject<LaboratoryMobile>(json);
                            laboratoryResponse.Success = true;
                            return await Task.FromResult(laboratoryResponse);
                        }
                        else
                        {
                            MobileException exception = JsonConvert.DeserializeObject<MobileException>(json);
                            return await Task.FromResult(new LaboratoryMobile(false, exception.Message));
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

