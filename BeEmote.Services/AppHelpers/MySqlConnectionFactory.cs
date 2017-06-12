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

using BeEmote.Core;
using MySql.Data.MySqlClient;
using System.Data;

namespace BeEmote.Services
{
    /// <summary>
    /// Connection factory implementing the connection interface
    /// </summary>
    public class MySqlConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// Returns a new connection from the BeEmote connection string constant
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionStrings.MysqlBeEmote);
        }
    }
}
