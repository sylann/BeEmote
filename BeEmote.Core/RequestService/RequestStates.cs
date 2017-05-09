namespace BeEmote.Core
{
    /// <summary>
    /// Possible status of the Application regarding API request/response
    /// </summary>
    public enum RequestStates
    {
        /// <summary>
        /// Initial state, no data has been given yet.
        /// </summary>
        NoData,
        /// <summary>
        /// The request has been sent but no response has arrived yet.
        /// </summary>
        AwaitingResponse,
        /// <summary>
        /// The response has been received, the result is not empty
        /// and there are no errors.
        /// </summary>
        ResponseReceived,
        /// <summary>
        /// The response has been received
        /// but there are errors or the result is empty.
        /// </summary>
        EmptyResult,
        /// <summary>
        /// The response has been received
        /// but is not complete.
        /// </summary>
        PartialResult
    }
}
