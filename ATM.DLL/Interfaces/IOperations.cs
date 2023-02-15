using ATM.DLL.Model;
using System;
using System.Threading.Tasks;

namespace ATM.DLL.Interfaces
{
    public interface IOperations : IDisposable
    {
        Task<int> Deposit(DepositViewModel depositView);

        Task<int> Withdraw(WithdrawViewModel withdraw);

        Task<int> Transfer(TransferViewModel transfer);

        Task CreateTransferTable();

        Task CreateWithdrawTable();

        Task CreateDepositTable();
    }
}
