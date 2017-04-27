using BeEmote.Core;
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
        #region Public Methods

        /// <summary>
        /// Finalize the configuration for the Emotion request.
        /// Executes the request and returns the json result asynchronously.
        /// </summary>
        /// <param name="ImagePath">The full path to a local image</param>
        public async Task<string> MakeEmotionRequest(string ImagePath)
        {
            return await MakeRequest(new RequestConfiguration(
                "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize",
                GetImageAsByteArray(ImagePath),
                "application/octet-stream",
                Credentials.EmotionKey));
        }

        /// <summary>
        /// Finalize the configuration for the Text Analytics request.
        /// Executes the request and returns the json result asynchronously.
        /// </summary>
        /// <param name="Query">Part that is specific to the service</param>
        /// <param name="JsonBody">The json string corresponding to the service</param>
        public async Task<string> MakeTextAnalyticsRequest(string Query, string JsonBody)
        {
            return await MakeRequest(new RequestConfiguration(
                $"https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/{Query}",
                GetJsonAsByteArray(JsonBody),
                "application/json",
                Credentials.TextAnalyticsKey
                ));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Send a request to the server with a configuration object as parameter,
        /// Await for the async response ( type string ) from the server
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <returns>Task(string) JSON data about the image</returns>
        private async Task<string> MakeRequest(RequestConfiguration Config)
        {
            // Configure Request
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Config.CredentialKey);

            // Send request
            using (var request = new ByteArrayContent(Config.Data))
            {
                request.Headers.ContentType = new MediaTypeHeaderValue(Config.ContentType);
                return await (await client.PostAsync(Config.Uri, request)).Content.ReadAsStringAsync();
            }
        }

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
        /// <param name="jsonBody">The string representing the json body</param>
        /// <returns></returns>
        private byte[] GetJsonAsByteArray(string jsonBody)
        {
            return Encoding.UTF8.GetBytes(jsonBody);
        }

        #endregion
    }
}
