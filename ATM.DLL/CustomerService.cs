using ATM.DLL.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ATM.DLL
{
    public class CustomerService : ICustomerInterface
    {
        private readonly AtmDbConnection _dbContext;
        private bool _disposed;
        private string _databaseName = "DbTest";

        public CustomerService(AtmDbConnection dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task CreateCustomerDb()
        {
            var connection = await _dbContext.OpenConnection();


            string checkDatabaseExistenceQuery = $"SELECT * FROM sys.databases WHERE name='{_databaseName}'";

            string query = "CREATE DATABASE MyDatabase ON PRIMARY " +
                 "(NAME = MyDatabase_Data, " +
                 "FILENAME = 'C:\\MyDatabaseData.mdf', " +
                 "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                 "LOG ON (NAME = MyDatabase_Log, " +
                 "FILENAME = 'C:\\MyDatabaseLog.ldf', " +
                 "SIZE = 1MB, " +
                 "MAXSIZE = 5MB, " +
                 "FILEGROWTH = 10%)";


            using (SqlCommand dbCommand = new SqlCommand(query, connection))
            {
                try
                {
                    await dbCommand.ExecuteNonQueryAsync();
                    Console.WriteLine("Database is created sucessfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }



        }

        public async Task CreateCustomerTable()
        {
            var connection = await _dbContext.OpenConnection();

            string tableName = "Customers";
            string checkTableExistenceQuery = $"SELECT * FROM MyDb WHERE TABLE_NAME = '{tableName}'";

            string query = $"CREATE TABLE {tableName} " +
                $"(CustomerID int Primary Key Identity(1,1), " +
                $"CardNumber varchar(50), " +
                $"Pin varchar(50) " +
                ");";

            using (SqlCommand createCommand = new SqlCommand(query, connection))
            {
                try
                {
                    await createCommand.ExecuteNonQueryAsync();
                    Console.WriteLine("Customer table is created successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //await _dbContext.CloseConnection();

        }

        public async Task<int> CreateCustomer(CustomerViewModel customer)
        {
            try
            {
                var connection = await _dbContext.OpenConnection();

                string query = "INSERT INTO Customers (CardNumber, Pin) VALUES(@CardNumber, @Pin) SELECT CAST(SCOPE_IDENTITY() AS BIGINT) ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter parameter = new SqlParameter()
                    {
                        ParameterName = "@CardNumber",
                        Value = customer.CardNumber,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter()
                    {
                        ParameterName = "@Pin",
                        Value = customer.Pin,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                    };
                    command.Parameters.Add(parameter);

                    //int customerId = (int) await command.ExecuteScalarAsync();
                    long customerId = (long)await command.ExecuteScalarAsync();

                    Console.WriteLine((int)customerId);
                    return (int)customerId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed operation");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);

                return 0;
            }

        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
