using BeEmote.Core;
using BeEmote.Services;
using System;

namespace BeEmote.Client.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // StartEmotion();
            // StartEmotionSync();
            StartTextAnalytics();
            // StartTextAnalyticsSync();

            Console.ReadLine();
        }

        #region Emotion Helper Methods

        /// <summary>
        /// Immediately send a request to the cognitive service.
        /// Use a local image which path is already set.
        /// Logs the description of the result in the console.
        /// </summary>
        static async void StartEmotion()
        {
            RequestManager Manager = new RequestManager();
            string json = await Manager.MakeEmotionRequest(@"G:\MyPics\self\20161214_124159.jpg");

            EmotionApiResponse Response = new EmotionApiResponse(json);
            Response.Describe();
        }

        /// <summary>
        /// Use a locally available example JSON String.
        /// Builds the Emotion Data without sending a request to the Cognitive service
        /// Logs the description of the result in the console.
        /// </summary>
        static void StartEmotionSync()
        {
            // Build an example string
            string json = JsonExamples.GetEmotionAPIResult1();
            //string jsonExample2 = JsonExamples.GetEmotionAPIResult2();

            EmotionApiResponse Response = new EmotionApiResponse(json);
            Response.Describe();
        }

        #endregion

        #region Text Analytics Helper Methods

        /// <summary>
        /// Send a request to the Text Analytics API.
        /// Makes use of fake Json bodies (TODO: Generate the json body from each new response).
        /// Logs the description of the result in the console.
        /// </summary>
        static async void StartTextAnalytics()
        {
            // Initialization
            RequestManager M = new RequestManager();
            string result;
            string JsonRequest = @"{""documents"": [{""id"":0,""text"":""CONTENT""}]}";
            JsonRequest = JsonRequest.Replace("CONTENT", JsonExamples.GetEnglishText());

            // Phase 1
            result = await M.MakeTextAnalyticsRequest("languages", JsonRequest);
            TextAnalyticsApiResponse TA = new TextAnalyticsApiResponse(result);
            Console.WriteLine($"{result}\n");

            // Update Request Content (méthode bourrin)
            JsonRequest = JsonRequest.Replace("[{", "[{"+ @"""language"":""" + TA.Language.Iso6391Name + @""",");

            // Phase 2
            result = await M.MakeTextAnalyticsRequest("keyPhrases", JsonRequest);
            TA.UpdateKeyPhrases(result);
            Console.WriteLine($"{result}\n");

            // Phase 3
            result = await M.MakeTextAnalyticsRequest("sentiment", JsonRequest);
            TA.UpdateScore(result);
            Console.WriteLine($"{result}\n");
        }

        /// <summary>
        /// Use a locally available example JSON String.
        /// Builds the Emotion Data without sending a request to the API.
        /// Logs the description of the result in the console.
        /// </summary>
        static void StartTextAnalyticsSync()
        {
            // Build an example string
            string result;

            // Phase 1
            result = JsonExamples.GetTextLanguage();
            TextAnalyticsApiResponse TA = new TextAnalyticsApiResponse(result);
            Console.WriteLine($"{result}\n");

            // Phase 2
            result = JsonExamples.GetTextKeyPhrases();
            TA.UpdateKeyPhrases(result);
            Console.WriteLine($"{result}\n");

            // Phase 3
            result = JsonExamples.GetTextSentiment();
            TA.UpdateKeyPhrases(result);
            Console.WriteLine($"{result}\n");


        }

        #endregion
    }
}
