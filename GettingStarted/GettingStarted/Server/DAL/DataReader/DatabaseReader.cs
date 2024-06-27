using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Data.Common;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GettingStarted.Server.DAL.DataReader
{
    public class DatabaseReader
    {
        public static readonly int THOI_GIAN_HUY_BO = 10; // (giây), là n giây connection pool tồn tại và sẽ được hủy bỏ sau n giây đó
        private string? _connectionString { get; set; }
        private IConfiguration? _configuration { get; set; }
        private List<SqlParameter> _params { get; set; }
        private string _nameOfProcedure { get; set; }
        private SqlConnection? connection { get; set; }

        public DatabaseReader(string nameOfProcedure)
        {
            _params = new List<SqlParameter>();
            _nameOfProcedure = nameOfProcedure;
            Initalize(nameOfProcedure);
            connection = new SqlConnection(_connectionString);
        }
        private void Initalize(string nameOfProcedure)
        {
            configureConnectionString();
            if(_connectionString != null)
                checkError(nameOfProcedure, _connectionString);
        }
        private void configureConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            if(connectionString != null)
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
            // check if connect is opened
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            if(connection != null)
            {
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
                    finally
                    {
                        Dispose();
                    }
                }
            }
            return -1;
        }
        // return the first value in the first line (id)
        public Object ExecuteScalar()
        {
            // check if connect is opened
            if (connection!= null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                if(connection != null)
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
                throw new Exception("Can not read");
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
            finally
            {
                // kết nối chỉ được 10 giây để đọc, sau đó sẽ được đóng kết nối lại để tránh rò rỉ connection
                Task.Run(async () => // Tiếp tục thực hiện các công việc khác mà không cần phải chờ kết nối được đóng.
                {
                    await Task.Delay(TimeSpan.FromSeconds(THOI_GIAN_HUY_BO));
                    Dispose();
                });
            }
        }

        public SqlDataReader ExcuteReader()
        {
            // check if connect is opened
            if (connection!= null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                if(connection != null)
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
                throw new Exception("Can not read");
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
            finally
            {
                // kết nối chỉ được 10 giây để đọc, sau đó sẽ được đóng kết nối lại để tránh rò rỉ connection
                Task.Run(async () => // Tiếp tục thực hiện các công việc khác mà không cần phải chờ kết nối được đóng.
                {
                    await Task.Delay(TimeSpan.FromSeconds(THOI_GIAN_HUY_BO));
                    Dispose();
                });
            }
        }
        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }
    }
}
