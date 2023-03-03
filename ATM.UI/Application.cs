using ATM.BLL;
using ATM.DLL.Model;

namespace ATM.UI
{
    public class Application
    {
        private List<AdminLoginViewModel> adminCredentials = new List<AdminLoginViewModel>
        {
            new AdminLoginViewModel{Username="admin", Password="2206"},
            new AdminLoginViewModel{Username="icecode", Password="6002"},
        };
        AdminOperations adminOperations = new AdminOperations();
        public async Task Run()
        {


            Utility.HomeContent();

            bool check = true;
            while (check)
            {
                Console.WriteLine("\nWelcome...\nPress 1: To login as a user\nPress 2: To login as an admin\nPress 0: To exit application\n");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("========================================================");
                        Console.WriteLine("\tUser login");
                        Console.WriteLine("========================================================");
                        await Users();
                        check = false;
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("========================================================");
                        Console.WriteLine("\tAdmin login");
                        Console.WriteLine("========================================================");
                        await AdminLogin();
                        check = false;
                        break;
                    case "0":
                        Console.WriteLine("Exit application");
                        check = false;
                        break;
                    default:
                        Console.WriteLine("Incorrect Inputs... Try again!");
                        check = true;
                        break;
                }

                if (!check)
                {
                    break;
                }
            }

        }

        public async Task Users()
        {
            //Validate class
            ValidateInputs validate = new();
            await validate.ValidateCustomerInputs();
            AtmOperations operations = new AtmOperations();
            bool check = true;

            while (check)
            {
            start: Console.WriteLine("\nHi! there\nWhat do you want to do?\nPress 1: Deposit\nPress 2: Withdraw " +
                "\nPress 3: Transfer\nPress 4: Balance\nPress 0: Exit");

                string option = Console.ReadLine();
                if (true)
                {
                    switch (option)
                    {
                        case "1":
                            Console.Clear();                            
                            //Deposit class
                            await operations.RunDeposit();
                            goto start;
                        //check = false;
                        //break;
                        case "2":
                            Console.Clear();                            
                            //withdraw class
                            //AtmOperations operations = new AtmOperations();
                            await operations.RunWithdraw();
                            goto start;
                        //check = false;
                        //break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine("========================================================");
                            Console.WriteLine("\tTransfer");
                            Console.WriteLine("========================================================");
                            //Transfer class
                            await operations.RunTransfer();
                            goto start;
                        //check = false;
                        //break;
                        case "4":
                            Console.Clear();
                            operations.RunBalance();
                            goto start;
                        case "0":
                            Console.Clear();
                            Console.WriteLine("Exiting . . .");
                            Console.WriteLine("We will love to have you back");

                            check = false;
                            break;
                        default:
                            Console.WriteLine("Error ...");
                            check = true;
                            break;
                    }
                }

                if (!check)
                {
                    break;
                }

            }
       
        }

        public async Task AdminLogin()
        {
            bool check = true;
            Console.Clear();
            Console.WriteLine("\n==========================================");
            Console.WriteLine("\tLogin as an admin");
            Console.WriteLine("============================================");

            while (check)
            {
            Start: Console.Write("\nEnter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                bool isAuthenticated = false;
                foreach (var admin in adminCredentials)
                {
                    if (admin.Username == username?.ToLower() && admin.Password == password)
                    {
                        isAuthenticated = true;

                        await AdminTransactions();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Failed attempt logging in\nTry again");
                        goto Start;
                    }
                };

            }
        }

        public async Task AdminTransactions()
        {
            bool check = true;
            while (check)
            {
            Start: Console.Clear();
                Console.WriteLine("\nHello Admin\n\nWhat do you want to do..." +
                    "\nPress 1: To view all deposit transactions " +
                    "\nPress 2: To view all transfer transactions " +
                    "\nPress 3: To view all withdrawal transactions " +
                    "\nPress 0: To exit\n");
                string option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":                            
                            await adminOperations.GetDepositTransactions();
                            check = false;
                            //goto Start;
                            break;
                        case "2":                            
                            //goto Start;
                            await adminOperations.GetTransferTransactions();
                            check = false;
                            break;
                        case "3":
                            
                            await adminOperations.GetWithdrawTransactions();
                            //goto Start;
                            check = false;
                            break;
                        case "0":
                            Console.Clear();
                            Console.WriteLine("Exit application");
                            check = true;
                            await Run();                            
                            break;
                        default:
                            Console.WriteLine("Incorrect Inputs... Try again!");
                            check = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Source);
                }


            Begin: Console.Write("\nDo you want to perform another transaction? (y/n): ");
                string options = Console.ReadLine();

                switch (options)
                {
                    case "y":
                        //Console.WriteLine();
                        goto Start;

                    case "n":
                        Console.Clear();
                        Console.WriteLine("Thank you!\n");
                        await Run();
                        return;
                    default:
                        Console.WriteLine("Invalid inputs . . . Try again!");
                        goto Begin;
                }

            }

        }


    }

}
