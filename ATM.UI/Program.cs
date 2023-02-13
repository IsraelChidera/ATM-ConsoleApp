using ATM.BLL;
using ATM.DLL;
using ATM.DLL.Model;

namespace ATM.UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {            
            Utility.HomeContent();

            ValidateInputs validate = new();
  
            //await validate.InsertCustomerInputs();

            //await validate.ValidateCustomerInputs();



            
            await validate.ValidateCardDetails();
        }
    }
}