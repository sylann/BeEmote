using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BeEmote.Core
{
    public interface IEmotionParser
    {
        /// <summary>
        /// Provides a valid Emotion API Request, encapsulating an url.
        /// </summary>
        /// <param name="url">The url to encapsulate in the json structure</param>
        /// <returns>The serialized JObject</returns>
        JObject GetEmotionJson(string url);

        /// <summary>
        /// Parses the response of the Emotion API,
        /// resulting in a valid List of <see cref="Faces"/>.
        /// If the json is invalid, it should return Null.
        /// </summary>
        /// <param name="json">Json string response from the Emotion API</param>
        /// <returns>A List of <see cref="Faces"/> or Null</returns>
        List<Face> GetFacesFromJson(string json);
    }
}
