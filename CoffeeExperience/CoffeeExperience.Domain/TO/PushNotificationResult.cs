using System;

namespace CoffeeExperience.Domain.TO
{
    public class PushNotificationResult
    {

        public bool Sucesss
        {
            get;
            set;
        }

        public string Details
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }

    }
}
