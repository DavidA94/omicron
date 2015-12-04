using System.Data;

namespace WebService
{
    /// <summary>
    /// A class for holding the necessary data to create a prepared parameter.
    /// </summary>
    public class PreparedData
    {
        /// <summary>
        /// Creates a new PreparedData object
        /// </summary>
        /// <param name="type">The type of data being put into the databse</param>
        /// <param name="data">The data to be put into the database</param>
        /// <param name="length">[Optional] The length of any variable data types (e.g. char, varchar)</param>
        public PreparedData(SqlDbType type, object data, int length = -1)
        {
            Type = type;
            Data = data;
            Length = length;
        }

        /// <summary>
        /// The type of data to be inserted into the database
        /// </summary>
        public SqlDbType Type { get; set; }

        /// <summary>
        /// The data to be inserted into the database.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// The length of any variable data types (e.g. char, varchar) (Less than zero indicates not used)
        /// </summary>
        public int Length { get; set; }
    }
}