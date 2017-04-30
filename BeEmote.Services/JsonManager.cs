using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeEmote.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BeEmote.Services
{
    public class JsonManager
    {
        #region Public Methods

        /// <summary>
        /// Builds a json object containing the provided url,
        /// as needed by the Emotion API "GET"
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public JObject GetEmotionJson(string url)
        {
            return new JObject
            {
                ["url"] = url
            };
        }

        /// <summary>
        /// Builds a json object containing a provided text,
        /// as needed by the Text Analytics API "GET /languages"
        /// </summary>
        public JObject GetTextAnalyticsJson(string text)
        {
            return new JObject
            {
                ["documents"] = new JArray {
                    new JObject {
                        ["id"] = 0,
                        ["text"] = text
                    }
                }
            };
        }

        /// <summary>
        /// Builds a json object request containing a language
        /// </summary>
        public JObject GetTextAnalyticsJson(string text, string language)
        {
            return new JObject
            {
                ["documents"] = new JArray {
                    new JObject {
                        ["id"] = 0,
                        ["text"] = text,
                        ["language"] = language
                    }
                }
            };
        }

        /// <summary>
        /// Try to get the list of faces from a json string if it contains any.
        /// Automatically instanciates the relevant classes by deserializing the response
        /// If the deserialization fails, return null.
        /// </summary>
        /// <param name="json">A response from the Text Analytics API</param>
        /// <returns>The list of faces of an image</returns>
        public List<Face> GetFacesFromJsonResponse(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Face>>(json);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get the language from a json string if it contains it.
        /// Else return null.
        /// </summary>
        /// <param name="jsonString">A response from the Text Analytics API</param>
        /// <returns>The language of a text</returns>
        public Language GetLanguageFromJsonResponse(string jsonString)
        {
            JObject raw = ParseJsonResponse(jsonString);

            if (raw != null)
            {
                return raw["documents"][0]["detectedLanguages"][0].ToObject<Language>();
            }
            return null;
        }

        /// <summary>
        /// Get the key phrases from a json string if it contains any.
        /// Else return null.
        /// </summary>
        /// <param name="jsonString">A response from the Text Analytics API</param>
        /// <returns>The key phrases of a text</returns>
        public List<string> GetKeyPhrasesFromJsonResponse(string jsonString)
        {
            JObject raw = ParseJsonResponse(jsonString);
            if (raw != null)
            {
                return raw["documents"][0]["keyPhrases"].ToObject<List<string>>();
            }
            return null;
        }

        /// <summary>
        /// Get the score value from a json string if it contains it.
        /// Else return -1.
        /// </summary>
        /// <param name="jsonString">A response from the Text Analytics API</param>
        /// <returns>The score of a text</returns>
        public double GetScoreFromJsonResponse(string jsonString)
        {
            JObject raw = ParseJsonResponse(jsonString);
            if (raw != null)
            {
                return (double)raw["documents"][0]["score"];
            }
            return -1;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Transform a response json string into a <see cref="JObject"/>.
        /// </summary>
        /// <param name="Query">Last part of the route to the Text Analytics API</param>
        /// <returns>The raw Json Object</returns>
        private JObject ParseJsonResponse(string jsonString)
        {
            JObject jsonObject = JObject.Parse(jsonString);

            // The API could have send errors instead of the expected response
            // In that case we would return null
            if (jsonObject["errors"].Count() > 0)
            {
                Console.WriteLine(jsonObject["errors"][0]["message"]);
                return null;
            }
            return jsonObject;
        }

        #endregion
    }
}
