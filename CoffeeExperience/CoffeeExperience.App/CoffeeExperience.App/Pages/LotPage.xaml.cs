using Acr.BarCodes;
using CoffeeExperience.App.Controls;
using CoffeeExperience.App.WebService;
using CoffeeExperience.Domain.Portable.Entities;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace CoffeeExperience.App.Pages
{
    public partial class LotPage : BasePage
    {
        public string LotCode { get; set; }
        public bool isSubscribed { get; set; }

        public ObservableCollection<AnalysisMobile> ListAnalysis { get; set; }
        public LotPage()
        {
   
            InitializeComponent();
            this.Title = "Lote";
            this.BaseLayout.Children.Add(GetOverlay());
            
        }

        private void BtnAnalysis_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListAnalysisPage(ListAnalysis, LotCode));
        }

        private void BtnSeguir_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LotCode))
            {
                DisplayAlert("Erro", "Sem lote", "Ok");
            }
            else
            {
                if (isSubscribed)
                {
                    DependencyService.Get<IFCMInfosGetter>().unsubscribeTopic("follow." + LotCode);
                    DisplayAlert("Sucesso","Você deixou de seguir","Ok");
                    isSubscribed = false;
                    btnSeguir.Text = "Seguir";
                }
                else
                {
                    DependencyService.Get<IFCMInfosGetter>().subscribeTopic("follow." + LotCode);
                    DisplayAlert("Sucesso", "Você começou seguir", "Ok");
                    isSubscribed = true;
                    btnSeguir.Text = "Deixar de seguir";
                }
            }
        }

        private void BtnScan_Clicked(object sender, EventArgs e)
        {
            Launch_BarcodeSearch();
        }


        #region Scan Barcode

        async void Launch_BarcodeSearch()
        {
            var result = await BarCodes.Instance.Read();
            if (!result.Success)
            {
                await this.DisplayAlert("Desculpe ! Falha! ", "Não foi possível ler o código de barras!", "OK");
            }
            else
            {               
                LotService lotService = new LotService();
                var lote = lotService.GetByLotCode(result.Code);
                InformacoesParaPagina(await lote);
            }
        }

        #endregion

        public void InformacoesParaPagina (LotMobile lot)
        {
            if (lot != null)
            {
                stackLot.IsVisible = true;
                if (DependencyService.Get<IFCMInfosGetter>().isSubscribedToTopic("follow." + lot.Code))
                {
                    btnSeguir.Text = "Deixar de seguir";
                    isSubscribed = true;
                }
                else
                {
                    btnSeguir.Text = "Seguir";
                    isSubscribed = false;
                }

                LotCode = lot.Code;

                lblCod.Text = lot.Code;
                lblValidade.Text = lot.Vality.ToString();
                ListViewProduct.ItemsSource = lot.ListProduct;
                ListAnalysis = lot.ListAnalysis;
            }
            
        }
    }
}
