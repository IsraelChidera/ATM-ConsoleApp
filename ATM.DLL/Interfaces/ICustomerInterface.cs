using ATM.DLL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DLL.Interfaces
{
    public interface ICustomerInterface : IDisposable
    {
        //Task CreateCustomerDb();        
        Task CreateCustomerTable();
        Task CreateDb();
        Task<int> CreateCustomer(CustomerViewModel customer);
    }
}
