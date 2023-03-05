using ATM.BLL;
using ATM.DLL;
using ATM.DLL.Interfaces;

namespace ATM.UI
{
    internal class Program
    {

        static async Task Main(string[] args)
        {

            Application application = new Application();
            await application.Run();                       

        }
    }
}