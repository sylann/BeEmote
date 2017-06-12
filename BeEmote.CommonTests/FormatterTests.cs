using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeEmote.Common.Tests
{
    [TestClass()]
    public class FormatterTests
    {
        [TestMethod()]
        public void Percent_Returns_Correct_Formatting()
        {
            // Arrange
            var value = 0.549876435d;
            var expected = "54,99%";

            // Act
            var actual = Formatter.Percent(value);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}