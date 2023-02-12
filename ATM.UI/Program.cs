using ATM.BLL;
using ATM.DLL;
using ATM.DLL.Model;

namespace ATM.UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ValidateInputs validate = new();

            await validate.CreateDb();
            /*using (ICustomerInterface customerService = new CustomerService(new AtmDbConnection()))
            {
                var customer = new CustomerViewModel
                {
                    Pin = "2323",
                    CardNumber = "323233"
                };
                await customerService.CreateCustomer(customer);
            };*/
            await validate.ValidateCustomerInputs();
        }
    }
}