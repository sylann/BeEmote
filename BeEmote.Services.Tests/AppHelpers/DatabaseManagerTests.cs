using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Data;

namespace BeEmote.Services.Tests
{
    [TestClass]
    public class DatabaseManagerTests
    {
        [TestMethod]
        public void DatabaseManager_WhenDatabaseExists_ConnectionWorks()
        {
            // arrange
            IDbConnection conn = new MySqlConnection(DatabaseManager.MySql_BeEmote);
            var noExceptionOccured = true;

            // act
            conn.Open();
            // If the connection is not valid, this will throw an exception
            conn.Close();
            // Assert
            Assert.IsTrue(noExceptionOccured); // LOL

        }

        [TestMethod]
        public void DatabseManager_WhenDatabaseNotExists_ThrowException()
        {
            try
            {
                IDbConnection conn = new MySqlConnection(DatabaseManager.Connect("myNewDatabase"));
                Assert.Fail("Should have thrown a NullReferenceException");
            }
            catch (NullReferenceException) { } // Any of this kind of exception is caught as expected
            // any other kind of exception make this test fail
        }
    }
}
