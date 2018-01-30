using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeExperience.App.Pages
{
    public partial class BasePage : ContentPage
    {
        bool _IsLoading;

        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }

            set
            {
                Overlay.IsVisible = value;
                LoadingIndicator.IsRunning = value;
                _IsLoading = value;
            }
        }

        //private void ToolbarItemHome_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new MenuPage(App.loggedUser));
        //}

        public BasePage()
        {
            InitializeComponent();
            Overlay.BackgroundColor = Constants.Colors.BackgroundOverlay;

            //ToolbarItem toolbarItemHome = new ToolbarItem { Icon = "home.png" };
            //toolbarItemHome.Clicked += ToolbarItemHome_Clicked;
            //ToolbarItems.Add(toolbarItemHome);
        }

        protected AbsoluteLayout GetOverlay()
        {
            return Overlay;
        }


        protected double GetWidth() { return BaseLayout.Width; }
    }
}
