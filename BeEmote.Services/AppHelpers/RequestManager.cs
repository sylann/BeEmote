using BeEmote.Core;
using Newtonsoft.Json.Linq;
using System;
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
        #region IConfigurable implementation

        /// <summary>
        /// Produce a new <see cref="RequestConfiguration"/> from the provided imagePath.
        /// Handles both local image path and remote url.
        /// </summary>
        /// <param name="imagePath">The path of the image (local or url)</param>
        /// <returns>A valid emotion request configuration or null</returns>
        public RequestConfiguration GetEmotionConfiguration(string imagePath)
        {
            // Empty path is not handeled
            if (string.IsNullOrWhiteSpace(imagePath))
                return null;

            var url = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize";
            Byte[] data;
            string type;
            var key = Credentials.EmotionKey;

            // In case of local path, directly use the image path
            if (!Uri.IsWellFormedUriString(imagePath, UriKind.Absolute))
            {
                data = ByteArrayBuilder.FromImagePath(imagePath);
                type = "application/octet-stream";
            }
            // In case of URL, embed the image in a json body
            else
            {
                var Json = new JsonManager();
                var body = Json.GetEmotionJson(imagePath);
                data = ByteArrayBuilder.FromJsonObject(body);
                type = "application/json";
            }
            
            return new RequestConfiguration(
                Uri: url,
                Data: data,
                ContentType: type,
                CredentialKey: Credentials.EmotionKey);
        }

        /// <summary>
        /// Produce a new <see cref="RequestConfiguration"/>
        /// from the provided <paramref name="query"/>, <paramref name="text"/> and <paramref name="language"/>.
        /// The resulting configuration depends on the value of <paramref name="query"/>
        /// </summary>
        /// <param name="query">The end of the Text Analytics API GET route</param>
        /// <param name="text">The text to analyse</param>
        /// <param name="language">The language of the text (may be null)</param>
        public RequestConfiguration GetTextAnalyticsConfiguration(string query, string text, string language)
        {
            var Json = new JsonManager();
            JObject body;

            switch (query)
            {
                case "languages":
                    body = Json.GetTextAnalyticsJson(text);
                    break;
                case "keyPhrases":
                case "sentiment":
                    if (language == null)
                        throw new ArgumentNullException($"language shouldn't be null for a {query} request.");
                    body = Json.GetTextAnalyticsJson(text, language);
                    break;
                default:
                    throw new ArgumentException($"Unexpected {nameof(query)}: {query}. Should be 'languages', 'keyPhrases' or 'sentiment'");
            }
            
            return new RequestConfiguration(
                Uri: $"https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/{query}",
                Data: ByteArrayBuilder.FromJsonObject(body),
                ContentType: "application/json",
                CredentialKey: Credentials.TextAnalyticsKey);
        }

        #endregion

        #region IRequest implementation

        /// <summary>
        /// Send a request to a Microsoft's cognitive API,
        /// according to the provided <paramref name="configuration"/>.
        /// Await for the response before returning it.
        /// </summary>
        /// <param name="configuration">Provides the url, the content and its type, and a credential key</param>
        /// <returns>Task(string) JSON data about the image</returns>
        public async Task<string> MakeRequest(RequestConfiguration configuration)
        {
            // Propagate null to the next phase
            if (configuration == null)
                return null;

            // Http request
            using (var client = new HttpClient())
            {
                // Configure
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", configuration.CredentialKey);
                var request = new ByteArrayContent(configuration.Data);
                request.Headers.ContentType = new MediaTypeHeaderValue(configuration.ContentType);

                // Send
                System.Console.WriteLine($"Sending request to {configuration.Uri}...");
                return await (await client.PostAsync(configuration.Uri, request)).Content.ReadAsStringAsync();
            }
        }

        #endregion
    }
}
