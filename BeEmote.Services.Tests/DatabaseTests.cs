using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeEmote.Services.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void DatabaseExists()
        {
            // TODO: Tests for the database layer. How?
            Assert.Fail();
        }

        // after Emotion request
        // - number of calls should increase by 1
        // - number of global calls should increase by 1

        // after TextAnalytics request
        // - number of calls should increase by 1
        // - number of global calls should increase by 1

        // after Failed request
        // - number of fail request should increase by 1
        // - stats for emotions or text analytics should not change

    }
}
