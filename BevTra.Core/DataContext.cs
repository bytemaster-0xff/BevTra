using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.Core
{
    public class DataContext : IDataContext
    {
        Microsoft.WindowsAzure.MobileServices.MobileServiceClient _client;

        public void Init()
        {
            _client = new Microsoft.WindowsAzure.MobileServices.MobileServiceClient(Constants.AccountUrl, Constants.AccountKey);
        }

        public async Task AddFluid(Models.Fluid fluid)
        {
            var tbl = _client.GetTable<Models.Fluid>();

            await tbl.InsertAsync(fluid);
        }

        public Task<List<Models.Fluid>> GetFluids(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateFluid(Models.Fluid fluid)
        {
            var tbl = _client.GetTable<Models.Fluid>();

            await tbl.UpdateAsync(fluid);
        }

        public async Task DeleteFluid(Models.Fluid fluid)
        {
            var tbl = _client.GetTable<Models.Fluid>();

            await tbl.DeleteAsync(fluid);
        }

        public async Task CreateUser(Models.User user)
        {
            var tbl = _client.GetTable<Models.User>();

            await tbl.InsertAsync(user);
        }

        public async Task Update(Models.User user)
        {
            var tbl = _client.GetTable<Models.User>();

            await tbl.UpdateAsync(user);
        }

        public async Task<Models.User> Get(String accountId)
        {
            var tbl = _client.GetTable<Models.User>();
            return await tbl.LookupAsync(accountId);
        }

        public Models.User CurrentUser { get; set; }

        public Models.Fluid CurrentFluid { get; set; }

        public Microsoft.WindowsAzure.MobileServices.IMobileServiceClient MobileServices { get { return _client; } }
    }
}
