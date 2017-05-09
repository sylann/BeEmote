namespace BeEmote.Core
{
    /// <summary>
    /// Defines the methods that are mandatory to interact 
    /// with the Text Analytics API in particular.
    /// </summary>
    public interface ITextAnalyticsAPI
    {
        /// <summary>
        /// Check the <paramref name="confType"/> and calls the proper method
        /// from the <see cref="RequestManager"/>.
        /// </summary>
        /// <param name="confType">The configuration type: 'languages', 'keyPhrases', 'sentiment'</param>
        /// <returns>A configuration for an TextAnalytics API Request</returns>
        RequestConfiguration Configure(string confType);
        /// <summary>
        /// Updates the value of the Language in the TextAnalytics Model.
        /// Then resolve the state of the App at this stage.
        /// </summary>
        /// <param name="jsonResponse">The response of the Language request</param>
        /// <returns>State of the app</returns>
        RequestStates UpdateLanguage(string jsonResponse);
        /// <summary>
        /// Updates the value of the Key phrases in the TextAnalytics Model.
        /// Then resolve the state of the App at this stage.
        /// </summary>
        /// <param name="jsonResponse">The response of the Key phrases request</param>
        /// <returns>State of the app</returns>
        RequestStates UpdateKeyPhrases(string jsonResponse);
        /// <summary>
        /// Updates the value of the Sentiment in the TextAnalytics Model.
        /// Then resolve the state of the App at this stage.
        /// </summary>
        /// <param name="jsonResponse">The response of the Sentiment request</param>
        /// <returns>State of the app</returns>
        RequestStates UpdateSentiment(string jsonResponse);
    }
}
