using CoffeeExperience.Domain.TO;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace CoffeeExperience.Domain.Services
{
    public class PushNotificationService
    {
        //Tópico é como é feito a lógica para enviar notificações para o aplicativo, o usuário se inscreve em um tópico através do aplicativo e o firebase encaminha a notificação para todo mundo que está inscrito nesse mesmo tópico.
        public PushNotificationResult SendNotification(string _title, string _message, string _topic)
        {            
            PushNotificationResult result = new PushNotificationResult();
            try
            {
                result.Sucesss = true;
                result.Error = null;
                var requestUri = "https://fcm.googleapis.com/fcm/send";

                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAA9WtlbSc:APA91bGauhKbdNozyR3eE_R20L5OuwlGOgQQmqQxcCf_qJMRd-BAFB6MpOvgyc-p4O4tatavsXLl47NqECpnq1VcD24FUGMNkWn_tyvC36e3ErNpc5drUfQ70E9uMQwIdKE_0SaFaCpl"));
                webRequest.Headers.Add(string.Format("Sender: id={0}", "1054068796711"));
                webRequest.ContentType = "application/json";

                var data = new
                {
                    // to = YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
                    to = "/topics/" + _topic, // this is for topic 
                    notification = new
                    {
                        title = _title,
                        body = _message
                        //icon="myicon"
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                result.Details = sResponseFromServer;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Sucesss = false;
                result.Details = null;
                result.Error = ex;
            }

            return result;
        }
    }
}
