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

            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.WriteLine("\n===========================================================");
            Console.WriteLine($"Your current balance is ${balance}");
            Console.WriteLine("===========================================================\n");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor= ConsoleColor.Yellow;
                Console.WriteLine("Withdrawal must be more than $100");
                Console.ResetColor();

            Start: Console.Write("Enter amount to withdraw: ");
                string input = Console.ReadLine();
                int withdrawAmount;

                if (!int.TryParse(input, out withdrawAmount))
                {
                    Console.Clear();
                    Utility.ErrorPrompts("Invalid amount. Please try again.");
                    //Console.WriteLine("\nInvalid amount. Please try again.\n");
                    goto Start;
                }

                if (withdrawAmount > balance)
                {
                    Console.Clear();
                    Utility.ErrorPrompts("Insufficient funds.");
                    //Console.WriteLine("Insufficient funds.");
                    goto Start;
                }

                if (withdrawAmount > 99)
                {
                    Console.Clear();
                    balance -= withdrawAmount;
                    
                    //Console.WriteLine($"\nSuccessfully withdrew ${withdrawAmount}.\nYour new balance is ${balance}.\n");

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
                    Utility.SucessfullTransferPrompts($"\nSuccessfully withdrew ${withdrawAmount}.\nYour new balance is ${balance}.\n");
                }
                else
                {
                    Console.Clear();
                    Utility.ErrorPrompts("Amount must be greater than $100");
                    //Console.WriteLine("Amount must be greater than $100");
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
                        Console.WriteLine("Thank you for banking with us");
                        return;
                    default:
                        Console.WriteLine("Error");
                        goto Begin;
                }

            }


        }

        public async Task RunDeposit()
        {
            using (IOperations deposit = new OperationService(new AtmDbConnection()))
            {
                await deposit.CreateDepositTable();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n================================================================");
            Console.WriteLine($"DEPOSIT");
            Console.WriteLine("=================================================================\n");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Amount to be deposited must be more than $50");
                Console.ResetColor();

            Start: Console.WriteLine("What amount do you want to deposit?");
                string amount = Console.ReadLine();

                int amt;
                if (!int.TryParse(amount, out amt))
                {
                    Console.Clear();
                    Utility.ErrorPrompts("Invalid amount. Please try again.");
                    //Console.WriteLine("\nInvalid amount. Please try again.\n");
                    goto Start;
                }

                if (int.TryParse(amount, out amt) && amt < 50)
                {
                    Console.Clear();
                    Utility.ErrorPrompts("Error\nAmount to be deposited must be more than $50");
                    //Console.WriteLine("Error\nAmount to be deposited must be more than $50");
                    goto Start;
                }


            Description: Console.WriteLine("\nWrite a short description here ...");
                string description = Console.ReadLine();

                if (description.Length < 5)
                {
                    Console.Clear();
                    Utility.ErrorPrompts("Description length must be longer than 5");
                    //Console.WriteLine("Description length must be longer than 5");
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
                        Utility.SucessfullTransferPrompts($"You have deposited ${amount} successfully");
                    }
                }
                
                Console.ForegroundColor = ConsoleColor.Yellow;
            Begin: Console.Write("Do you want to perform another transaction? (y/n): ");
                Console.ResetColor();
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

        }

        public async Task RunTransfer()
        {
            using (IOperations transfer = new OperationService(new AtmDbConnection()))
            {
                await transfer.CreateTransferTable();
            }

            Console.Clear();
            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.WriteLine("\n====================================================");
            Console.WriteLine($"TRANSFER");
            Console.WriteLine("====================================================\n");
            Console.ResetColor();

            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                Receiver: Console.WriteLine("\nPlease enter the recipient's account number ");
                    Console.WriteLine("Account length must be greater than 5");
                    Console.ResetColor();

                    string receiverAccountNumber = Console.ReadLine();
                    int accountNumber;

                    if (!int.TryParse(receiverAccountNumber, out accountNumber))
                    {
                        Console.Clear();
                        Utility.ErrorPrompts("Invalid input.\nPlease enter a positive integer for the account number: ");
                        //Console.WriteLine("\nInvalid input.\nPlease enter a positive integer for the account number: ");
                        goto Receiver;
                    }

                    if (int.TryParse(receiverAccountNumber, out accountNumber) && receiverAccountNumber.Length < 6)
                    {
                        Console.Clear();
                        Utility.ErrorPrompts("Invalid input.\nPlease enter a positive integer for the account number: ");
                        //Console.WriteLine("\nInvalid input.\nPlease enter a positive integer for the account number: ");
                        goto Receiver;
                    }


                    Console.ForegroundColor = ConsoleColor.Yellow;
                Amount: Console.Write("Please enter the amount you wish to transfer: ");
                    Console.ResetColor();

                    string transferAmount = Console.ReadLine();
                    int Amount;

                    if (!int.TryParse(transferAmount, out Amount))
                    {
                        Console.Clear();
                        Utility.ErrorPrompts("Invalid input.\nPlease enter a positive integer for the account number: ");
                        //Console.Write("\nInvalid input.\nPlease enter a positive integer for the account number: ");
                        goto Amount;
                    }
                    if (int.TryParse(transferAmount, out Amount) && Amount < 50)
                    {
                        Console.Clear();
                        Utility.ErrorPrompts("Invalid input.\nPlease enter a positive integer for the account number: ");
                        //Console.WriteLine("\nInvalid input.\nPlease enter a positive integer for the account number: ");
                        goto Amount;
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                Desc: Console.Write("Please enter the recipient's name: ");
                    Console.ResetColor();
                    string descriptions = Console.ReadLine();
                    if (descriptions.Length < 5)
                    {
                        Console.Clear();
                        Utility.ErrorPrompts("Invalid input.\nInput length must be greater than 5 ");
                        //Console.WriteLine("\nInvalid input.\nInput length must be greater than 5 ");
                        goto Desc;
                    }

                    if (Amount > balance)
                    {
                        throw new Exception("\nInsufficient funds.");
                    }

                    balance -= Amount;

                    Utility.SucessfullTransferPrompts("Transfer successful.");
                    //Console.WriteLine("\nTransfer successful.");
                    Console.WriteLine("Your new balance is ${0}.", balance);

                    string Account = receiverAccountNumber;
                    string amount = transferAmount;
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.TargetSite);
                    Console.WriteLine(ex.Source);
                }


            Begin: Console.Write("Do you want to perform another transaction? (y/n): ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "y":
                        break;

                    case "n":
                        Console.WriteLine("Thank you for banking with us");
                        return;
                    default:
                        Console.WriteLine("Error");
                        goto Begin;
                }
            }


        }

    }
}
