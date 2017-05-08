using BeEmote.Core;
using Newtonsoft.Json.Linq;
using PropertyChanged;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace BeEmote.Services
{
    /// <summary>
    /// Handles the global functionning of the application
    /// </summary>
    [ImplementPropertyChanged]
    public class AppManager : INotifyPropertyChanged, ICognitiveApp, IEmotionAPI, ITextAnalyticsAPI
    {
        #region Private Fields

        private RequestManager _RequestManager;
        private JsonManager _JsonManager;
        private string imagePath;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Todo: Add description
        /// </summary>
        public TextAnalyticsApiResponse TextAnalyticsResponse { get; set; }

        /// <summary>
        /// Todo: Add description
        /// </summary>
        public EmotionApiResponse EmotionResponse { get; set; }

        /// <summary>
        /// This is the text to send to the Text Analytics API
        /// </summary>
        public string TextToAnalyse { get; set; }

        /// <summary>
        /// This is the path of the image to send to the Emotion API
        /// </summary>
        public string ImagePath {
            get => imagePath;
            set
            {
                // If the path is a urL, the API will handle its validity, so we don't care.
                // Thus we only check if it's nor a url neither a valid local path.
                // And eliminate the case where no value is provided at all.
                if (value == null
                || !Uri.IsWellFormedUriString(value, UriKind.Absolute)
                && !File.Exists(value))
                {
                    imagePath = null;
                    return;
                }
                // Everything is ok, Set ImagePath
                imagePath = value;
            }
        }

        /// <summary>
        /// Indicates if the ImagePath looks like a URI.
        /// </summary>
        public bool ImagePathIsUri
        {
            get => Uri.IsWellFormedUriString(imagePath, UriKind.Absolute);
        }

        /// <summary>
        /// Folder containing the Image of the ImagePath.
        /// </summary>
        public string ImageFolder
        {
            get => imagePath != null && !ImagePathIsUri
                ? Directory.GetParent(ImagePath).ToString()
                : null;
        }

        /// <summary>
        /// Indicates the state in which the app currently is
        /// </summary>
        public RequestStates State { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the app such that it can wait for user interaction
        /// </summary>
        public AppManager()
        {
            _RequestManager = new RequestManager();
            _JsonManager = new JsonManager();
            // Set start up state
            State = RequestStates.NoData;
            // Put dummy data until the interface allows us to set it for real
            //TextToAnalyse = JsonExamples.GetEnglishText();
        }

        #endregion

        #region ICognitiveApp implementation

        /// <summary>
        /// Send a request to the Emotion API and fully handle the results.
        /// Finally, print a description of the result in the console.
        /// </summary>
        public async Task StartEmotion()
        {
            // Sends the request and handle the result
            State = RequestStates.AwaitingResponse;

            var conf = GetEmotionConfiguration(ImagePath);
            EmotionResponse = await GetEmotionFaces(conf);

            if (EmotionResponse?.Faces?.Count > 0)
            {
                State = RequestStates.ResponseReceived;
                // Prints results in the console
                EmotionResponse.Describe();
            }
            else
                State = RequestStates.EmptyResult;
        }

        /// <summary>
        /// Send 3 requests in a defined order to the Text Analytics API.
        /// Finally, print a description of the result in the console.
        /// </summary>
        public async Task StartTextAnalytics()
        {
            // Instanciates the Text Analytics API Response model
            TextAnalyticsResponse = new TextAnalyticsApiResponse();

            // Send the request and handle the result
            State = RequestStates.AwaitingResponse;

            var conf = GetTextAnalyticsConfiguration("languages", TextToAnalyse);
            TextAnalyticsResponse.Language = await GetTextAnalyticsLanguage(conf);

            State = RequestStates.PartialResult;

            conf = GetTextAnalyticsConfiguration("keyPhrases", TextToAnalyse, TextAnalyticsResponse.Language.Iso6391Name);
            TextAnalyticsResponse.KeyPhrases = await GetTextAnalyticsKeyPhrases(conf);

            conf = GetTextAnalyticsConfiguration("sentiment", TextToAnalyse, TextAnalyticsResponse.Language.Iso6391Name);
            TextAnalyticsResponse.Score = await GetTextAnalyticsScore(conf);

            if (string.IsNullOrEmpty(TextAnalyticsResponse?.Language?.Name))
                State = RequestStates.EmptyResult;
            else
                State = RequestStates.ResponseReceived;

            // Prints results in the console
            TextAnalyticsResponse.Describe();
        }

        #endregion

        #region IEmotionAPI implementation

        /// <summary>
        /// Sends a request with the provided configuration
        /// </summary>
        /// <param name="conf">A configuration for the Emotion API</param>
        /// <returns>The response from API</returns>
        public async Task<EmotionApiResponse> GetEmotionFaces(RequestConfiguration conf)
        {
            // Send the request
            string json = await _RequestManager.MakeRequest(conf);
            // Put the result in the model
            return new EmotionApiResponse(_JsonManager.GetFacesFromJsonResponse(json));
        }

        /// <summary>
        /// Configures an emotion request according to the provided image url
        /// </summary>
        /// <returns></returns>
        public RequestConfiguration GetEmotionConfiguration(string imagePath)
        {
            // Configure a new Request
            if (string.IsNullOrWhiteSpace(imagePath))
                return null;
            else if (Uri.IsWellFormedUriString(imagePath, UriKind.Absolute))
                return _RequestManager.GetEmotionConfiguration(_JsonManager.GetEmotionJson(imagePath));
            else
                return _RequestManager.GetEmotionConfiguration(imagePath);
        }

        #endregion

        #region ITextAnalytics implementation
        
        /// <summary>
        /// Sends a request to the Text Analytics API.
        /// Retrive the <see cref="TextAnalyticsApiResponse.Language"/> from the json response.
        /// </summary>
        public async Task<Language> GetTextAnalyticsLanguage(RequestConfiguration conf)
        {
            // Send Request
            string jsonString = await _RequestManager.MakeRequest(conf);
            // Put the result into the model
            return _JsonManager.GetLanguageFromJsonResponse(jsonString);
        }

        /// <summary>
        /// Sends a request to the Text Analytics API.
        /// Retrive the <see cref="TextAnalyticsApiResponse.KeyPhrases"/> from the json response.
        /// </summary>
        public async Task<List<string>> GetTextAnalyticsKeyPhrases(RequestConfiguration conf)
        {
            // Send Request
            string jsonString = await _RequestManager.MakeRequest(conf);
            // Put the result into the model
            return _JsonManager.GetKeyPhrasesFromJsonResponse(jsonString);

        }

        /// <summary>
        /// Sends a request to the Text Analytics API.
        /// Retrive the <see cref="TextAnalyticsApiResponse.Score"/> from the Json Response
        /// </summary>
        public async Task<double?> GetTextAnalyticsScore(RequestConfiguration conf)
        {
            // Send Request
            string jsonString = await _RequestManager.MakeRequest(conf);
            // Put the result into the model
            return _JsonManager.GetScoreFromJsonResponse(jsonString);
        }

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="query">The end of the Text Analytics API url</param>
        /// <param name="text">The text to analyse</param>
        /// <returns>A configuration to request a Text's Language</returns>
        public RequestConfiguration GetTextAnalyticsConfiguration(string query, string text)
        {
            // Configure request
            JObject body = _JsonManager.GetTextAnalyticsJson(text);
            return _RequestManager.GetTextAnalyticsConfiguration(query, body);
        }

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="query">The end of the Text Analytics API url</param>
        /// <param name="text">The text to analyse</param>
        /// <param name="language">The language of the text as already identified by the API</param>
        /// <returns>A configuration to request a Text's key phrases or Score</returns>
        public RequestConfiguration GetTextAnalyticsConfiguration(string query, string text, string language)
        {
            // Configure request
            JObject body = _JsonManager.GetTextAnalyticsJson(text, language);
            return _RequestManager.GetTextAnalyticsConfiguration(query, body);
        }

        #endregion

        #region Events

        /// <summary>
        /// An event is fired whenever a public property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #endregion
    }
}
