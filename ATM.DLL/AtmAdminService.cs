using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATM.DLL
{
    public class AtmAdminService : IAdminInterface
    {
        private readonly AtmDbConnection _dbContext;
        private bool _disposed;
        private string _databaseName = "ADOATMDATABASE";

        public AtmAdminService(AtmDbConnection dbContext)  
        {
            _dbContext = dbContext;
        }             

        public async Task<IEnumerable<DepositViewModel>> DepositTransactions()
        {
            var connection = await _dbContext.OpenConnection();            

            string query = $"Use {_databaseName}; SELECT Deposit.AmountDeposited, Deposit.DepositDescription " +
                    $"FROM Deposit";
            /*string query = "select * from Deposit Where";*/

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                List<DepositViewModel> transferList = new List<DepositViewModel>();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        transferList.Add(
                            new DepositViewModel()
                            {
                                AmountDeposited = reader["AmountDeposited"].ToString(),
                                DepositDescription = reader["DepositDescription"].ToString(),
                            });
                    }
                };

                foreach (var list in transferList)
                {
                    Console.WriteLine($"Amount: {list.AmountDeposited} --- Description: {list.DepositDescription}");
                }

                return transferList;
            }

        }
       
        public async Task<IEnumerable<TransferViewModel>> TransferTransactions()
        {
            var connection = await _dbContext.OpenConnection();
            string query = $"Use {_databaseName}; SELECT Transfer.ReceiverAccount, Transfer.AmountTransferred, Transfer.TransferDescription, Transfer.CreatedAt " +
                $"FROM Transfer";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                List<TransferViewModel> transferList = new List<TransferViewModel>();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        transferList.Add(new TransferViewModel()
                        {
                            ReceiverAccount = reader["ReceiverAccount"].ToString(),
                            AmountTransferred = reader["AmountTransferred"].ToString(),
                            TransferDescription = reader["TransferDescription"].ToString(),
                            CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        });
                    }
                }

                foreach (var list in transferList)
                {
                    Console.WriteLine($"Receiver Account: {list.ReceiverAccount}, Amount: {list.AmountTransferred}, " +
                        $"Description: {list.TransferDescription}, Transfer At: {list.CreatedAt}");
                }

                return transferList;
            }

                       
        }
        
        public async Task<IEnumerable<WithdrawViewModel>> WithdrawTransactions()
        {
            var connection = await _dbContext.OpenConnection();
            string query = $"Use {_databaseName}; SELECT Withdraw.Balance, Withdraw.AmountWithdrawn " +
               $"FROM Withdraw";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                List<WithdrawViewModel> transferList = new List<WithdrawViewModel>();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        transferList.Add(new WithdrawViewModel()
                        {
                            Balance = int.Parse(reader["Balance"].ToString()),
                            AmountWithdrawn = int.Parse(reader["AmountWithdrawn"].ToString())
                        });
                    }
                }
                Console.WriteLine("---------------------------------------------------------------------------------------------------");
                Console.WriteLine($"\tBalance\t\t\t\t|\t\t\t\tAmount");
                Console.WriteLine("---------------------------------------------------------------------------------------------------");
                foreach (var list in transferList)
                {
                    Console.WriteLine($"\t{list.Balance}\t\t\t\t|\t\t\t\t{list.AmountWithdrawn}");                    
                }

                return transferList;
            }
        }

        public async Task ViewAllTransactions()
        {
            try
            {
                var connection = await _dbContext.OpenConnection();
                string query = $"Use {_databaseName}; SELECT customer.CardNumber, customer.CustomerID, customer.Pin, " +
                    $"customer.LogTime, deposit.AmountDeposited, deposit.DepositDescription, " +
                    $"transfer.ReceiverAccount, transfer.AmountTransferred, transfer.TransferDescription, " +
                    $"transfer.CreatedAt, withdraw.Balance, withdraw.AmountWithdrawn " +
                    $"FROM Customers customer " +
                    $"INNER JOIN Deposit deposit ON customer.CustomerID = deposit.DepositID " +
                    $"INNER JOIN Transfer transfer ON customer.CustomerID = transfer.TransferID " +
                    $"INNER JOIN Withdraw withdraw ON customer.CustomerID = withdraw.WithdrawID; ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using(SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {                            
                            var CardNumber = reader["CardNumber"].ToString();
                            var CustomerID = int.Parse(reader["CustomerID"].ToString());
                            var Pin = reader["Pin"].ToString();
                            var LogTime = DateTime.Parse(reader["LogTime"].ToString());
                            var AmountDeposited = int.Parse(reader["AmountDeposited"].ToString());
                            var DepositedDescription = reader["DepositDescription"];
                            var ReceiverAccount = reader["ReceiverAccount"];
                            var AmountTransferred = int.Parse(reader["AmountTransferred"].ToString());
                            var TransferDescription = reader["TransferDescription"];
                            var CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString());
                            var Balance = int.Parse(reader["Balance"].ToString());
                            var AmountWithdrawn = int.Parse(reader["AmountWithdrawn"].ToString());

                          
                            //Console.BackgroundColor= ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"ID: {CustomerID}, CardNumber: {CardNumber}, Pin: {Pin}, LogTime: {LogTime}" +
                                $", Amount Deposited: {AmountDeposited}, Amount Withdrawn: {AmountWithdrawn}," +
                                $" Amount Transferred: {AmountTransferred}" +
                                $", Balance: {Balance}");
                            Console.ResetColor();
                            Console.WriteLine();
                            
                        }
                    }


                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Source);
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
