using ATM.DLL;
using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace ATM.BLL
{
    public class ValidateInputs
    {
        private string _cardNumber;
        private string _pinNumber;
        public async Task CreateDbAndTable()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();            
            using (ICustomerInterface customerService = new CustomerService(new AtmDbConnection()))
            {
                await customerService.CreateDb();
                await customerService.CreateCustomerTable();
            }
            
        }      

        public async Task ValidateCardDetails()
        {
            Console.WriteLine("\nEnter Card Number\nCard number must be 8 digits");

            _cardNumber = Console.ReadLine();
            
            while (true)
            {
                try
                {
                    if (_cardNumber.Length == 8 && int.TryParse(_cardNumber, out int cardNum))
                    {
                        Utility.Animation();
                        Console.Clear();
                        Console.WriteLine("\n===============================================");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("This is a valid card number");
                        Console.WriteLine($"Card number: {cardNum}");
                        Console.ResetColor();
                        Console.WriteLine("===============================================\n");

                        break;
                    }
                    else
                    {
                        Utility.Animation();                        
                        Console.WriteLine();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid input. Please try again");                       
                        Console.ResetColor();
                        Console.WriteLine("\nEnter Card Number\nCard number must be 8 digits");
                        _cardNumber = Console.ReadLine();
                    }
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPlease enter a valid card number");

                    Console.WriteLine(exception.Message);
                    Console.ResetColor();
                    continue;
                }
            }

            Console.WriteLine("\nPin must be up to 4 digits");
            _pinNumber = Console.ReadLine();

            while (true)
            {
                try
                {
                    if (_pinNumber.Length == 4 && int.TryParse(_pinNumber, out int pinNum))
                    {
                        Utility.Animation();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n===============================================");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"PIN: {pinNum}");
                        Utility.SucessfullTransferPrompts("Congrats... You can now do your transactions");                        
                        Console.ResetColor();
                        Console.WriteLine("===============================================\n");
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid PIN. Please try again");
                        _pinNumber = Console.ReadLine();
                    }
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a PIN number");
                    Console.WriteLine(exception.Message);
                    Console.ResetColor();
                    continue;
                }
            }



            DateTime dateTime= DateTime.Now;

            using (ICustomerInterface insertInputs = new CustomerService(new AtmDbConnection()))
            {
                CustomerViewModel customerDetails = new CustomerViewModel
                {
                    CardNumber = _cardNumber,
                    Pin = _pinNumber,
                    LogTime= dateTime,
                };

                await insertInputs.CreateCustomer(customerDetails);
            }

        }


        
        public async Task ValidateCustomerInputs()
        {
            await CreateDbAndTable();
            await ValidateCardDetails();
        }
    }
}
