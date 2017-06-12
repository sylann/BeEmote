using System.Data;

namespace BeEmote.Core
{
    /// <summary>
    /// Interface to allow its use in a mock for the unit tests
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Returns a new connection from the BeEmote connection string constant
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();
    }
}
