using BeEmote.Core;
using Newtonsoft.Json.Linq;
using PropertyChanged;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace BeEmote.Services
{
    /// <summary>
    /// Possible status of the Application regarding API request/response
    /// </summary>
    public enum AwaitStatus
    {
        NoData,
        AwaitingResponse,
        ResponseReceived,
        EmptyResult
    }

    /// <summary>
    /// Handles the global functionning of the application
    /// </summary>
    [ImplementPropertyChanged]
    public class AppManager : INotifyPropertyChanged
    {
        #region Private Fields

        private RequestManager _RequestManager;
        private JsonManager _JsonManager;
        private string imagePath;

        #endregion

        #region Events

        /// <summary>
        /// An event is fired whenever a public property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

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
                if (!value.StartsWith("http") && !File.Exists(value))
                    ImagePath = null;
                // Everything is ok, Set ImagePath and return true
                imagePath = value;
            }
        }

        /// <summary>
        /// Indicates the state in which the app currently is
        /// </summary>
        public AwaitStatus State { get; set; }

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
            State = AwaitStatus.NoData;
            // Put dummy data until the interface allows us to set it for real
            //TextToAnalyse = JsonExamples.GetEnglishText();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Send a request with the provided <see cref="ImagePath"/> to the Emotion API.
        /// Finally, print a description of the result in the console.
        /// </summary>
        public async Task StartEmotion()
        {
            // Configure a new Request
            if (ImagePath.StartsWith("http"))
                _RequestManager.SetEmotionConfiguration(_JsonManager.GetEmotionJson(ImagePath));
            else
                _RequestManager.SetEmotionConfiguration(ImagePath);

            State = AwaitStatus.AwaitingResponse;
            // Sends the request
            string json = await _RequestManager.MakeRequest();
            // Request satisfied
            State = AwaitStatus.ResponseReceived;
            // Instanciates the Emotion API Response model
            EmotionResponse = new EmotionApiResponse(_JsonManager.GetFacesFromJsonResponse(json));
            // Prints results in the console
            EmotionResponse.Describe();
        }

        /// <summary>
        /// Succesfully then send 3 requests in a defined order to the Text Analytics API.
        /// Finally, print a description of the result in the console.
        /// </summary>
        public async Task StartTextAnalytics()
        {
            // Instanciates the Text Analytics API Response model
            TextAnalyticsResponse = new TextAnalyticsApiResponse();

            State = AwaitStatus.AwaitingResponse;
            // Phase 1
            await GetTextAnalyticsLanguage();
            // Phase 2
            await GetTextAnalyticsKeyPhrases();
            // Phase 3
            await GetTextAnalyticsScore();
            // Request satisfied
            State = AwaitStatus.ResponseReceived;

            // Prints results in the console
            TextAnalyticsResponse.Describe();
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
            TextAnalyticsResponse.Language = _JsonManager.GetLanguageFromJsonResponse(jsonString);
        }

        /// <summary>
        /// Calls the <see cref="ParseJsonResponse(string)"/> method with the specified query,
        /// which sends the request to the Text Analytics API.
        /// Sets the <see cref="TextAnalyticsApiResponse.KeyPhrases"/> of the text once the response is caught.
        /// </summary>
        private async Task GetTextAnalyticsKeyPhrases()
        {
            // Configure request
            JObject body = _JsonManager.GetTextAnalyticsJson(TextToAnalyse, TextAnalyticsResponse.Language.Iso6391Name);
            _RequestManager.SetTextAnalyticsConfiguration("keyPhrases", body);
            // Send Request
            string jsonString = await _RequestManager.MakeRequest();
            // Put the result into the model
            TextAnalyticsResponse.KeyPhrases = _JsonManager.GetKeyPhrasesFromJsonResponse(jsonString);

        }

        /// <summary>
        /// Calls the <see cref="ParseJsonResponse(string)"/> method with the specified query,
        /// which sends the request to the Text Analytics API.
        /// Sets the <see cref="TextAnalyticsApiResponse.Score"/> of the text once the response is caught.
        /// </summary>
        private async Task GetTextAnalyticsScore()
        {
            // Configure request
            JObject body = _JsonManager.GetTextAnalyticsJson(TextToAnalyse, TextAnalyticsResponse.Language.Iso6391Name);
            _RequestManager.SetTextAnalyticsConfiguration("sentiment", body);
            // Send Request
            string jsonString = await _RequestManager.MakeRequest();
            // Put the result into the model
            TextAnalyticsResponse.Score = _JsonManager.GetScoreFromJsonResponse(jsonString);
        }

        #endregion
    }
}
