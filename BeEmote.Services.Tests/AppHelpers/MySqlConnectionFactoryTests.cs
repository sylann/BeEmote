using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeEmote.Services.Tests
{
    [TestClass()]
    public class MySqlConnectionFactoryTests
    {
        [TestMethod]
        public void CreateConnection_To_BeEmote_Database()
        {
            // Arrange
            var DB = new MySqlConnectionFactory();

            // Act
            try
            {
                // (If the connection is not valid, this will throw an exception)
                using (var conn = DB.CreateConnection()) { }
            }

            // Assert
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}