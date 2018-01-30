using Xamarin.Forms;

namespace CoffeeExperience.App
{
    public class Constants
    {
        public static class Colors
        {
            public static Color BackgroundOverlay = Color.FromRgba(120, 120, 120, 180);
            public static Color BackgroundToolbar = Color.FromHex("117fc1");
            public static Color FontColorBrown = Color.FromHex("2e0407");
        }

        public static string WebServiceEndPoint = "http://coffeeexperiencewebapi.azurewebsites.net/";


        public static string UserEndPoint = "api/v1/userv1/";
        public static string LotEndPoint = "api/v1/lotv1/";
        public static string AnalysisEndPoint = "api/v1/analysisv1/";
        public static string LaboratoryEndPoint = "api/v1/laboratoryv1/";

    }

}
