using ATM.DLL;
using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using System;
using System.Threading.Tasks;

namespace ATM.BLL
{
    public class AtmOperations
    {
        public async Task RunWithdraw()
        {
            using (IOperations withdraw = new OperationService(new AtmDbConnection()))
            {
                await withdraw.CreateWithdrawTable();
            }

            string name = "Edeh";
            string accountNumber = "676376";
            string amount = "3232";

            using (IOperations withdraw = new OperationService(new AtmDbConnection()))
            {
                WithdrawViewModel userWithdraw = new WithdrawViewModel
                {
                    Name = name,
                    AccountNumber = accountNumber,
                    Amount = amount,
                };

                await withdraw.Withdraw(userWithdraw);
            }

        }

        public async Task RunDeposit()
        {
            using (IOperations deposit = new OperationService(new AtmDbConnection()))
            {
                await deposit.CreateDepositTable();
            }
                     
            string description = "Money for books";
            string amount = "3232";

            using (IOperations deposit = new OperationService(new AtmDbConnection()))
            {
                DepositViewModel userDeposit = new DepositViewModel
                {                                     
                    Description = description,
                    Amount = amount
                };

                await deposit.Deposit(userDeposit);
            }
        }

        public async Task RunTransfer()
        {
            using (IOperations transfer = new OperationService(new AtmDbConnection()))
            {
                await transfer.CreateTransferTable();
            }

            /*public string ReceiverAccount;

       public string Amount;

       public string Description;

       public DateTime CreatedAt;*/

            string Account = "434343";
            string amount = "120000";
            string description = "Good money";
            DateTime dateTime= DateTime.Now;

            using (IOperations transfer = new OperationService(new AtmDbConnection()))
            {
                TransferViewModel userTransfer = new TransferViewModel
                {
                    ReceiverAccount = Account,
                    Amount = amount,
                    Description = description,
                    CreatedAt = dateTime
                };

                await transfer.Transfer(userTransfer);
            }


            /*;
            using (IOperations deposit = new OperationService(new AtmDbConnection()))
            {
                DepositViewModel userDeposit = new DepositViewModel
                {
                    Description = description,
                    Amount = amount
                };

                await deposit.Deposit(userDeposit);
            }*/
        }

    }
}
