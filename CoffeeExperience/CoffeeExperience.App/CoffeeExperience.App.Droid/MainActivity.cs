using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using Acr.BarCodes;
using Xamarin.Forms;
using System.Collections.Generic;
using Firebase.Iid;
using System.Net;
using System.IO;
using System.Json;
using Newtonsoft.Json.Linq;
using System;
using Firebase.Messaging;
using Android.Gms.Common;

namespace CoffeeExperience.App.Droid
{
    [Activity(Label = "Coffee Experience", Theme = "@style/MainTheme",  Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public string deviceToken;
        public static MainActivity Self { get; private set; }
        public List<string> topics { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            BarCodes.Init(() => (Activity)Forms.Context);

            //configuração do firebase
            topics = new List<string>();
            if (IsPlayServicesAvailable())
            {
                if (FirebaseInstanceId.Instance.Token != null)
                {
                    MakeGetRequest(FirebaseInstanceId.Instance.Token);
                }
            }

            Self = this;
            LoadApplication(new App());
        }
        //metodos de configuração do firebase
        public string getToken()
        {
            return FirebaseInstanceId.Instance.Token;
        }

        public async void MakeGetRequest(string token)
        {
            try
            {
                string url = "https://iid.googleapis.com/iid/info/" + token + "?details=true";
                string serverKey = "AAAA9WtlbSc:APA91bGauhKbdNozyR3eE_R20L5OuwlGOgQQmqQxcCf_qJMRd-BAFB6MpOvgyc-p4O4tatavsXLl47NqECpnq1VcD24FUGMNkWn_tyvC36e3ErNpc5drUfQ70E9uMQwIdKE_0SaFaCpl";
                deviceToken = token;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = "GET";
                request.Headers["Authorization"] = "key=" + serverKey;

                var response = request.GetResponse();
                var respStream = response.GetResponseStream();
                respStream.Flush();

                using (StreamReader sr = new StreamReader(respStream))
                {
                    JsonValue jsonDoc = JsonObject.Load(respStream);
                    var objResponse = JObject.Parse(jsonDoc.ToString());
                    if (objResponse["rel"] != null)
                    {
                        foreach (KeyValuePair<string, JToken> sub_obj in (JObject)objResponse["rel"]["topics"])
                        {
                            topics.Add(sub_obj.Key);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }

        }

        public void subscribeToTopic(string topicName)
        {
            FirebaseMessaging.Instance.SubscribeToTopic(topicName);
            topics.Add(topicName);
        }



        public void unsubscribeFromTopic(string topicName)
        {
            FirebaseMessaging.Instance.UnsubscribeFromTopic(topicName);
            topics.Remove(topicName);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

