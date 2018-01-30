using CoffeeExperience.Domain.Portable.Entities;
using CoffeExperience.Domain.Portable;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.App.WebService
{
    public class UserService
    {
        public UserService()
        {

        }

        public async Task<UserMobile> Login(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                return await Task.FromResult(new UserMobile(false, "Usuário e/ou senha inválidos"));
            }
            else
            {
                using (var client = new HttpClient())
                {
                    UserMobile user = new UserMobile(Email, Password);
                    var param = JsonConvert.SerializeObject(user);
                    HttpContent contentPost = new StringContent(param, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(string.Format(Constants.WebServiceEndPoint + Constants.UserEndPoint + "login"), contentPost).Result;
                    var json = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        UserMobile userResponse = JsonConvert.DeserializeObject<UserMobile>(json);
                        userResponse.Success = true;
                        return await Task.FromResult(userResponse);
                    }
                    else
                    {
                        MobileException exception = JsonConvert.DeserializeObject<MobileException>(json);
                        return await Task.FromResult(new UserMobile(false, exception.Message));
                    }
                }
            }
        }
    }
}
