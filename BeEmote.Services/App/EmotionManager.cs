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
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace BeEmote.Services
{
    /// <summary>
    /// Handles the global functioning of the application with the Emotion API Context
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
            var jsonString = await SendRequest(config);
            // Resolve result
            State = HandleResult(jsonString);
            // Console print the results.
            Response?.Describe();
            
            // TODO: extract method + interface?
            // Insert into database
            var DB = new DataAccess();
            var dbSuccess = DB.UpdateEmotion(Response.Faces, ImagePath);
            // Resolve final state
            if (dbSuccess)
                State = RequestStates.DatabaseUpdated;
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
        /// Updates the model according to the result of the
        /// Emotion <paramref name="jsonResponse"/>.
        /// Returns the State of the App.
        /// </summary>
        /// <param name="jsonResponse">A response of the Emotion API</param>
        /// <returns>The state of the app</returns>
        public RequestStates HandleResult(string jsonResponse)
        {
            // Put the result in the model
            var Json = new JsonManager();
            List<Face> Faces = Json.GetFacesFromJson(jsonResponse);
            Response = new EmotionApiResponse(Faces);
            // Check response
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
            var req = new RequestManager();
            return req.GetEmotionConfiguration(imagePath);
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        /// <summary>
        /// An event that is fired whenever a public property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #endregion
    }
}
