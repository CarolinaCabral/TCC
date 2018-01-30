using CoffeeExperience.App.WebService;
using CoffeeExperience.Domain.Portable.Entities;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoffeeExperience.App.Pages
{
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BaseLayout.Children.Add(GetOverlay(), () => new Rectangle(0, 0, BaseLayout.Width, BaseLayout.Height)); //Configuração de quando se herda da BasePage no xaml para que o loading funcione
        }

        private async void BtnLogin_Clicked(object sender, System.EventArgs e)
        {
            IsLoading = true;
            try
            {
                UserService userService = new UserService(); //Cada tipo de serviço que vamos consumir na webApi terá seu próprio service, nesse caso o serviço de usuário

                var userTask = await Task.Factory.StartNew(() => userService.Login(EntryEmail.Text, EntryPassword.Text)); //Cria uma nova task, para que o Loading fique rodando na página enquanto essa nova task busca as informações na webapi
                //var userTask = await Task.Factory.StartNew(() => userService.Login("luiz.lucas@gmail.com", "123456"));

                UserMobile userMobile = userTask.Result;

                IsLoading = false;
                if (userMobile.Success)
                {
                    Navigation.PushAsync(new MenuPage(userMobile));
                }
                else
                {
                    DisplayAlert("Erro", userMobile.Details, "Ok");
                }

            }
            catch (Exception ex)
            {
                IsLoading = false;
                DisplayAlert("Erro", ex.Message, "OK");
            }

        }        
    }
}