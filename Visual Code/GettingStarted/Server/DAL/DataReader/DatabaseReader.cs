using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.Common;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GettingStarted.Server.DAL.DataReader
{
    public class DatabaseReader
    {
        //Note: ExcuteNonQuery return 0 if no statement is update and -1 if error occurs
        private string _connectionString { get; set; }
        private IConfiguration? _configuration { get; set; }
        private List<SqlParameter> _params { get; set; }
        private string _nameOfProcedure { get; set; }

        public DatabaseReader(string nameOfProcedure)
        {
            _params = new List<SqlParameter>();
            _nameOfProcedure = nameOfProcedure;
            Initalize(nameOfProcedure);
        }
        private void Initalize(string nameOfProcedure)
        {
            configureConnectionString();
            checkError(nameOfProcedure, _connectionString);
        }
        private void configureConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connectionString = connectionString;
        }
        private void checkError(string nameOfProcedure, string connectionString)
        {
            if (nameOfProcedure.IsNullOrEmpty() || nameOfProcedure.Length == 0)
            {
                throw new ArgumentNullException("Name Procedure");
            }
            if (connectionString.IsNullOrEmpty() || connectionString.Length == 0)
            {
                throw new ArgumentNullException("ConnectString");
            }
        }
        public void SqlParams(string nameOfParam, SqlDbType sqltype, object value)
        {
            SqlParameter parameter = new SqlParameter(nameOfParam, sqltype);
            parameter.Value = value;
            _params.Add(parameter);
        }
        // return lines
        public int ExcuteNonQuery()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // check if connect is opened
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = _nameOfProcedure;
                            foreach (var param in _params)
                            {
                                command.Parameters.Add(param);
                            }
                            int result = command.ExecuteNonQuery();
                            transaction.Commit();
                            return result;
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Erorr 208: not found procedure in SQL
                        if (ex.Number == 208)
                        {
                            throw new Exception("Procedure: " + _nameOfProcedure + " not found in SQL Server", ex);
                        }
                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }
                        throw new Exception("An error occurred: " + ex.Message, ex);
                    }

                }
            }
        }
        // return the first value in the first line (id)
        public Object ExecuteScalar()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            // check if connect is opened
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = _nameOfProcedure;
                    foreach (var param in _params)
                    {
                        command.Parameters.Add(param);
                    }
                    var result = command.ExecuteScalar();
                    return result;
                }
            }
            catch (SqlException ex)
            {
                // Erorr 208: not found procedure in SQL
                if (ex.Number == 208)
                {
                    throw new Exception("Procedure: " + _nameOfProcedure + " not found in SQL Server", ex);
                }
                throw new Exception("An error occurred: " + ex.Message, ex);
            }
        }

        public SqlDataReader ExcuteReader()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            // check if connect is opened
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = _nameOfProcedure;
                    foreach (var param in _params)
                    {
                        command.Parameters.Add(param);
                    }
                    SqlDataReader result = command.ExecuteReader();
                    return result;
                }
            }
            catch (SqlException ex)
            {
                // Erorr 208: not found procedure in SQL
                if (ex.Number == 208)
                {
                    throw new Exception("Procedure: " + _nameOfProcedure + " not found in SQL Server", ex);
                }
                throw new Exception("An error occurred: " + ex.Message, ex);
            }
        }
    }
}
