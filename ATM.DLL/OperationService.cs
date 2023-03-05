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
        private string _databaseName = "ADOATMDATABASE";

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
                $"(WithdrawID int Primary Key Identity(1,1),  " +
                $"Balance INT, " +
                $"AmountWithdrawn INT, " +
                $"FOREIGN KEY (WithdrawID) REFERENCES Customers(CustomerID));";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await command.ExecuteNonQueryAsync();
                    //Console.WriteLine("Withdraw table is created successfully");
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }

        }

        public async Task<int> Withdraw(WithdrawViewModel withdraw)
        {
            try
            {
                var connection = await _dbContext.OpenConnection();
                string query = $"Use {_databaseName}; INSERT INTO Withdraw (Balance, AmountWithdrawn) " +
                    "VALUES( @Balance, @AmountWithdrawn) " +
                    "SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";

               

                using (SqlCommand commands = new SqlCommand(query, connection))
                {


                    SqlParameter parameter = new SqlParameter()
                    {
                        ParameterName = "Balance",
                        Value = withdraw.Balance,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                    };
                    commands.Parameters.Add(parameter);

                    parameter = new SqlParameter()
                    {
                        ParameterName = "AmountWithdrawn",
                        Value = withdraw.AmountWithdrawn,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                    };
                    commands.Parameters.Add(parameter);


                    long withdrawId = (long)await commands.ExecuteScalarAsync();
                    //Console.WriteLine($"You have succesfully added a withdraw data with Id:{(int)withdrawId} to the Db");
                    return (int)withdrawId;
                }


                /*using (SqlCommand command = new SqlCommand(query, connection))
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
                        ParameterName = "AmountWithdrawn",
                        Value = withdraw.AmountWithdrawn,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                    };
                    command.Parameters.Add(parameter);


                    long withdrawId = (long)await command.ExecuteScalarAsync();
                    //Console.WriteLine($"You have succesfully added a withdraw data with Id:{(int)withdrawId} to the Db");
                    return (int)withdrawId;
                }*/

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.ToString());
                Console.WriteLine("wqaja");
                return 0;
            }
        }

        public async Task InsertWithdrawData()
        {
            try
            {
                var connection = await _dbContext.OpenConnection();
                string AddWithdrawSeed = $"Use {_databaseName}; INSERT INTO Withdraw( Balance, AmountWithdrawn) " +
                   $"VALUES(967666, 980), " +
                   $"( 267666, 99980), " +
                   $"(1067666, 8999) " +
                   $"(67666, 892) " +
                   $"(8066, 480) ";

                string checkWithdrawTableQuery = $"Use {_databaseName}; SELECT COUNT(*) FROM Withdraw";
                int count = 0;

                using (SqlCommand command = new SqlCommand(checkWithdrawTableQuery, connection))
                {
                    count = (int)await command.ExecuteScalarAsync();
                };

                if (count == 0)
                {
                    using (SqlCommand addWithdrawSeed = new SqlCommand(AddWithdrawSeed, connection))
                    {
                        await addWithdrawSeed.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
        }

        public async Task InsertTransferData()
        {
            try
            {
                var connection = await _dbContext.OpenConnection();
                string AddTransferSeed = $"Use {_databaseName}; INSERT INTO Transfer( ReceiverAccount, AmountTransferred, TransferDescription, CreatedAt) " +
                    $"VALUES('90676660', '600', 'Money for food', GETDATE()), " +
                    $"( '20076660', '465','Kroft payment', GETDATE()), " +
                    $"( '00233455', '465','Kroft payment', GETDATE()), " +
                    $"( '61076660', '465','Kroft payment', GETDATE()), " +
                    $"('50076660', '39', 'Savings for car', GETDATE()); ";

                string checkTransferTableQuery = $"Use {_databaseName}; SELECT COUNT(*) FROM Transfer";
                int count = 0;

                using (SqlCommand command = new SqlCommand(checkTransferTableQuery, connection))
                {
                    count = (int)await command.ExecuteScalarAsync();
                };

                if (count == 0)
                {
                    using (SqlCommand addTransferSeedCommand = new SqlCommand(AddTransferSeed, connection))
                    {
                        await addTransferSeedCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        public async Task InsertDepositData()
        {
            try
            {
                var connection = await _dbContext.OpenConnection();
                string AddDepositData = $"Use {_databaseName};  INSERT INTO Deposit( AmountDeposited, DepositDescription) " +
                    $"VALUES(9067666, 'Money for food'), " +
                    $"( 2007666, 'Kroft payment'), " +
                    $"( 10400, 'Dish payment'), " +
                    $"( 200050, 'Marketing payment'), " +
                    $"(5007666, 'Savings for car') ";

                string checkDepositTableQuery = $"Use {_databaseName}; SELECT COUNT(*) FROM Deposit";
                int count = 0;

                using (SqlCommand command = new SqlCommand(checkDepositTableQuery, connection))
                {
                    count = (int)await command.ExecuteScalarAsync();
                };

                if (count == 0)
                {
                    using (SqlCommand addCommand = new SqlCommand(AddDepositData, connection))
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

        //Deposit Operations
        public async Task CreateDepositTable()
        {
            var connection = await _dbContext.OpenConnection();

            string tableName = "Deposit";
            string query = $"Use {_databaseName}; " +
                $"CREATE TABLE {tableName}" +
                $"(DepositID int Primary Key Identity(1,1), " +
                $"AmountDeposited varchar(50), " +
                $"DepositDescription varchar(50), " +
                $"FOREIGN KEY (DepositID) REFERENCES Customers(CustomerID));";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await command.ExecuteNonQueryAsync();
                    //Console.WriteLine("Withdraw table is created successfully");
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }

        }

        public async Task<int> Deposit(DepositViewModel depositView)
        {
            try
            {
                var connection = await _dbContext.OpenConnection();


                string query = $"Use {_databaseName}; " +
                    $"INSERT INTO Deposit ( AmountDeposited, DepositDescription ) " +
                        "VALUES(@AmountDeposited, @DepositDescription) " +
                        "SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "AmountDeposited",
                        Value = depositView.AmountDeposited,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "DepositDescription",
                        Value = depositView.DepositDescription,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    long DepositId = (long)await command.ExecuteScalarAsync();
                    //Console.WriteLine($"You have succesfully added a Deposit data with Id:{(int)DepositId} to the Db");

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
            string query = $"Use {_databaseName}; " +
                $"CREATE TABLE {tableName}" +
                $"(TransferID int Primary Key Identity(1,1), " +
                $"ReceiverAccount varchar(50), " +
                $"AmountTransferred varchar(50), " +
                $"TransferDescription varchar(50), " +
                $"CreatedAt varchar(50), " +
                $"FOREIGN KEY (TransferID) REFERENCES Customers(CustomerID));";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await command.ExecuteNonQueryAsync();
                    //Console.WriteLine("Transfer table is created successfully");
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }

        }
        public async Task<int> Transfer(TransferViewModel transfer)
        {
            try
            {
                var connection = await _dbContext.OpenConnection();


                string query = $"Use {_databaseName}; INSERT INTO Transfer ( ReceiverAccount, AmountTransferred, TransferDescription, CreatedAt ) " +
                        "VALUES(@Account, @AmountTransferred, @TransferDescription, @CreatedAt) " +
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
                        ParameterName = "AmountTransferred",
                        Value = transfer.AmountTransferred,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter
                    {
                        ParameterName = "TransferDescription",
                        Value = transfer.TransferDescription,
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
                    //Console.WriteLine($"You have succesfully added a Deposit data with Id:{(int)TransferId} to the Db");
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
