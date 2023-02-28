using ATM.DLL.Interfaces;
using ATM.DLL;

namespace ATM.UI
{
    internal class Program
    {
        
        static async Task Main(string[] args)
        {

            Application application = new Application();
            await application.Run();

           /* Console.WriteLine("Trying out views");
            try
            {
                Console.Clear();
                Console.WriteLine("============================================================");
                Console.WriteLine("\tAll Transfer Transactions");
                Console.WriteLine("============================================================");
                using (ITransaction admin = new TransactionService(new AtmDbConnection()))
                {
                    await admin.TransactionsView();
                };                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }*/

        }
    }
}