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
            // TODO: Produce tests in order to TDD the database part of the application
            // TDD* Test driven development
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
        // - stats for emotions or textanalytics should not change

    }
}
