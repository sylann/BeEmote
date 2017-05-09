using BeEmote.Core;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace BeEmote.Services
{
    /// <summary>
    /// Handles the global functionning of the application with the Emotion API Context
    /// </summary>
    [ImplementPropertyChanged]
    public class EmotionManager : ICognitiveApp<EmotionApiResponse>, IEmotionAPI, INotifyPropertyChanged
    {

        #region Members
        
        /// <summary>
        /// Path of the image to send to the Emotion API.
        /// Either a local image path or a remote url.
        /// </summary>
        public string ImagePath { get; set; }
        
        /// <summary>
        /// Provides the parent folder of the image path,
        /// but only if it is neither null nor an url.
        /// </summary>
        public string ImageFolder
        {
            get => ImagePath != null && !Uri.IsWellFormedUriString(ImagePath, UriKind.Absolute)
                ? Directory.GetParent(ImagePath).ToString()
                : null;
        }

        #endregion

        #region ICognitiveApp implementation

        /// <summary>
        /// Contains the model for a typical Emotion API Response.
        /// </summary>
        public EmotionApiResponse Response { get; set; }

        /// <summary>
        /// Indicates the state in which the app currently is
        /// </summary>
        public RequestStates State { get; set; } = RequestStates.NoData;

        /// <summary>
        /// Send a request to the Emotion API and fully handle the results.
        /// Finally, print a description of the result in the console.
        /// </summary>
        public async Task Start()
        {
            State = RequestStates.AwaitingResponse;
            // Configure, send the request and wait for the result
            var config = Configure(ImagePath);
            var json = await SendRequest(config);
            // Resolve result
            State = HandleResult(json);
            // Console print the results.
            Response?.Describe();

            // TODO: UpdateDataBase
            // TODO: GetStatistics
        }

        /// <summary>
        /// Reset the <see cref="State"/> to its default value.
        /// Should be executed between each call to <see cref="Start"/>.
        /// </summary>
        /// <returns>The resulting state</returns>
        public RequestStates Reset()
        {
            // Set start up state
            return State = RequestStates.NoData;
        }

        /// <summary>
        /// Awaits and returns the result of a configured request to the TextAnalytics API.
        /// </summary>
        /// <param name="config">A configuration for the request</param>
        /// <returns>json response of the API</returns>
        public async Task<string> SendRequest(RequestConfiguration config)
        {
            RequestManager Req = new RequestManager();

            return await Req.MakeRequest(config);
        }

        /// <summary>
        /// Updates the model acccording to the result of the
        /// Emotion <paramref name="jsonResponse"/>.
        /// Returns and Set the State of the App.
        /// </summary>
        /// <param name="jsonResponse">A response of the Emotion API</param>
        /// <returns>The state of the app</returns>
        public RequestStates HandleResult(string jsonResponse)
        {
            // Put the result in the model
            JsonManager Json = new JsonManager();
            List<Face> Faces = Json.GetFacesFromJson(jsonResponse);
            Response = new EmotionApiResponse(Faces);
            // Resolve final state
            return Response?.Faces?.Count == 0
                ? RequestStates.EmptyResult
                : RequestStates.ResponseReceived;
        }

        #endregion

        #region IEmotionAPI implementation

        /// <summary>
        /// Calls the <see cref="RequestManager.GetEmotionConfiguration(string)"/> method.
        /// </summary>
        /// <param name="imagePath">The path of the image to analyse</param>
        /// <returns>A configuration for an Emotion API Request</returns>
        public RequestConfiguration Configure(string imagePath)
        {
            RequestManager Req = new RequestManager();
            return Req.GetEmotionConfiguration(imagePath);
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        /// <summary>
        /// An event is fired whenever a public property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #endregion
    }
}
