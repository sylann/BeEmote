using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeEmote.Services.Tests
{
    [TestClass()]
    public class RectangleDrawerTests
    {
        // Tested class instance
        private readonly RectangleDrawer drawer = new RectangleDrawer();

        [TestMethod()]
        public void LeftResize_Change_With_ValidValues()
        {
            //Arrange
            var drawer = new RectangleDrawer();
            int left = 20;
            double canvasWidth = 10;
            double imageWidth = 8;
            double initialWidth = 4;
            var expected = 0.5d;

            //Act
            var actual = drawer.LeftResize(left, canvasWidth, imageWidth, initialWidth);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LeftResize_Change_With_InvalidValues()
        {
            //Arrange
            int left = 5;
            double canvaWidth = 2;
            double imageWidth = 8;
            double initialWidth = 4;
            double offset = 3;
            double result1;
            double result2;

            //Act
            result1 = (canvaWidth - imageWidth) / 2;
            result2 = offset + (left / initialWidth / imageWidth);

            //Assert
            Assert.AreNotEqual(result1, 1);
            Assert.AreNotEqual(result2, 3.625);
        }

        [TestMethod()]
        public void TopResize_Change_With_ValidValues()
        {
            //Arrange
            int top = 20;
            double canvasHeight = 10;
            double imageHeight = 8;
            double initalHeight = 4;
            double offset = 3;
            double result1;
            double result2;

            //Act
            result1 = (canvasHeight - imageHeight) / 2;
            result2 = offset + (top / initalHeight / imageHeight);

            //Assert
            Assert.AreEqual(result1, 1);
            Assert.AreEqual(result2, 3.625);
        }

        [TestMethod()]
        public void TopResize_Change_With_InvalidValues()
        {
            //Arrange
            int top = 1;
            double canvasHeight = 3;
            double imageHeight = 8;
            double initalHeight = 4;
            double offset = 3;
            double result1;
            double result2;

            //Act
            result1 = (canvasHeight - imageHeight) / 2;
            result2 = offset + (top / initalHeight / imageHeight);

            //Assert
            Assert.AreNotEqual(result1, 1);
            Assert.AreNotEqual(result2, 3.625);
        }

        [TestMethod()]
        public void WidthResize_Change_With_ValidValues()
        {
            //Arrange 
            double xValue = 10;
            double initialWidth = 5;
            double actualWidth = 2;
            //Act 
            double result = xValue / initialWidth / actualWidth;

            //Assert
            Assert.AreEqual(result, 1);
        }

        [TestMethod()]
        public void WidthResize_Change_With_InvalidValues()
        {
            //Arrange 
            double xValue = 3;
            double initialWidth = 5;
            double actualWidth = 2;
            //Act 
            double result = xValue / initialWidth / actualWidth;

            //Assert
            Assert.AreNotEqual(result, 1);
        }

        [TestMethod()]
        public void HeightResize_Change_With_ValidValues()
        {
            //Arrange
            double yValue = 10;
            double initialHeight = 5;
            double actualHeight = 2;

            //Act
            double result = yValue / initialHeight / actualHeight;

            //Assert
            Assert.AreEqual(result, 1);
        }

        [TestMethod()]
        public void HeightResize_Change_With_InvalidValues()
        {
            //Arrange
            double yValue = 2;
            double initialHeight = 5;
            double actualHeight = 2;

            //Act
            double result = yValue / initialHeight / actualHeight;

            //Assert
            Assert.AreNotEqual(result, 1);
        }
    }
}