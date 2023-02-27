using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DLL
{
    public class TransactionService : ITransaction
    {
        private readonly AtmDbConnection _dbContext;
        private bool _disposed;
        private string _databaseName = "TestDb";        

        public TransactionService(AtmDbConnection dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task TransactionsView()
        {
            var connection = await _dbContext.OpenConnection();

            //string tableName = "Withdraw";
            string query = $"Use {_databaseName}; SELECT Transfer.ReceiverAccount, Transfer.Amount, Transfer.Description, Transfer.CreatedAt " +
                $"FROM Transfer";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                List<TransferViewModel> transferList = new List<TransferViewModel>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        string ReceiverAccount = reader["ReceiverAccount"].ToString();
                        string Amount = reader["Amount"].ToString();
                        string Description = reader["Description"].ToString();
                        DateTime CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString());

                        Console.WriteLine($"Receiver Account: {ReceiverAccount}, Amount: {Amount}, " +
                       $"Description: {Description}, Transfer At: {CreatedAt}");
                    }
                }


                
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
