using ATM.DLL;
using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATM.BLL
{
    public class AdminOperations
    {
        public async Task GetDepositTransactions()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("============================================================");
                Console.WriteLine("\tAll Deposit Transactions");
                Console.WriteLine("============================================================");
                using (IAdminInterface admin = new AtmAdminService(new AtmDbConnection()))
                {
                    await admin.DepositTransactions();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }
        }   
        
        public async Task GetTransferTransactions()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("============================================================");
                Console.WriteLine("\tAll Transfer Transactions");
                Console.WriteLine("============================================================");
                using (IAdminInterface admin = new AtmAdminService(new AtmDbConnection()))
                {
                    await admin.TransferTransactions();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }

            
        }

        public async Task GetWithdrawTransactions()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===============================================================");
                Console.WriteLine("\tAll Withdraw Transactions");
                Console.WriteLine("===============================================================");
                using (IAdminInterface admin = new AtmAdminService(new AtmDbConnection()))
                {
                    await admin.WithdrawTransactions();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }


        }

        public async Task GetAllTransactions()
        {
            try
            {
                using (IAdminInterface admin = new AtmAdminService(new AtmDbConnection()))
                {
                    await admin.ViewAllTransactions();
                }
            }
            catch
            {
                Utility.ErrorPrompts("Error. Unable to fetch all transactions");
            }
        }


    }
}


