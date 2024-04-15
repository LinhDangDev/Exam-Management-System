using Microsoft.Data.SqlClient;
using System.Data;

namespace GettingStarted.Server.BUS
{
    public class DatabaseService
    {
        private readonly string? _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //public SqlCommand CreateSqlCommand(string commandText)
        //{
        //    SqlConnection connection = new SqlConnection(_connectionString);
        //    SqlCommand command = new SqlCommand(commandText, connection);
        //    command.ExecuteNonQueryAsync();
        //    return command;
        //}

        public SqlCommand CreateSqlCommandProcedure(string nameOfProcedure, string variables)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            if(connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand("exec " + nameOfProcedure + " " + variables, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQueryAsync();
            return command;
        }
    }
}
