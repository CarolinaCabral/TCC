using Acr.BarCodes;
using CoffeeExperience.App.WebService;
using CoffeeExperience.Domain.Portable.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CoffeeExperience.App.Pages
{
    public partial class AnalysisPage : ContentPage
    {
        public string LotCode { get; set; }
        public AnalysisPage(AnalysisMobile analysis, string lotCode)
        {
            InitializeComponent();
            this.Title = "Análise";
            LotCode = lotCode;
            InformacoesParaPagina(analysis);            
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
                AnalysisService analysisService = new AnalysisService();
                var analysis = analysisService.GetByAnalysisCode(result.Code);
                InformacoesParaPagina(await analysis);
            }
        }

        #endregion

        public void InformacoesParaPagina(AnalysisMobile analysis)
        {
            if (analysis != null)
            {
                lblCod.Text = analysis.Code;
                lblLaboratorio.Text = analysis.Laboratory.Name;
                if(string.IsNullOrEmpty(LotCode))
                {
                    LotCode = analysis.Lot == null ? "" : analysis.Lot.Code;                   

                }
                lblLote.Text = LotCode;
                lblTipo.Text = analysis.Type.ToString();
                if (analysis.ListQualityResult != null)
                {
                    ListViewQuality.ItemsSource = analysis.ListQualityResult;
                }
            } 
            
        }
    }
}
