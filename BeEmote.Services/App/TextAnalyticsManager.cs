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
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BeEmote.Services
{
    /// <summary>
    /// Handles the global functionning of the application with the Text Analytics API Context
    /// </summary>
    public class TextAnalyticsManager : ICognitiveApp<TextAnalyticsApiResponse, TextAnalyticsStats>, ITextAnalyticsAPI, INotifyPropertyChanged
    {
        #region Members

        /// <summary>
        /// This is the text to send to the Text Analytics API
        /// </summary>
        public string TextToAnalyse { get; set; }

        #endregion

        #region ICognitiveApp implementation

        /// <summary>
        /// Contains the model for a typical Text Analytics API Response.
        /// </summary>
        public TextAnalyticsApiResponse Response { get; set; }

        /// <summary>
        /// Contains the model for the statistics of the textanalysis table.
        /// </summary>
        public TextAnalyticsStats Stats { get; set; }

        /// <summary>
        /// Indicates the state in which the app currently is
        /// </summary>
        public RequestStates State { get; set; } = RequestStates.NoData;

        /// <summary>
        /// Executes 3 requests to the Text Analytics API in a defined order.
        /// 1 get the language, 2 get the key phrases, 3 get the sentiment of
        /// the <see cref="TextToAnalyse"/>.
        /// For each of the 3 phases, first configure, then send the request
        /// Wait for the result and resolve it. Finally, update the database.
        /// </summary>
        public async Task Start()
        {
            Response = new TextAnalyticsApiResponse();
            RequestConfiguration config;
            string jsonResponse;

            State = RequestStates.AwaitingResponse;

            // phase 1
            config = Configure("languages");
            jsonResponse = await SendRequest(config);
            State = UpdateLanguage(jsonResponse);
            // phase 2
            config = Configure("keyPhrases");
            jsonResponse = await SendRequest(config);
            State = UpdateKeyPhrases(jsonResponse);
            // phase 3
            config = Configure("sentiment");
            jsonResponse = await SendRequest(config);
            State = UpdateSentiment(jsonResponse);

            // Prints results in the console
            Response.Describe();

            // TODO: extract method + interface?
            // Insert into database
            var DB = new DataAccess();
            var dbSuccess = DB.UpdateTextAnalytics(Response.Language, Response.Score, TextToAnalyse);

            // Get stats
            Stats = DB.GetTextAnalyticsStats();
            Console.WriteLine(Stats);

            // Resolve final state
            if (dbSuccess)
                State = RequestStates.DatabaseUpdated;
        }

        /// <summary>
        /// Reset the <see cref="State"/> to its default value.
        /// Should be executed between each call to <see cref="Start"/>.
        /// </summary>
        /// <returns>The resulting state</returns>
        public RequestStates Reset()
        {
            // Set start up state and return it
            return State = RequestStates.NoData;
        }

        /// <summary>
        /// Awaits and returns the result of a configured request to the TextAnalytics API.
        /// </summary>
        /// <param name="config">A configuration for the request</param>
        /// <returns>json response of the API</returns>
        public async Task<string> SendRequest(RequestConfiguration config)
        {
            var Req = new RequestManager();
            return await Req.MakeRequest(config);
        }

        #endregion

        #region ITextAnalyticsAPI implementation

        /// <summary>
        /// Check the <paramref name="confType"/> and calls the proper method
        /// from the <see cref="RequestManager"/>.
        /// </summary>
        /// <param name="confType">The configuration type: 'languages', 'keyPhrases', 'sentiment'</param>
        /// <returns>A configuration for an TextAnalytics API Request</returns>
        public RequestConfiguration Configure(string confType)
        {
            var Req = new RequestManager();
            if (confType == "languages")
                return Req.GetTextAnalyticsConfiguration(confType, TextToAnalyse);

            if (confType == "keyPhrases" ||
                confType == "sentiment")
                return Req.GetTextAnalyticsConfiguration(confType, TextToAnalyse, Response.Language.Iso6391Name);

            throw new ArgumentException($"Unexpected {nameof(confType)}. Should be 'languages', 'keyPhrases' or 'sentiment'");
        }

        /// <summary>
        /// Updates the value of the Language in the TextAnalytics Model.
        /// Then resolve the state of the App at this stage.
        /// </summary>
        /// <param name="jsonResponse">The response of the Language request</param>
        /// <returns>State of the app</returns>
        public RequestStates UpdateLanguage(string jsonResponse)
        {
            var json = new JsonManager();
            // Updates the model
            Response.Language = json.GetLanguageFromJson(jsonResponse);
            Response.FormattedLanguageUpdated();

            // resolve the current state
            var result = string.IsNullOrEmpty(Response?.Language?.Name)
                ? RequestStates.EmptyResult
                : RequestStates.PartialResult;
            return result;
        }

        /// <summary>
        /// Updates the value of the Key phrases in the TextAnalytics Model.
        /// Then resolve the state of the App at this stage.
        /// </summary>
        /// <param name="jsonResponse">The response of the Key phrases request</param>
        /// <returns>State of the app</returns>
        public RequestStates UpdateKeyPhrases(string jsonResponse)
        {
            var json = new JsonManager();
            // Updates the model
            Response.KeyPhrases = json.GetKeyPhrasesFromJson(jsonResponse);
            // resolve the current state
            return RequestStates.PartialResult;
        }

        /// <summary>
        /// Updates the value of the Sentiment in the TextAnalytics Model.
        /// Then resolve the state of the App at this stage.
        /// </summary>
        /// <param name="jsonResponse">The response of the Sentiment request</param>
        /// <returns>State of the app</returns>
        public RequestStates UpdateSentiment(string jsonResponse)
        {
            var json = new JsonManager();
            // Updates the model
            Response.Score = json.GetScoreFromJson(jsonResponse);
            // resolve the current state (if any info is lacking => partial result)
            return Response.KeyPhrases?.Count == 0
                || Response.Score == null
                   ? RequestStates.PartialResult
                   : RequestStates.ResponseReceived;
        }
        
        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #endregion
    }
}
