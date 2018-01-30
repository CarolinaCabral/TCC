using CoffeeExperience.App.WebService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeExperience.Domain.Portable.Entities;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using CoffeExperience.Domain.Portable;

namespace CoffeeExperience.App.WebService
{
    public class LotService : ILotService
    {
        public async Task<LotMobile> GetByLotCode(string LotCode)
        {
            try
            {
                if (string.IsNullOrEmpty(LotCode))
                {
                    return await Task.FromResult(new LotMobile(false, "Usuário e/ou senha inválidos"));
                }
                else
                {
                    using (var client = new HttpClient())
                    {

                        var response = client.GetAsync(string.Format(Constants.WebServiceEndPoint + Constants.LotEndPoint + "getbylotcode/" + LotCode)).Result;
                        var json = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode.Equals(HttpStatusCode.OK))
                        {
                            LotMobile lotResponse = JsonConvert.DeserializeObject<LotMobile>(json);
                            lotResponse.Success = true;
                            return await Task.FromResult(lotResponse);
                        }
                        else
                        {
                            MobileException exception = JsonConvert.DeserializeObject<MobileException>(json);
                            return await Task.FromResult(new LotMobile(false, exception.Message));
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
