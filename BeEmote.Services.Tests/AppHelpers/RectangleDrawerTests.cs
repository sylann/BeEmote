using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeEmote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeEmote.Services.Tests
{
    [TestClass()]
    public class RectangleDrawerTests
    {
        // Tested class instance
        private readonly RectangleDrawer drawer = new RectangleDrawer();

        [TestMethod()]
        public void LeftResize_When_Return()
        {
            //Arrange
            int left = 20;
            double canvaWidth = 10;
            double imageWidth = 8;
            double initialWidth = 4;
            double offset = 3;
            double result1;
            double result2;

            //Act
            result1 = (canvaWidth - imageWidth) / 2;
            result2 = offset + (left / initialWidth / imageWidth);

            //Assert
            Assert.AreEqual(result1, 1);
            Assert.AreEqual(result2, 3.625);
        }

        [TestMethod()]
        public void TopResize_When_Return()
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
        public void WidthResize_When_Return()
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
        public void HeightResize_When_Return()
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
    }
}