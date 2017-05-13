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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeEmote.Services
{
    public class RectangleDrawer
    {

        /// <summary>
        /// Updates the distance between canvas's and image's lefts.
        /// </summary>
        /// <param name="left">actual distance</param>
        /// <param name="canvasWidth">actual width of the canvas</param>
        /// <param name="imageWidth">actual width of the image</param>
        /// <param name="initialWidth">initial width of the image</param>
        /// <returns>Updated distance</returns>
        public double LeftResize(int left, double canvasWidth, double imageWidth, double initialWidth)
        {
            double offset = (canvasWidth - imageWidth) / 2;
            return offset + WidthResize(left, initialWidth, imageWidth);
        }

        /// <summary>
        /// Updates the distance between canvas's and image's tops
        /// </summary>
        /// <param name="top">actual distance between canvas's and image's tops</param>
        /// <param name="canvasHeight">actual height of the canvas</param>
        /// <param name="imageHeight">actual height of the image</param>
        /// <param name="initialHeight">initial height of the image</param>
        /// <returns>Updated distance</returns>
        public double TopResize(int top, double canvasHeight, double imageHeight, double initialHeight)
        {
            double offset = (canvasHeight - imageHeight) / 2;
            return offset + HeightResize(top, initialHeight, imageHeight);
        }

        /// <summary>
        /// Re Evaluates a horizontal dimension after a resize of the canvas
        /// </summary>
        /// <param name="xValue">A horizontal dimension (left position or width)</param>
        /// <param name="initialWidth">initial width of the image</param>
        /// <param name="actualWidth">actual width of the image</param>
        /// <returns>The new dimension</returns>
        public double WidthResize(double xValue, double initialWidth, double actualWidth)
        {
            double HorizontalRatio = initialWidth / actualWidth;
            return xValue / HorizontalRatio;
        }

        /// <summary>
        /// Re Evaluates a vertical dimension after a resize of the canvas
        /// </summary>
        /// <param name="yValue">A vertical dimension (top position or height)</param>
        /// <param name="initialHeight">initial height of the image</param>
        /// <param name="actualHeight">actual height of the image</param>
        /// <returns>The new dimension</returns>
        public double HeightResize(double yValue, double initialHeight, double actualHeight)
        {
            double VerticalRatio = initialHeight / actualHeight;
            return yValue / VerticalRatio;
        }
    }
}
