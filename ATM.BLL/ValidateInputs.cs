using ATM.DLL;
using ATM.DLL.Model;
using System;
using System.Threading.Tasks;

namespace ATM.BLL
{
    public class ValidateInputs
    {
        public async Task CreateDb() 
        {
            using (ICustomerInterface customerService = new CustomerService(new AtmDbConnection()) )
            {
                await customerService.CreateCustomerDb();
                await customerService.CreateCustomerTable();
            }
                
        }
       
        public async Task ValidateCustomerInputs()
        {
            ValidateCardNumber();
            ValidatePinNumber();

            using (ICustomerInterface customerService = new CustomerService(new AtmDbConnection()))
            {
                {
                    var customer = new CustomerViewModel
                    {
                        Pin = "2323",
                        CardNumber = "323233"
                    };
                    await customerService.CreateCustomer(customer);
                };
            }
            
        }

        public void ValidateCardNumber()
        {
            Console.WriteLine("Card number must be up to 6 digits");

            string cardNumber = Console.ReadLine();
            while (true)
            {
                try
                {
                    if (cardNumber.Length == 6 && int.TryParse(cardNumber, out int cardNum))
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
                        cardNumber = Console.ReadLine();
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


        }

        public void ValidatePinNumber()
        {

            Console.WriteLine("\nPin must be up to 4 digits");
            string pinNumber = Console.ReadLine();
            Console.WriteLine(pinNumber);

            while (true)
            {
                try
                {
                    if (pinNumber.Length == 4 && int.TryParse(pinNumber, out int pinNum))
                    {
                        Utility.Animation();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n**********************************************");
                        Console.WriteLine($"PIN: {pinNum}");
                        Console.WriteLine("Congrats... You can now do your transactions");
                        Console.WriteLine("**********************************************");
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid PIN. Please try again");
                        pinNumber = Console.ReadLine();
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
        }
    }
}
