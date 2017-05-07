using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeEmote.Client.WPF.Tests
{
    [TestClass]
    public class ViewModelTests
    {
        #region EmotionView Tests

        // Image source
        // - when starting              should be null
        // - when no image path         should be null
        // - when invalid image path    should be null
        // - when valid image path      should be able to load
        // - when image source not null should be able to analyze

        // Analyze
        // - when started
        // - when success
        // - when fail

        #endregion

        #region rectangle and canvas

        // number of rectangles
        // - when starting should be 0
        // - after loaded image should be 0
        // - after analyse success should not be 0
        // - after analyse fail should be 0

        // canvas size and position
        // - should never be bigger than image container
        // - rectangles should never be out of the canvas

        #endregion

        #region TextAnalytics

        // Not a lot to check here
        // - when starting          elements should be empty
        // - after request success  elements should be filled
        // - after request fail     elements should be empty

        #endregion
    }
}
