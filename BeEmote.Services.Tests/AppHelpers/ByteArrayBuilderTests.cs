using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeEmote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BeEmote.Services.Tests
{
    [TestClass()]
    public class ByteArrayBuilderTests
    {
        [TestMethod()]
        public void FromImagePath_WhenValidImagePath_ReturnByteArray()
        {
            // Arrange
            var imageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "oberyn-wear-helmet.jpg");

            // Act
            var actual = ByteArrayBuilder.FromImagePath(imageFilePath);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(byte[]));
        }

        [TestMethod()]
        public void FromImagePath_WhenInvalidImagePath_ReturnNull()
        {
            // Arrange
            var imageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "invalid-image-path.jpg");

            // Act
            var actual = ByteArrayBuilder.FromImagePath(imageFilePath);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void FromJsonObject_When_Return_CheckType()
        {
            //Arrange
            string json = "hello";
            //Act
            var result = Encoding.UTF8.GetBytes(json);

            //Assert
            Assert.IsInstanceOfType(result, typeof(byte[]));
        }
        [TestMethod()]

        public void FromJsonObject_When_Return_CheckStringContent()
        {
            //Arrange
            JObject toCompare = new JObject();
            var result = ByteArrayBuilder.FromJsonObject(toCompare);
            var resultFromTest = Encoding.UTF8.GetBytes(toCompare.ToString());
            var textResult = "";
            var secondTextResult = "";
            //Act

            foreach (var byteItem in result)
            {
                textResult += byteItem.ToString();
            }

            foreach (var byteItem in resultFromTest)
            {
                secondTextResult += byteItem.ToString();
            }
            //Assert
            Assert.AreEqual(textResult, secondTextResult);
        }
    }
}