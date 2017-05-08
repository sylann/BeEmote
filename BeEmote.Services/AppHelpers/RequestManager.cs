using BeEmote.Core;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BeEmote.Services
{
    /// <summary>
    /// Dedicated to preparing and sending requests to the API
    /// </summary>
    public class RequestManager : IConfigurable, IRequest
    {        
        #region Public Methods (Configuration)

        /// <summary>
        /// Configure an Emotion request for a local image,
        /// by providing its local path
        /// </summary>
        /// <param name="ImagePath">The local path of the image</param>
        public RequestConfiguration GetEmotionConfiguration(string ImagePath)
        {
            return new RequestConfiguration(
                Uri: "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize",
                Data: ByteArrayBuilder.FromImagePath(ImagePath),
                ContentType: "application/octet-stream",
                CredentialKey: Credentials.EmotionKey);
        }

        /// <summary>
        /// Configure An Emotion Request for a remote image,
        /// by providing its url.
        /// </summary>
        /// <param name="ImageUrl">Image url encapsulated in a <see cref="JObject"/></param>
        public RequestConfiguration GetEmotionConfiguration(JObject ImageUrl)
        {
            return new RequestConfiguration(
                Uri: "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize",
                Data: ByteArrayBuilder.FromJsonObject(ImageUrl),
                ContentType: "application/json",
                CredentialKey: Credentials.EmotionKey);
        }

        /// <summary>
        /// TODO: Add description
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="body"></param>
        public RequestConfiguration GetTextAnalyticsConfiguration(string Query, JObject body)
        {
            return new RequestConfiguration(
                Uri: $"https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/{Query}",
                Data: ByteArrayBuilder.FromJsonObject(body),
                ContentType: "application/json",
                CredentialKey: Credentials.TextAnalyticsKey);
        }

        #endregion

        #region Public Methods (Request)

        /// <summary>
        /// Send a request to the server with a configuration object as parameter,
        /// Await for the async response ( type string ) from the server
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <returns>Task(string) JSON data about the image</returns>
        public async Task<string> MakeRequest(RequestConfiguration conf)
        {
            using (var client = new HttpClient())
            {
                // Configure
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", conf.CredentialKey);
                var request = new ByteArrayContent(conf.Data);
                request.Headers.ContentType = new MediaTypeHeaderValue(conf.ContentType);

                // Send
                System.Console.WriteLine($"Sending request to {conf.Uri}...");
                return await (await client.PostAsync(conf.Uri, request)).Content.ReadAsStringAsync();
            }
        }

        #endregion
    }
}
