using BeEmote.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace BeEmote.Services
{
    /// <summary>
    /// Handles the global functionning of the application
    /// </summary>
    public class AppManager
    {
        #region Private Fields

        private RequestManager _RequestManager;
        private JsonManager _JsonManager;
        private TextAnalyticsApiResponse _TextAnalytics;
        private EmotionApiResponse _Emotion;

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates if the app is currently waiting for the API response
        /// </summary>
        public bool AwaitingServiceResponse { get; private set; }

        /// <summary>
        /// This is the text to send to the Text Analytics API
        /// </summary>
        public string TextToAnalyse { get; set; }

        /// <summary>
        /// This is the path of the image to send to the Emootion API
        /// </summary>
        public string ImagePath { get; private set; }

        /// <summary>
        /// Verify that the provided path is correct.
        /// Update <see cref="ImagePath"/> if it is and return true.
        /// Else return false.
        /// </summary>
        /// <param name="path">A provided path that should point to an image</param>
        public bool SetImagePath(string path)
        {
            // If the path is a urL, the API will handle its validity, so we don't care.
            // Thus we only check if it's nor a url neither a valid local path.
            if (!path.StartsWith("http") && !File.Exists(path))
                return false;
            
            // Everything is ok, Set ImagePath and return true
            ImagePath = path;
            return true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize the app such that it can wait for user interaction
        /// </summary>
        public void Init()
        {
            _RequestManager = new RequestManager();
            _JsonManager = new JsonManager();
            // Put dummy data until the interface allows us to set it for real
            //TextToAnalyse = JsonExamples.GetEnglishText();
        }

        /// <summary>
        /// Send a request with the provided <see cref="ImagePath"/> to the Emotion API.
        /// Finally, print a description of the result in the console.
        /// </summary>
        public async void StartEmotion()
        {
            // Configure a new Request
            if (ImagePath.StartsWith("http"))
                _RequestManager.SetEmotionConfiguration(_JsonManager.GetEmotionJson(ImagePath));
            else
                _RequestManager.SetEmotionConfiguration(ImagePath);

            // Sends the request
            AwaitingServiceResponse = true;
            string json = await _RequestManager.MakeRequest();
            AwaitingServiceResponse = false;
            // Instanciates the Emotion API Response model
            _Emotion = new EmotionApiResponse(_JsonManager.GetFacesFromJsonResponse(json));
            // Prints results in the console
            _Emotion.Describe();
        }

        /// <summary>
        /// Succesfully then send 3 requests in a defined order to the Text Analytics API.
        /// Finally, print a description of the result in the console.
        /// </summary>
        public async void StartTextAnalytics()
        {
            // Instanciates the Text Analytics API Response model
            _TextAnalytics = new TextAnalyticsApiResponse();
            AwaitingServiceResponse = true;
            // Phase 1
            await GetTextAnalyticsLanguage();
            // Phase 2
            await GetTextAnalyticsKeyPhrases();
            // Phase 3
            await GetTextAnalyticsScore();
            AwaitingServiceResponse = false;
            // Prints results in the console
            _TextAnalytics.Describe();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Calls the <see cref="ParseJsonResponse(string)"/> method with the specified query,
        /// which sends the request to the Text Analytics API.
        /// Sets the <see cref="TextAnalyticsApiResponse.Language"/> of the text once the response is caught.
        /// </summary>
        private async Task GetTextAnalyticsLanguage()
        {
            // Configure request
            JObject body = _JsonManager.GetTextAnalyticsJson(TextToAnalyse);
            _RequestManager.SetTextAnalyticsConfiguration("languages", body);
            // Send Request
            string jsonString = await _RequestManager.MakeRequest();
            // Put the result into the model
            _TextAnalytics.Language = _JsonManager.GetLanguageFromJsonResponse(jsonString);
        }

        /// <summary>
        /// Calls the <see cref="ParseJsonResponse(string)"/> method with the specified query,
        /// which sends the request to the Text Analytics API.
        /// Sets the <see cref="TextAnalyticsApiResponse.KeyPhrases"/> of the text once the response is caught.
        /// </summary>
        private async Task GetTextAnalyticsKeyPhrases()
        {
            // Configure request
            JObject body = _JsonManager.GetTextAnalyticsJson(TextToAnalyse, _TextAnalytics.Language.Iso6391Name);
            _RequestManager.SetTextAnalyticsConfiguration("keyPhrases", body);
            // Send Request
            string jsonString = await _RequestManager.MakeRequest();
            // Put the result into the model
            _TextAnalytics.KeyPhrases = _JsonManager.GetKeyPhrasesFromJsonResponse(jsonString);

        }

        /// <summary>
        /// Calls the <see cref="ParseJsonResponse(string)"/> method with the specified query,
        /// which sends the request to the Text Analytics API.
        /// Sets the <see cref="TextAnalyticsApiResponse.Score"/> of the text once the response is caught.
        /// </summary>
        private async Task GetTextAnalyticsScore()
        {
            // Configure request
            JObject body = _JsonManager.GetTextAnalyticsJson(TextToAnalyse, _TextAnalytics.Language.Iso6391Name);
            _RequestManager.SetTextAnalyticsConfiguration("sentiment", body);
            // Send Request
            string jsonString = await _RequestManager.MakeRequest();
            // Put the result into the model
            _TextAnalytics.Score = _JsonManager.GetScoreFromJsonResponse(jsonString);
        }

        #endregion
    }
}
