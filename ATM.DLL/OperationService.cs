using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ATM.DLL
{
    public class OperationService : IOperations
    {
        private readonly AtmDbConnection _dbContext;
        private bool _disposed;
        private string _databaseName = "TestDb";

        public OperationService(AtmDbConnection dbContext)
        {
            _dbContext = dbContext;
        }

        //Withdraw Operations
        public async Task CreateWithdrawTable()
        {
            var connection = await _dbContext.OpenConnection();

            string tableName = "Withdraw";
            string query = $"Use {_databaseName}; CREATE TABLE {tableName} " +
                $"(WithdrawId int Primary Key Identity(1,1), " +              
                $"Balance int, " +
                $"Amount int " +
                ");";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Withdraw table is created successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public async Task<int> Withdraw(WithdrawViewModel withdraw)
        {
            try
            {
                var connection = await _dbContext.OpenConnection();
                string query = $"Use {_databaseName}; INSERT INTO Withdraw (Balance, Amount) " +
                    "VALUES( @Balance, @Amount) SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    SqlParameter parameter = new SqlParameter()
                    {
                        ParameterName = "Balance",
                        Value = withdraw.Balance,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter()
                    {
                        ParameterName = "Amount",
                        Value = withdraw.Amount,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                    };
                    command.Parameters.Add(parameter);


                    long withdrawId = (long)await command.ExecuteScalarAsync();
                    Console.WriteLine($"You have succesfully added a withdraw data with Id:{(int)withdrawId} to the Db");
                    return (int)withdrawId;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }


        //Deposit Operations
        public async Task CreateDepositTable()
        {
            var connection = await _dbContext.OpenConnection();

            string tableName = "Deposit";
            string query = $"Use {_databaseName}; CREATE TABLE {tableName} " +
                $"(WithdrawId int Primary Key Identity(1,1), " +                             
                $"Amount varchar(50), " +
                $"Description varchar(50) " +
                ");";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Withdraw table is created successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public async Task<int> Deposit(DepositViewModel depositView)
        {
            try
            {
                var connection = await _dbContext.OpenConnection();


                string query = $"Use {_databaseName}; INSERT INTO Deposit ( Amount, Description ) " +
                        "VALUES(@Amount, @Description) " +
                        "SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {                                     
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "Amount",
                        Value = depositView.Amount,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "Description",
                        Value = depositView.Description,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    long DepositId = (long)await command.ExecuteScalarAsync();
                    Console.WriteLine($"You have succesfully added a Deposit data with Id:{(int)DepositId} to the Db");
                    return (int)DepositId;
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Source);
                return 0;
            }

        }        

        //Transfer Operations
        public async Task CreateTransferTable()
        {
            var connection = await _dbContext.OpenConnection();

            string tableName = "Transfer";
            string query = $"Use {_databaseName}; CREATE TABLE {tableName} " +
                $"(TransferID int Primary Key Identity(1,1), " +
                $"ReceiverAccount varchar(50), " +
                $"Amount varchar(50), " +
                $"Description varchar(50), " +
                $"CreatedAt varchar(50) " +
                ");";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Transfer table is created successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        public async Task<int> Transfer(TransferViewModel transfer)
        {
            try
            {
                var connection = await _dbContext.OpenConnection();


                string query = $"Use {_databaseName}; INSERT INTO Transfer ( ReceiverAccount, Amount, Description, CreatedAt ) " +
                        "VALUES(@Account, @Amount, @Description, @CreatedAt) " +
                        "SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "Account",
                        Value = transfer.ReceiverAccount,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "Amount",
                        Value = transfer.Amount,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "Description",
                        Value = transfer.Description,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "CreatedAt",
                        Value = transfer.CreatedAt,
                        SqlDbType = SqlDbType.DateTime,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    long TransferId = (long)await command.ExecuteScalarAsync();
                    Console.WriteLine($"You have succesfully added a Deposit data with Id:{(int)TransferId} to the Db");
                    return (int)TransferId;
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Source);
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
            //throw new NotImplementedException();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
