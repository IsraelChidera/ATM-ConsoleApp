using ATM.DLL;
using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using System;
using System.Threading.Tasks;

namespace ATM.BLL
{
    public class AtmOperations
    {
        private int balance = 100000;
        public async Task RunWithdraw()
        {
            using (IOperations withdraw = new OperationService(new AtmDbConnection()))
            {
                await withdraw.CreateWithdrawTable();
            }



            Console.WriteLine("\n===========================================================");
            Console.WriteLine($"Your current balance is ${balance}");
            Console.WriteLine("===========================================================\n");

            while (true)
            {
                Console.WriteLine("Withdrawal must be more than $100");
                Start: Console.Write("Enter amount to withdraw: ");
                string input = Console.ReadLine();
                int withdrawAmount;

                if (!int.TryParse(input, out withdrawAmount))
                {
                    Console.WriteLine("\nInvalid amount. Please try again.\n");
                    goto Start;
                }

                if (withdrawAmount > balance)
                {
                    Console.WriteLine("Insufficient funds.");
                    goto Start;
                }

                if (withdrawAmount > 99)
                {
                    balance -= withdrawAmount;
                    Console.WriteLine($"\nSuccessfully withdrew ${withdrawAmount}.\nYour new balance is ${balance}.\n");

                    //int amount = withdrawAmount;

                    using (IOperations withdraw = new OperationService(new AtmDbConnection()))
                    {
                        WithdrawViewModel userWithdraw = new WithdrawViewModel
                        {
                            Balance = balance,
                            Amount = withdrawAmount,
                        };

                        await withdraw.Withdraw(userWithdraw);
                    }
                }
                else
                {
                    Console.WriteLine("Amount must be greater than $100");
                    goto Start;
                }

                Begin: Console.Write("Do you want to perform another transaction? (y/n): ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "y": 
                        //Console.WriteLine();
                        goto Start;

                    case "n":
                        Console.WriteLine("Thank you for staying with us");
                        return;
                    default:
                        Console.WriteLine("Error");
                        goto Begin;
                }

            }

            /*string name = "Edeh";
            string accountNumber = "676376";*/


        }

        public async Task RunDeposit()
        {
            using (IOperations deposit = new OperationService(new AtmDbConnection()))
            {
                await deposit.CreateDepositTable();
            }

            Console.WriteLine("\n====================================================");
            Console.WriteLine($"Deposit");
            Console.WriteLine("====================================================\n");

            while (true)
            {
                Console.WriteLine("Amount to be deposited must be more than $50");
                Start: Console.WriteLine("What amount do you want to deposit?");
                string amount = Console.ReadLine();

                int amt;
                if (!int.TryParse(amount, out amt))
                {
                    Console.WriteLine("\nInvalid amount. Please try again.\n");
                    goto Start;
                }

                if (int.TryParse(amount, out amt) && amt < 50)
                {
                    Console.WriteLine("Error\nAmount to be deposited must be more than $50");
                    goto Start;
                }


                Description:  Console.WriteLine("\nWrite a short description here ...");
                string description = Console.ReadLine();                

                if (description.Length < 5)
                {
                    Console.WriteLine("Description length must be longer than 5");
                    goto Description;
                }                

                else
                {
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
                
            Begin: Console.Write("Do you want to perform another transaction? (y/n): ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "y":
                        //Console.WriteLine();
                        goto Start;

                    case "n":
                        Console.WriteLine("Thank you for staying with us");
                        return;
                    default:
                        Console.WriteLine("Error");
                        goto Begin;
                }
            };

            /*string description = "Money for books";
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
            */
        }

        public async Task RunTransfer()
        {
            using (IOperations transfer = new OperationService(new AtmDbConnection()))
            {
                await transfer.CreateTransferTable();
            }

            // Simulate account balances
            /*decimal balance = 1000.00m;*/
            decimal receiverBalance = 200.00m;

            while (true)
            {
                try
                {
                    // Collect user input                                       
                    Console.Write("Please enter the recipient's account number: ");
                    int receiverAccountNumber = int.Parse(Console.ReadLine());

                    Console.Write("Please enter the amount you wish to transfer: ");
                    int transferAmount = int.Parse(Console.ReadLine());

                    Console.Write("Please enter the recipient's name: ");
                    string descriptions = Console.ReadLine();

                    // Verify user has enough funds
                    if (transferAmount > balance)
                    {
                        throw new Exception("Insufficient funds.");
                    }

                    // Perform the transfer
                    balance -= transferAmount;
                    receiverBalance += transferAmount;

                    // Display the updated balances
                    Console.WriteLine("Transfer successful.");
                    Console.WriteLine("Your new balance is ${1}.", balance);

                    string Account = receiverAccountNumber.ToString();
                    string amount = transferAmount.ToString();
                    string description = descriptions;
                    DateTime dateTime = DateTime.Now;

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
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // Ask user if they want to make another transfer
                Console.Write("Do you want to make another transfer? (Y/N) ");
                string response = Console.ReadLine().ToUpper();

                if (response != "Y")
                {
                    break;
                }
            }

            /*public string ReceiverAccount;

       public string Amount;

       public string Description;

       public DateTime CreatedAt;*/




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
