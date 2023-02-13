using ATM.DLL.Model;
using System;
using System.Threading.Tasks;

namespace ATM.DLL.Interfaces
{
    public interface IOperations : IDisposable
    {
        Task<int> Deposit(int amount);
        Task<int> Withdraw(WithdrawViewModel withdraw);
        Task<int> Transfer(int amount);

        Task CreateWithdrawTable();
    }
}
