using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CoffeeExperience.App.Droid.Controls;
using CoffeeExperience.App.Controls;

[assembly: Xamarin.Forms.Dependency(typeof(FCMInfosGetterImplementation))]

namespace CoffeeExperience.App.Droid.Controls
{
    public class FCMInfosGetterImplementation : IFCMInfosGetter
    {
        public List<string> getAllTopics()
        {
            return MainActivity.Self.topics;
        }

        public bool isSubscribedToTopic(string topicName)
        {
            if (MainActivity.Self.topics != null)
            {
                var topicInfo = MainActivity.Self.topics.IndexOf(topicName);

                if (topicInfo != -1)
                    return true;
                return false;
            }
            return false;
        }

        public void subscribeTopic(string topicName)
        {
            MainActivity.Self.subscribeToTopic(topicName);
        }

        public void unsubscribeTopic(string topicName)
        {
            MainActivity.Self.unsubscribeFromTopic(topicName);
        }

        public string getToken()
        {
            return MainActivity.Self.deviceToken;
        }
    }
}
