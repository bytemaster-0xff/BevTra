using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;

namespace BevTra.Core
{

    // https://azure.microsoft.com/en-us/documentation/articles/mobile-services-windows-store-dotnet-get-started-offline-data/
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
          return (await _client.GetTable<Models.User>().Where(usr=>usr.AccountId == accountId).ToListAsync()).FirstOrDefault();
        }

        public Models.User CurrentUser { get; set; }

        public Models.Fluid CurrentFluid { get; set; }

        public Microsoft.WindowsAzure.MobileServices.IMobileServiceClient MobileServices { get { return _client; } }
    }

    public class SyncHandler : IMobileServiceSyncHandler
    {
        Microsoft.WindowsAzure.MobileServices.MobileServiceClient _client;

        public SyncHandler(Microsoft.WindowsAzure.MobileServices.MobileServiceClient client)
        {
            _client = client;
        }

        public async Task<JObject> ExecuteTableOperationAsync(IMobileServiceTableOperation operation)
        {
            MobileServicePreconditionFailedException error;

            do
            {
                error = null;
                try
                {
                    return await operation.ExecuteAsync();
                }
                catch (MobileServicePreconditionFailedException ex)
                {
                    error = ex;
                }

                if (error != null)
                {
                    bool resolveWithLocal = true;
                    bool resolveWithServer = true;

                    var localItem = operation.Item.ToObject<Models.Fluid>();
                    var serverValue = error.Value;
                    if (resolveWithLocal)
                    {
                        //Result with local Version
                        operation.Item[MobileServiceSystemColumns.Version] = serverValue[MobileServiceSystemColumns.Version];
                        continue;
                    }
                    else if (resolveWithServer)
                    {
                        //resolve with server version
                        return (JObject)serverValue;
                    }
                    else
                    {
                        operation.AbortPush();
                        error = null;
                    }
                }

            }
            while (error != null);

            return null;
        }

        public Task OnPushCompleteAsync(MobileServicePushCompletionResult result)
        {
            return Task.FromResult(0);

        }
    }
}
