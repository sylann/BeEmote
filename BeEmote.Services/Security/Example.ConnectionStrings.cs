namespace BeEmote.Services.Example
{
    /// <summary>
    /// Provides a connection string to use in the App.config file 
    /// of any relevant Client."Interface_Project".
    /// </summary>
    internal static class ConnectionStrings
    {
        /// <summary>
        /// This is an example connection string for Mysql.
        /// Replace the following by your own values:
        /// - Host_Name (server where the database is: either localhost or your_domain.com)
        /// - Database_Name
        /// - Database_User_Name
        /// - Database_User_Password
        /// </summary>
        internal const string MysqlBeEmote = "Server=Host_Name; Database=Database_Name; Uid=Database_User_Name; Pwd=Database_User_Password;";
    }
}
