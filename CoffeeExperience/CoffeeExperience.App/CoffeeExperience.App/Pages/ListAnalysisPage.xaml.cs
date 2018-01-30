using CoffeeExperience.Domain.Portable.Entities;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace CoffeeExperience.App.Pages
{
    public partial class ListAnalysisPage : ContentPage
    {
        public ObservableCollection<AnalysisMobile> ListAnalysis { get; set; }
        public string LotCode { get; set; }
        public ListAnalysisPage(ObservableCollection<AnalysisMobile> listAnalysis, string lotCode)
        {

            InitializeComponent();
            Title = "Lista de Análises";
            lblLote.Text = lotCode;
            LotCode = lotCode;
            ListViewAnalysis.ItemsSource = listAnalysis;
            ListAnalysis = listAnalysis;

        }

        private void ListViewAnalysis_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new AnalysisPage(e.Item as AnalysisMobile, LotCode));
        }
    }

}