using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeEmote.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using BeEmote.Core;

namespace BeEmote.Services.Tests
{
    [TestClass()]
    public class JsonTests
    {
        private readonly JsonManager jsonManager = new JsonManager();
        
        [TestMethod()]
        public void GetEmotionJson_ShouldProvideValidEmotionRequestJson()
        {
            // Arrange
            string expected = @"{""url"":""http://www.google.com""}";
            // Act
            string actual = JsonConvert.SerializeObject(jsonManager.GetEmotionJson("http://www.google.com"));
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTextAnalyticsJson_ShouldProvideValidTextAnalyticsRequestJson()
        {
            // Arrange
            string expected = @"{""documents"":[{""id"":0,""text"":""MyAmazingText""}]}";
            // Act
            string actual = JsonConvert.SerializeObject(jsonManager.GetTextAnalyticsJson("MyAmazingText"));
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTextAnalyticsJson_WithLanguage_ShouldProvideValidTextAnalyticsRequestJson()
        {
            // Arrange
            var expectedEnglish = @"{""documents"":[{""id"":0,""text"":""My Amazing Text"",""language"":""English""}]}";
            var expectedEspanol = @"{""documents"":[{""id"":0,""text"":""Ni siquiera hablaba una palabra de español"",""language"":""Espanol""}]}";
            var expectedVoid = @"{""documents"":[{""id"":0,""text"":"""",""language"":""""}]}";
            
            // Act
            var actualEnglish = JsonConvert.SerializeObject(jsonManager.GetTextAnalyticsJson("My Amazing Text", "English"));
            var actualEspanol = JsonConvert.SerializeObject(jsonManager.GetTextAnalyticsJson("Ni siquiera hablaba una palabra de español", "Espanol"));
            var actualVoid = JsonConvert.SerializeObject(jsonManager.GetTextAnalyticsJson("", ""));
            // Assert
            Assert.AreEqual(expectedEnglish, actualEnglish);
            Assert.AreEqual(expectedEspanol, actualEspanol);
            Assert.AreEqual(expectedVoid, actualVoid);
        }

        [TestMethod()]
        public void GetFacesFromJsonResponse_WhenValidJsonResponse_ReturnsListOfFace()
        {
            // Arrange
            string jsonResponse = @"[{""faceRectangle"":{""left"":488,""top"":263,""width"":148,""height"":148},""scores"":{""anger"":9.075572e-13,""contempt"":7.048959e-9,""disgust"":1.02152783e-11,""fear"":1.778957e-14,""happiness"":0.9999999,""neutral"":1.31694478e-7,""sadness"":6.04054263e-12,""surprise"":3.92249462e-11}}]";
            int expectedNumberOfFaces = 1;
            int expectedLeft = 488;
            //var expected = new List<Face>{new Face{FaceRectangle=new FaceRectangle{Left=488,Top=263,Width=148,Height=148},Scores=new Scores{Anger=9.075572e-13,Contempt=7.048959e-9,Disgust=1.02152783e-11,Fear=1.778957e-14,Happiness=0.9999999,Neutral=1.31694478e-7,Sadness=6.04054263e-12,Surprise=3.92249462e-11}}};
            // Act
            var instance = jsonManager.GetFacesFromJson(jsonResponse);
            // Assert
            Assert.IsInstanceOfType(instance,typeof(List<Face>));
            Assert.AreEqual(expectedNumberOfFaces, instance.Count);
            Assert.AreEqual(expectedLeft, instance[0].FaceRectangle.Left);
        }

        [TestMethod()]
        public void GetFacesFromJsonResponse_WhenInvalidJsonResponse_ReturnsNull()
        {
            // Arrange
            string jsonResponse = @"[{"":263,""width"":148,""height"":148},""scores"":{""anger"":9.075572e-13,""contempt"":7.048959e-9,""disgust"":1.02152783e-11,""fear"":1.778957e-14,""happiness"":0.9999999,""neutral"":1.31694478e-7,""sadness"":6.04054263e-12,""surprise"":3.92249462e-11}}]";
            // Act
            var actual = jsonManager.GetFacesFromJson(jsonResponse);
            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void GetLanguageFromJsonResponse_WhenValidEnglishResponse_ReturnsEnglish()
        {
            // Arrange
            var validResponse = @"{""documents"":[{""id"":""0"",""detectedLanguages"":[{""name"":""English"",""iso6391Name"":""en"",""score"":1}]}],""errors"":[]}";
            var expected = new Language { Name = "English", Iso6391Name = "en", Score = 1 };
            // Act
            var actual = jsonManager.GetLanguageFromJson(validResponse);
            // Assert
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Iso6391Name, actual.Iso6391Name);
            Assert.AreEqual(expected.Score, actual.Score);
        }

        [TestMethod()]
        public void GetLanguageFromJsonResponse_WhenInvalidEnglishResponse_ReturnsNull()
        {
            // Arrange
            var invalidResponse = @"{""doc0"",""detectedLanguages"":[{""name"":""English"",""iso6391Name"":""en"",""score"":1}]}],""errors"":[]}";
            var errorResponse = @"{""documents"":[],""errors"":[{""id"":""0"",""message"":""invalid""}]}";
            // Act
            var invalidActual = jsonManager.GetLanguageFromJson(invalidResponse);
            var errorActual = jsonManager.GetLanguageFromJson(errorResponse);
            // Assert
            Assert.IsNull(invalidActual);
            Assert.IsNull(errorActual);
        }

        [TestMethod()]
        public void GetKeyPhrasesFromJsonResponse_WhenValidKeyResponse_ReturnsListOfString()
        {
            // Arrange
            var validResponse = @"{""documents"":[{""keyPhrases"":[""Excellent"",""unbelievable""],""id"":""0""}],""errors"":[]}";
            var expected = new List<string> { "Excellent", "unbelievable" };
            // Act
            var actual = jsonManager.GetKeyPhrasesFromJson(validResponse);
            // Assert
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

        [TestMethod()]
        public void GetKeyPhrasesFromJsonResponse_WhenInvalidKeyResponse_ReturnsNull()
        {
            // Arrange
            var invalidResponse = @"""documents"":[{""keyPhrases"":[""Excellent"",""unbelievable""],""id"":""0""}],""errors"":[]}";
            var errorResponse = @"{""documents"":[],""errors"":[{""id"":""0"",""message"":""invalid""}]}";
            // Act
            var invalidActual = jsonManager.GetKeyPhrasesFromJson(invalidResponse);
            var errorActual = jsonManager.GetKeyPhrasesFromJson(errorResponse);
            // Assert
            Assert.IsNull(invalidActual);
            Assert.IsNull(errorActual);
        }

        [TestMethod()]
        public void GetScoreFromJsonResponse_WhenValidScoreResponse_ReturnsDouble()
        {
            // Arrange
            var validResponse = @"{""documents"":[{""score"":0.995092326472033,""id"":""0""}],""errors"":[]}";
            var expected = 0.995092326472033;
            // Act
            var actual = jsonManager.GetScoreFromJson(validResponse);
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetScoreFromJsonResponse_WhenInvalidScoreResponse_ReturnsNull()
        {
            // Arrange
            var invalidResponse = @"{""documents"":""score"":0.995092326472033,""id"":""0""}],""errors"":[]}";
            var errorResponse = @"{""documents"":[],""errors"":[{""id"":""0"",""message"":""invalid""}]}";
            // Act
            var invalidActual = jsonManager.GetScoreFromJson(invalidResponse);
            var errorActual = jsonManager.GetScoreFromJson(errorResponse);
            // Assert
            Assert.IsNull(invalidActual);
            Assert.IsNull(errorActual);
        }
    }
}
