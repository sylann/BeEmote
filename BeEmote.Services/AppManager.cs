using BeEmote.Core;
using Newtonsoft.Json.Linq;
using PropertyChanged;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace BeEmote.Services
{
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

        #region Public Methods

        /// <summary>
        /// Send a request with the provided <see cref="ImagePath"/> to the Emotion API.
        /// Finally, print a description of the result in the console.
        /// </summary>
        public async Task StartEmotion()
        {
            // Sends the request and handle the result
            InitStatus();
            await GetEmotionFaces();
            SetEmotionResultStatus();

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

            // Send the request and handle the result
            InitStatus();
            await GetTextAnalyticsLanguage();
            await GetTextAnalyticsKeyPhrases();
            await GetTextAnalyticsScore();
            SetTextAnalyticsResultStatus();

            // Prints results in the console
            TextAnalyticsResponse.Describe();
        }

        #endregion

        #region Private Status Methods

        /// <summary>
        /// Initializes the request status to <see cref="RequestStates.NoData"/>
        /// </summary>
        private void InitStatus()
        {
            State = RequestStates.AwaitingResponse;
        }

        /// <summary>
        /// Sets the request status according to the result:
        /// Either <see cref="RequestStates.ResponseReceived"/>
        /// Or <see cref="RequestStates.EmptyResult"/> if no face was found.
        /// </summary>
        private void SetEmotionResultStatus()
        {
            State = EmotionResponse?.Faces?.Count > 0
               ? RequestStates.ResponseReceived
               : RequestStates.EmptyResult;
        }

        /// <summary>
        /// Sets the request status according to the Text analytics result:
        /// The resulting State is one of <see cref="RequestStates"/>.
        /// </summary>
        private void SetTextAnalyticsResultStatus()
        {
            if (string.IsNullOrEmpty(TextAnalyticsResponse?.Language?.Name))
                State = RequestStates.EmptyResult;
            else if (TextAnalyticsResponse.KeyPhrases?.Count == 0)
                State = RequestStates.PartialResult;
            else
                State = RequestStates.ResponseReceived;
        }

        #endregion

        #region Private Request Methods

        /// <summary>
        /// Configure an emotion request according to the set image url,
        /// then sends the request,
        /// finally put the result in the model.
        /// </summary>
        /// <returns></returns>
        private async Task GetEmotionFaces()
        {
            // Configure a new Request
            if (ImagePath.StartsWith("http"))
                _RequestManager.SetEmotionConfiguration(_JsonManager.GetEmotionJson(ImagePath));
            else
                _RequestManager.SetEmotionConfiguration(ImagePath);

            // Send the request
            string json = await _RequestManager.MakeRequest();
            // Put the result in the model
            EmotionResponse = new EmotionApiResponse(_JsonManager.GetFacesFromJsonResponse(json));
        }

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

        #region Events

        /// <summary>
        /// An event is fired whenever a public property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #endregion
    }
}
