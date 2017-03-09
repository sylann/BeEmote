using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using System.Configuration;

namespace BeEmote.Services
{
    public class EmotionRequest
    {
        public void Main()
        {
            MakeRequest();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        public async void MakeRequest()
        {
            var client = new HttpClient();

            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["emotionKey"]);

            var uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize?" + queryString;

            HttpResponseMessage response;

            // Request body Example
            byte[] byteData = Encoding.UTF8.GetBytes("{ \"url\": \"http://hd-wall-papers.com/images/wallpapers/family-pictures/family-pictures-3.jpg\" }");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }

        }

        // Response 400
        // Indicates JSON parsing error, faceRectangles cannot be parsed correctly, or count exceeds 64, or content-type is not recognized.

        // Response 401
        // application/json
        // { "statusCode": 401, "message": "Access denied due to invalid subscription key. Make sure you are subscribed to an API you are trying to call and provide the right key." }

        // Response 403
        // application/json
        // { "statusCode": 403, "message": "Out of call volume quota. Quota will be replenished in 2.12 days." }

        // Response 429
        // application/json
        // { "statusCode": 429, "message": "Rate limit is exceeded. Try again in 26 seconds." }
    }
}
