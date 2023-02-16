using ATM.BLL;
using ATM.BLL.Admin;

namespace ATM.UI
{
    public class Application
    {
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
                        Console.WriteLine("User login");
                        await Users();
                        check = false;
                        break;
                    case "2":
                        Console.WriteLine("Admin logins");
                        AdminOperations admin = new();
                        admin.AdminLogin();
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
                start:  Console.WriteLine("\nHi! there\nWhat do you want to do?\nPress 1: Deposit\nPress 2: Withdraw " +
                    "\nPress 3: Transfer\nPress 0: Exit");

                string option = Console.ReadLine();
                if (true)
                {
                    switch (option)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Deposit");
                            //Deposit class
                            await operations.RunDeposit();
                            check = false;
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("Withdraw");
                            //withdraw class
                            //AtmOperations operations = new AtmOperations();
                            await operations.RunWithdraw();
                            goto start;
                            //check = false;
                            //break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine("Transfer");
                            //Transfer class
                            await operations.RunTransfer();
                            check = false;
                            break;
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

            //withdraw class
            //AtmOperations operations = new AtmOperations();
            //await operations.RunWithdraw();            

            //Deposit class
            //await operations.RunDeposit();

            //Transfer class
            //await operations.RunTransfer();
        }

    }

}
