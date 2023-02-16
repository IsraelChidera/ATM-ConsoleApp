using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using System;
using System.Collections.Generic;

namespace ATM.BLL.Admin
{
    public class AdminOperations : IAdminInterface
    {
        //Admin should be able to 
        //0.Login as an admin
        //1.View all transactions done by user ---i. Deposit ii. Transfer iii. withdraw iv. update a user
        //2.Delete and edit customer stuff
        //3. Add Money to the ATM 

        List<AdminLoginViewModel> adminCredentials = new List<AdminLoginViewModel>
        {
            new AdminLoginViewModel{Username="admin", Password="0260"},
            new AdminLoginViewModel{Username="icecode", Password="6002"},
        };

        public void AdminLogin()
        {
            bool check = true;

            Console.WriteLine("\n********************************************");
            Console.WriteLine("Login as an admin");
            Console.WriteLine("********************************************");

            while (check)
            {
                Console.Write("\nEnter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                bool isAuthenticated = false;
                foreach (var admin in adminCredentials)
                {
                    if (admin.Username == username && admin.Password == password)
                    {
                        isAuthenticated = true;
                        AdminTransactions();
                        break;
                    };
                };

                if(isAuthenticated == true)
                {
                    Console.WriteLine("You have successfully logged in as an admin");
                    break;
                }
                else
                {
                    Console.WriteLine("Failed attempt logging in");
                }

            }
        }

        public void AdminTransactions()
        {
            bool check = true;
            while (check)
            {
                Console.WriteLine("\nHello Admin\nWhat do you want to do..." +
                    "\nPress 1: To view all deposit transactions " +
                    "\nPress 2: To view all withdrawal transactions " +
                    "\nPress 3: To view all transfer transactions " +
                    "\nPress 0: To exit\n");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Deposit transaction");                        
                        check = false;
                        break;
                    case "2":
                        Console.WriteLine("Withdrawal transaction");
                        check = false;
                        break;
                    case "3":
                        Console.WriteLine("Transfer transaction");
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

    }
}
