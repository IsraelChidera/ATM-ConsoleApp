using ATM.DLL;
using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using System;
using System.Threading.Tasks;

namespace ATM.BLL
{
    public class ValidateInputs
    {
        private string _cardNumber;
        private string _pinNumber;
        public async Task CreateDb()
        {
            using (ICustomerInterface customerService = new CustomerService(new AtmDbConnection()))
            {
                await customerService.CreateCustomerTable();
            }

        }

        public async Task ValidateCustomerInputs()
        {
            await CreateDb();
            await ValidateCardDetails();            
        }

        public async Task InsertCustomerInputs()
        {
            ICustomerInterface insertInputs = new CustomerService(new AtmDbConnection());
            CustomerViewModel customer = new CustomerViewModel
            {
                CardNumber = "569467",
                Pin = "1234",
            };

            await insertInputs.CreateCustomer(customer);
        }

        public async Task ValidateCardDetails()
        {
            Console.WriteLine("Enter Card Number\nCard number must be 6 digits");

            _cardNumber = Console.ReadLine();
            //public int 
            while (true)
            {
                try
                {
                    if (_cardNumber.Length == 6 && int.TryParse(_cardNumber, out int cardNum))
                    {
                        Utility.Animation();
                        Console.WriteLine("\n**********************************************");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Congrats... Valid card number");
                        Console.WriteLine($"Card number: {cardNum}");
                        Console.ResetColor();
                        Console.WriteLine("**********************************************\n");

                        break;
                    }
                    else
                    {
                        Utility.Animation();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid input. Please try again");
                        _cardNumber = Console.ReadLine();
                        Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("**********************************************");
                        Console.WriteLine($"PIN: {pinNum}");
                        Console.WriteLine("Congrats... You can now do your transactions");
                        Console.WriteLine("**********************************************");
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



            Console.WriteLine(_pinNumber);
            Console.WriteLine(_cardNumber);

            using (ICustomerInterface insertInputs = new CustomerService(new AtmDbConnection()))
            {
                CustomerViewModel customerDetails = new CustomerViewModel
                {
                    CardNumber = _cardNumber,
                    Pin = _pinNumber,
                };

                await insertInputs.CreateCustomer(customerDetails);
            }

        }

    }
}
