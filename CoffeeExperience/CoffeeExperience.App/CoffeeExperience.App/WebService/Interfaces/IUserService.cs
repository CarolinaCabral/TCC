using CoffeeExperience.Domain.Portable.Entities;
using System;

namespace CoffeeExperience.App.WebService.Interfaces
{
    public interface IUserService
    {
        void Login(string Email, string Password, Action<UserMobile> UserHandler);
    }
}
