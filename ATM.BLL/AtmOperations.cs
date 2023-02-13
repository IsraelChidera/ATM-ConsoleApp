using ATM.DLL;
using ATM.DLL.Interfaces;
using ATM.DLL.Model;
using System.Threading.Tasks;

namespace ATM.BLL
{
    public class AtmOperations
    {
        public async Task RunWithdraw()
        {
            using (IOperations withdraw = new OperationService(new AtmDbConnection()) )
            {
                await withdraw.CreateWithdrawTable();
            }

            string name = "Edeh";
            string accountNumber = "676376";
            string amount = "3232";

            using (IOperations withdraw = new OperationService(new AtmDbConnection()))
            {
                WithdrawViewModel userWithdraw = new WithdrawViewModel
                {
                    Name= name,
                    AccountNumber= accountNumber,
                    Amount= amount,
                };

                await withdraw.Withdraw(userWithdraw);
            }


        }
    }
}
