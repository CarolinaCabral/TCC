using CoffeeExperience.Domain.Portable.Entities;
using System;
using System.Linq;
using Xamarin.Forms;

namespace CoffeeExperience.App.Pages
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage(UserMobile user)
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            Title = "Menu";

            if (user != null)
            {
                LabelUserEmail.Text = user.Email;
                LabelUserLogin.Text = user.CPF;
                LabelUserName.Text = user.Name;
            }

        }

        private void BtnLot_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LotPage());
        }
        private void BtnAnalysis_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AnalysisPage(null,null));
        }
        private void BtnLaboratory_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LaboratoryPage());
        }

        private void BtnFollowLot_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FollowingLotPage());
        }


        void OnTapGestureRecognizerTappedExit(object sender, EventArgs args)
        {
            RedirectAndCleanNavigation(new LoginPage());
        }

        protected async void RedirectAndCleanNavigation(Page Page)
        {
            Navigation.PushAsync(Page);
            foreach (var page in Navigation.NavigationStack.ToList())
            {
                Navigation.RemovePage(page);
            }
        }

    }
}
