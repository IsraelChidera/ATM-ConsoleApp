using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ATM.DLL
{
    public class AtmDbConnection : IDisposable
    {
        private bool _disposed;
        private readonly string _connectionString;
        private SqlConnection _dbConnection = null;

        //Initial Catalog = AtmDBApp;
        public AtmDbConnection() : this(@"Data Source=ISRAEL-CHIDERA\SQLEXPRESS01;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {

        }

        public AtmDbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<SqlConnection> OpenConnection()
        {
            _dbConnection = new SqlConnection(_connectionString);
            await _dbConnection.OpenAsync();
            return _dbConnection;
        }

        public async Task CloseConnection()
        {
            if(_dbConnection?.State != ConnectionState.Closed)
            {
                //await _dbConnection?.CloseAsync();
                await _dbConnection.CloseAsync();
            }            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _dbConnection?.Dispose();
            }

            _disposed = true;           
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
    }
}
