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
    public partial class LaboratoryPage : ContentPage
    {
        public LaboratoryPage()
        {
            try
            {
                InitializeComponent();
                this.Title = "Laboratório";
            }
            catch (Exception e)
            {

                
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
                LaboratoryService laboratoryService = new LaboratoryService();
                var laboratory = laboratoryService.GetByLaboratoryCode(result.Code);
                InformacoesParaPagina(await laboratory);
            }
        }

        #endregion

        public void InformacoesParaPagina(LaboratoryMobile laboratory)
        {
            if (laboratory != null)
            {
                lblNome.Text = laboratory.Name;
                lblCidade.Text = laboratory.City;
                lblCnpj.Text = laboratory.CNPJ.ToString();
                lblCodigo.Text = laboratory.Code;
                
            }

        }

    }
}
