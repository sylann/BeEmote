using System;

namespace BeEmote.Services
{
    /// <summary>
    /// Configuration to use with a Request to the Microsoft Congnitive Services
    /// </summary>
    public class RequestConfiguration
    {
        /// <summary>
        /// The complete URL of the request for the cognitive service
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Data content for the request. Could be made either
        /// from an image with <see cref="RequestManager.GetImageAsByteArray(string)"/>
        /// or from a Json string with <see cref="RequestManager.GetJsonAsByteArray(string)"/>.
        /// </summary>
        public Byte[] Data { get; set; }

        /// <summary>
        /// The type of the provided content of the request.
        /// Not to be confused with the accepted content-type.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// A hash key allowing for the use of the respective cognitive service.
        /// Each service has its own Credential key. See <see cref="Credentials"/>.
        /// </summary>
        public string CredentialKey { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="query">The lacking part of the URI after the host</param>
        /// <param name="Data"></param>
        /// <param name="ContentType"></param>
        /// <param name="CredentialKey"></param>
        public RequestConfiguration(string query, byte[] Data, string ContentType, string CredentialKey)
        {
            Uri = $"{host}{query}";
            this.Data = Data;
            this.ContentType = ContentType;
            this.CredentialKey = CredentialKey;
        }

        /// <summary>
        /// Host of the Microsoft Cognitive services.
        /// </summary>
        private string host = "https://westus.api.cognitive.microsoft.com/";
    }
}
