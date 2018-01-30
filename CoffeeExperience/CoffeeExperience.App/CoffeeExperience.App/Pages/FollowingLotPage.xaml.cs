using Acr.BarCodes;
using CoffeeExperience.App.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CoffeeExperience.App.Pages
{
    public partial class FollowingLotPage : BasePage
    {
        public ObservableCollection<string> FollowingLots { get; set; } = new ObservableCollection<string>();
        public FollowingLotPage()
        {
            try
            {
                InitializeComponent();
                this.BaseLayout.Children.Add(GetOverlay());
                Title = "Lotes Seguidos";
                ListViewLots.ItemsSource = FollowingLots;
                GetFollowingLots();
            }
            catch (Exception e)
            {
                
            }
           
           
        }

        public void GetFollowingLots()
        {
            var lots = DependencyService.Get<IFCMInfosGetter>().getAllTopics();

            if (FollowingLots != null && FollowingLots.Count > 0)
            {
                FollowingLots.Clear();
            }

            foreach (var lot in lots)
            {
                //substring para retirar a parte do follow. que tem no tópico antes do código do lote.
                FollowingLots.Add(lot.Split('.')[1]);
            }

            ListViewLots.ItemsSource = new ObservableCollection<string>();
            ListViewLots.ItemsSource = FollowingLots;
        }

        private void BtnScan_Clicked(object sender, EventArgs e)
        {
           
            Launch_BarcodeSearch();
        }


        #region Scan Barcode

        public async Task Launch_BarcodeSearch()
        {
            var result = await BarCodes.Instance.Read();
            if (!result.Success)
            {
                await DisplayAlert("Desculpe ! Falha! ", "Não foi possível ler o código de barras!", "OK");
            }
            else
            {

                if (await Task.Factory.StartNew(() => DependencyService.Get<IFCMInfosGetter>().isSubscribedToTopic("follow." + result.Code)))
                {
                    await DisplayAlert("Alerta!", "Você já esta seguindo este lote", "Ok");
                }
                else
                {
                    await Task.Factory.StartNew(() => DependencyService.Get<IFCMInfosGetter>().subscribeTopic("follow." + result.Code));
                    GetFollowingLots();
                }
            }
        }

        #endregion

        async void OnTapGestureRecognizerTappedUnfollow(object sender, EventArgs args)
        {
            Image img = sender as Image;
            var recognizer = img.GestureRecognizers.FirstOrDefault() as TapGestureRecognizer;
            await Task.Factory.StartNew(() => DependencyService.Get<IFCMInfosGetter>().unsubscribeTopic("follow." + recognizer.CommandParameter));
            GetFollowingLots();
        }
    }
}