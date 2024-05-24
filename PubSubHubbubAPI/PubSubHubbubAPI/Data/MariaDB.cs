using MySql.Data.MySqlClient;

namespace PubSubHubbubAPI.Data
{
    public static class MariaDB
    {
        public static void InsertToMariaDB(string message)
        {
            string? connection = Environment.GetEnvironmentVariable("DATABASE_CONNECTION");

            string commandtext;
            //var cs = @"";
            MySqlConnection dbConn = new(connection);
            dbConn.Open();
            commandtext = $"INSERT INTO InboundMessages (MessageText) VALUES(?message);";
            //cmd = new MySqlCommand(commandtext, dbConn);
            MySqlCommand cmd = dbConn.CreateCommand();
            cmd.CommandText = commandtext;
            cmd.Parameters.Add("?message", MySqlDbType.MediumText).Value = message;
            cmd.ExecuteNonQuery();
            dbConn.Close();

        }
    }
}
