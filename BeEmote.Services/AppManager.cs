using BeEmote.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeEmote.Services
{
    /// <summary>
    /// Handles the global functionning of the application
    /// </summary>
    public class AppManager
    {
        #region Private Fields

        private RequestManager _RequestManager;
        private TextAnalyticsApiResponse _TextAnalytics;
        private EmotionApiResponse _Emotion;
        private JObject _Request;

        #endregion

        #region Public Properties

        List<string> Errors { get; set; }

        string TextToAnalyse { get; set; }

        string ImagePath { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize the app such that it can wait for user interaction
        /// </summary>
        public void Init()
        {
            _RequestManager = new RequestManager();
            // Put dummy data until the interface allows us to set it for real
            TextToAnalyse = JsonExamples.GetEnglishText();
            ImagePath = @"G:\MyPics\self\20161214_124159.jpg";
        }

        /// <summary>
        /// Init a request body, then send 3 requests in a defined order to the Text Analytics API.
        /// Finally, print a description of the result in the console.
        /// </summary>
        public async void StartTextAnalytics()
        {
            // Resets the data
            _TextAnalytics = new TextAnalyticsApiResponse();
            // Initialization
            InitRequest();
            // Phase 1
            await GetTextAnalyticsLanguage();
            // Phase 2
            await GetTextAnalyticsKeyPhrases();
            // Phase 3
            await GetTextAnalyticsScore();

            _TextAnalytics.Describe();
        }

        /// <summary>
        /// Send a request with the provided <see cref="ImagePath"/> to the Emotion API.
        /// Finally, print a description of the result in the console.
        /// </summary>
        public async void StartEmotion()
        {
            // Sends the request
            string json = await _RequestManager.MakeEmotionRequest(ImagePath);

            // Automatically instanciates the relevant classes by deserializing the response
            List<Face> faces = JsonConvert.DeserializeObject<List<Face>>(json);

            // Instanciates the Emotion API Response model
            _Emotion = new EmotionApiResponse(faces);

            // Prints results in the console
            _Emotion.Describe();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds a json object request with no language specified
        /// </summary>
        private void InitRequest()
        {
            _Request = new JObject
            {
                ["documents"] = new JArray {
                    new JObject {
                        ["id"] = 0,
                        ["text"] = JsonExamples.GetEnglishText()
                    }
                }
            };
        }

        /// <summary>
        /// Builds a json object request containing a language
        /// </summary>
        private void InitRequestWithLanguage()
        {
            _Request = new JObject
            {
                ["documents"] = new JArray {
                    new JObject {
                        ["id"] = 0,
                        ["text"] = TextToAnalyse,
                        ["language"] = _TextAnalytics.Language.Iso6391Name
                    }
                }
            };

        }

        /// <summary>
        /// Checks if The language is indeed available in the json structure of the request
        /// </summary>
        /// <returns></returns>
        private bool RequestHasLanguage()
        {
            return _Request["documents"][0]["language"] != null;
        }

        /// <summary>
        /// Calls the <see cref="GetResponseObject(string)"/> method with the specified query,
        /// which sends the request to the Text Analytics API.
        /// Sets the <see cref="TextAnalyticsApiResponse.Language"/> of the text once the response is caught.
        /// </summary>
        private async Task GetTextAnalyticsLanguage()
        {
            JObject raw = await GetResponseObject("languages");
            if (raw != null)
            {
                _TextAnalytics.Language = raw["documents"][0]["detectedLanguages"][0].ToObject<Language>();
                InitRequestWithLanguage();
            }
        }

        /// <summary>
        /// Calls the <see cref="GetResponseObject(string)"/> method with the specified query,
        /// which sends the request to the Text Analytics API.
        /// Sets the <see cref="TextAnalyticsApiResponse.KeyPhrases"/> of the text once the response is caught.
        /// </summary>
        private async Task GetTextAnalyticsKeyPhrases()
        {
            JObject raw = await GetResponseObject("keyPhrases");
            if (raw != null)
            {
                _TextAnalytics.KeyPhrases = raw["documents"][0]["keyPhrases"].ToObject<List<string>>();
            }
        }

        /// <summary>
        /// Calls the <see cref="GetResponseObject(string)"/> method with the specified query,
        /// which sends the request to the Text Analytics API.
        /// Sets the <see cref="TextAnalyticsApiResponse.Score"/> of the text once the response is caught.
        /// </summary>
        private async Task GetTextAnalyticsScore()
        {
            JObject raw = await GetResponseObject("sentiment");
            if (raw != null)
            {
                _TextAnalytics.Score = (double)raw["documents"][0]["score"];
            }
        }

        /// <summary>
        /// Send a request to the Text analytics API. Check for errors.
        /// Then transform the response json string into a <see cref="JObject"/>.
        /// </summary>
        /// <param name="Query">Last part of the route to the Text Analytics API</param>
        /// <returns>The raw Json Object</returns>
        private async Task<JObject> GetResponseObject(string Query)
        {
            string jsonString = await _RequestManager.MakeTextAnalyticsRequest(Query, _Request.ToString());
            JObject jsonObject = JObject.Parse(jsonString);

            // The API could send errors instead of the expected response
            // In that case we would return null
            if (jsonObject["errors"].Count() > 0)
            {
                Console.WriteLine(jsonObject["errors"][0]["message"]);
                Errors.Insert(0, jsonObject["errors"][0]["message"].ToString());
                return null;
            }
            return jsonObject;
        }

        #endregion
    }
}
