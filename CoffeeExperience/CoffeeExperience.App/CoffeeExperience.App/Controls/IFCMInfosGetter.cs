using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.App.Controls
{
    public interface IFCMInfosGetter
    {
        Boolean isSubscribedToTopic(string topicName);
        void subscribeTopic(string topicName);
        void unsubscribeTopic(string topicName);
        List<string> getAllTopics();
        string getToken();
    }
}
