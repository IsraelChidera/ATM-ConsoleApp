using ATM.DLL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATM.DLL.Interfaces
{
    public interface IAdminInterface : IDisposable
    {       
        Task<IEnumerable<DepositViewModel>> DepositTransactions();

        Task<IEnumerable<WithdrawViewModel>> WithdrawTransactions();

        Task<IEnumerable<TransferViewModel>> TransferTransactions();

        Task ViewAllTransactions();

    }
}
