using ATM.DLL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DLL.Interfaces
{
    public interface ITransaction:IDisposable
    {
        Task TransactionsView();
    }
}
