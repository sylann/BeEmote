///<License terms GNU v3>
/// BeEmote is a simple application that allows you to analyse photos
/// or text with the Microsoft's Cognitive "Emotion API" and "Text Analytics API"
/// Copyright (C) 2017  Romain Vincent
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
/// </License>

using BeEmote.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeEmote.Services
{
    /// <summary>
    /// Provides methods to deserialize or serialize Json,
    /// related to the Cognitive Services API.
    /// Should be used by the <see cref="AppManager"/>.
    /// </summary>
    public class JsonManager : IEmotionParser, ITextAnalyticsParser
    {
        #region public Methods

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
        public List<Face> GetFacesFromJson(string json)
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
        public Language GetLanguageFromJson(string jsonString)
        {
            JObject raw;
            try
            {
                raw = ParseJsonResponse(jsonString);
                return raw["documents"][0]["detectedLanguages"][0].ToObject<Language>();
            }
            catch (Exception)
            {
                Console.WriteLine("The response structure was unexpected");
                return null;
            }
        }

        /// <summary>
        /// Get the key phrases from a json string if it contains any.
        /// Else return null.
        /// </summary>
        /// <param name="jsonString">A response from the Text Analytics API</param>
        /// <returns>The key phrases of a text</returns>
        public List<string> GetKeyPhrasesFromJson(string jsonString)
        {
            JObject raw;
            try
            {
                raw = ParseJsonResponse(jsonString);
                return raw["documents"][0]["keyPhrases"].ToObject<List<string>>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get the score value from a json string if it contains it.
        /// Else return null.
        /// </summary>
        /// <param name="jsonString">A response from the Text Analytics API</param>
        /// <returns>The score of a text</returns>
        public double? GetScoreFromJson(string jsonString)
        {
            JObject raw;
            try
            {
                raw = ParseJsonResponse(jsonString);
                return (double?)raw["documents"][0]["score"];
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Transform a response json string into a <see cref="JObject"/>.
        /// </summary>
        /// <param name="jsonString">A json structured string</param>
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
