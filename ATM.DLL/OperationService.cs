using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DLL
{
    public class OperationService : IOperations
    {
        private readonly AtmDbConnection _dbContext;
        private bool _disposed;

        public OperationService(AtmDbConnection dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task CreateWithdrawTable()
        {
            var connection = await _dbContext.OpenConnection();

            string tableName = "Withdraw";
            string query = $"CREATE TABLE {tableName} " +
                $"(WithdrawId int Primary Key Identity(1,1), " +
                $"Name varchar(50), " +
                $"AccountNumber varchar(50), " +
                $"Amount varchar(50) " +
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

        async Task<int> IOperations.Withdraw(WithdrawViewModel withdraw)
        {
            try
            {
                var connection = await _dbContext.OpenConnection();
                string query = "INSERT INTO Withdraw (Name, AccountNumber, Amount) " +
                    "VALUES(@Name, @AccountNumber, @Amount) SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter parameter = new SqlParameter()
                    {
                        ParameterName= "Name",
                        Value = withdraw.Name,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter()
                    {
                        ParameterName = "AccountNumber",
                        Value = withdraw.AccountNumber,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                    };
                    command.Parameters.Add(parameter);

                    parameter = new SqlParameter()
                    {
                        ParameterName = "Amount",
                        Value = withdraw.Amount,
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                    };
                    command.Parameters.Add(parameter);


                    long withdrawId = (long)await command.ExecuteScalarAsync();
                    Console.WriteLine($"You have succesfully added a withdraw data with Id:{(int)withdrawId} to the Db");
                    return (int)withdrawId;
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            throw new NotImplementedException();
        }

        public Task<int> Deposit(int amount)
        {
            throw new NotImplementedException();
        }        

        public Task<int> Transfer(int amount)
        {
            throw new NotImplementedException();
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
