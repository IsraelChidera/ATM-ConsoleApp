using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ATM.DLL
{
    public class CustomerService : ICustomerInterface
    {
        private readonly AtmDbConnection _dbContext;
        private bool _disposed;
        private string _databaseName = "ADOATMDATABASE";

        public CustomerService(AtmDbConnection dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateDb()
        {

            var connection = await _dbContext.OpenConnection();


            string checkDatabaseExistenceQuery = $"SELECT * FROM sys.databases WHERE name='{_databaseName}'";
            
            string query = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{_databaseName}') " +
                $"BEGIN " +
                $"CREATE DATABASE {_databaseName}; " +
                $"END";

            using (SqlCommand dbCommand = new SqlCommand(query, connection))
            {
                try
                {
                    await dbCommand.ExecuteNonQueryAsync();
                    //Console.WriteLine("Database is created sucessfully");
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);                    
                }
            }



        }

        public async Task CreateCustomerTable()
        {
            var connection = await _dbContext.OpenConnection();
            
            string tableName = "Customers";            

            string query = $"Use {_databaseName}; " +
                $"CREATE TABLE {tableName} " +
                $"(CustomerID int Primary Key Identity(1,1), " +
                $"CardNumber varchar(50) NOT NULL, " +
                $"Pin varchar(50) NOT NULL, " +
                $"LogTime varchar(50)); ";

            using (SqlCommand createCommand = new SqlCommand(query, connection))
            {
                try
                {
                    await createCommand.ExecuteNonQueryAsync();
                    //Console.WriteLine("Customer table is created successfully");
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }

            //await _dbContext.CloseConnection();

        }

        public async Task<int> CreateCustomer(CustomerViewModel customer)
        {
            try
            {
                var connection = await _dbContext.OpenConnection();

                string query = $"Use {_databaseName}; " +
                    $"INSERT INTO Customers (CardNumber, Pin, LogTime) " +
                    $"VALUES(@CardNumber, @Pin, @Log) SELECT CAST(SCOPE_IDENTITY() AS BIGINT) ";

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

                    parameter = new SqlParameter()
                    {
                        ParameterName = "@Log",
                        Value = customer.LogTime,
                        SqlDbType = SqlDbType.DateTime,
                        Direction = ParameterDirection.Input,
                    };
                    command.Parameters.Add(parameter);

                    long customerId = (long)await command.ExecuteScalarAsync();
                    
                    //Console.WriteLine($"You have succesfully added a customer with Id:{(int)customerId} to the Db");
                    return (int)customerId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed operation");
                //Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.ToString());
                //Console.WriteLine(ex.StackTrace);

                return 0;
            }

        }

        public async Task InsertCustomerData()
        {
            try
            {
                var connection = await _dbContext.OpenConnection();
                string AddCustomerData = $"Use {_databaseName};  INSERT INTO Customers(CardNumber, Pin, LogTime) " +
                    $"VALUES('60677722', '0190', GETDATE()), " +
                    $"('00921134', '0051', GETDATE()), " +
                    $"('00633859', '4032', GETDATE()), " +
                    $"('61596933', '2109', GETDATE()), " +
                    $"('67221121', '5507', GETDATE()) ";

                string checkCustomersTableQuery = $"Use {_databaseName}; SELECT COUNT(*) FROM Customers";
                int count = 0;

                using (SqlCommand command = new SqlCommand(checkCustomersTableQuery, connection))
                {
                    count = (int)await command.ExecuteScalarAsync();
                };

                if (count == 0)
                {
                    using (SqlCommand addCommand = new SqlCommand(AddCustomerData, connection))
                    {
                        await addCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

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
