using BeEmote.Core;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BeEmote.Services
{
    /// <summary>
    /// Dedicated to preparing and sending requests to the API
    /// </summary>
    public class RequestManager
    {
        /// <summary>
        /// Configuration containing the need information to make a request.
        /// </summary>
        private RequestConfiguration Configuration;

        #region Public Methods

        /// <summary>
        /// Configure an Emotion request for a local image,
        /// by providing its local path
        /// </summary>
        /// <param name="ImagePath">The local path of the image</param>
        public void SetEmotionConfiguration(string ImagePath)
        {
            Configuration = new RequestConfiguration(
                Uri: "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize",
                Data: GetImageAsByteArray(ImagePath),
                ContentType: "application/octet-stream",
                CredentialKey: Credentials.EmotionKey);
        }

        /// <summary>
        /// Configure An Emotion Request for a remote image,
        /// by providing its url.
        /// </summary>
        /// <param name="ImageUrl">Image url encapsulated in a <see cref="JObject"/></param>
        public void SetEmotionConfiguration(JObject ImageUrl)
        {
            Configuration = new RequestConfiguration(
                Uri: "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize",
                Data: GetJsonAsByteArray(ImageUrl),
                ContentType: "application/json",
                CredentialKey: Credentials.EmotionKey);
        }

        /// <summary>
        /// TODO: Add description
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="body"></param>
        public void SetTextAnalyticsConfiguration(string Query, JObject body)
        {
            Configuration = new RequestConfiguration(
                Uri: $"https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/{Query}",
                Data: GetJsonAsByteArray(body),
                ContentType: "application/json",
                CredentialKey: Credentials.TextAnalyticsKey);
        }

        /// <summary>
        /// Send a request to the server with a configuration object as parameter,
        /// Await for the async response ( type string ) from the server
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <returns>Task(string) JSON data about the image</returns>
        public async Task<string> MakeRequest()
        {
            using (var client = new HttpClient())
            {
                // Configure
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Configuration.CredentialKey);
                var request = new ByteArrayContent(Configuration.Data);
                request.Headers.ContentType = new MediaTypeHeaderValue(Configuration.ContentType);

                // Send
                System.Console.WriteLine($"Sending request to {Configuration.Uri}...");
                return await (await client.PostAsync(Configuration.Uri, request)).Content.ReadAsStringAsync();
            }
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Transform an image from its local path into an array of byte
        /// </summary>
        /// <param name="imageFilePath">The full path to the image</param>
        /// <returns></returns>
        private byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        /// <summary>
        /// Transforms a string representing a json body into an array of byte
        /// </summary>
        /// <param name="json">The string representing the json body</param>
        /// <returns></returns>
        private byte[] GetJsonAsByteArray(JObject json)
        {
            return Encoding.UTF8.GetBytes(json.ToString());
        }

        #endregion

    }
}
