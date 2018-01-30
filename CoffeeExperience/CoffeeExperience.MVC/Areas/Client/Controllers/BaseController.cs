using CoffeeExperience.Data.Context;
using CoffeeExperience.Domain.Interfaces.Repositories;
using CoffeeExperience.Domain.Services;
using System.Web.Mvc;

namespace CoffeeExperience.MVC.Areas.Client.Controllers
{
    public class BaseController : Controller
    {
        #region Constructor
        public ServiceManager services;
        public BaseController()
        {
            UnitOfWork unit = new UnitOfWork();
            services = new ServiceManager(unit);
        }
        #endregion

        #region Properties
        protected string _Success;
        public string SuccessMessage
        {
            get
            {
                return _Success;
            }
            set
            {
                _Success = value;
                TempData["SuccessMessage"] = value; //Onde é guardado no servidor as mensagens de sucesso e erro para serem exibidas com ajax quando se retorna uma partial view.
            }
        }

        protected string _Error;
        public string ErrorMessage
        {
            get
            {
                return _Error;
            }
            set
            {
                _Error = value;
                TempData["ErrorMessage"] = value;
            }
        }
        #endregion

        #region Methods
        public void ClearMessages()
        {
            if(!string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = "";
                TempData.Remove("ErrorMessage");
            }

            if (!string.IsNullOrEmpty(SuccessMessage))
            {
                SuccessMessage = "";
                TempData.Remove("SuccessMessage");
            }
           
            
        }
        #endregion




    }
}