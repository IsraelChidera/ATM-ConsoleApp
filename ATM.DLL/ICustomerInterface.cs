using ATM.DLL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DLL
{
    public interface ICustomerInterface:IDisposable
    {
        Task CreateCustomerDb();        
        Task CreateCustomerTable();

        Task<int> CreateCustomer(CustomerViewModel customer);
    }
}
 