using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BevTra.Core.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;

namespace BevTra.Core
{
    public class DesignDataContext : IDataContext
    {
        public void Init()
        {

        }

        public Task AddFluid(Fluid fluid)
        {
            return Task.Delay(50);
        }

        public Task CreateUser(User user)
        {

            return Task.Delay(50);
        }

        public Task DeleteFluid(Fluid fluid)
        {

            return Task.Delay(50);
        }

        public async Task<User> Get(string accountId)
        {
            await Task.Delay(50);

            return new User()
            {

            };
        }

        public async Task<List<Fluid>> GetFluids(DateTime start, DateTime end)
        {
            await Task.Delay(50);

            return null;
        }

        public Task Update(User user)
        {
            return Task.Delay(50);
        }

        public Task UpdateFluid(Fluid fluid)
        {

            return Task.Delay(50);
        }

        public Models.User CurrentUser
        {
            get
            {
                return new User()
                {
                    FirstName = "Kevion",
                    LastName = "Wolf"
                };
            }
        }

        Microsoft.WindowsAzure.MobileServices.IMobileServiceClient _mockClient = new MockMobileSerivceClient();

        public Microsoft.WindowsAzure.MobileServices.IMobileServiceClient MobileServices
        {
            get { return _mockClient; }
        }

        private class MockMobileSerivceClient : Microsoft.WindowsAzure.MobileServices.IMobileServiceClient
        {
            public string ApplicationKey { get; set; }

            public Uri ApplicationUri { get; set; }

            public MobileServiceUser CurrentUser { get; set; }

            public MobileServiceJsonSerializerSettings SerializerSettings { get; set; }

            public IMobileServiceSyncContext SyncContext
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public IMobileServiceSyncTable GetSyncTable(string tableName)
            {
                throw new NotImplementedException();
            }

            public IMobileServiceSyncTable<T> GetSyncTable<T>()
            {
                throw new NotImplementedException();
            }

            public IMobileServiceTable GetTable(string tableName)
            {
                throw new NotImplementedException();
            }

            public IMobileServiceTable<T> GetTable<T>()
            {
                throw new NotImplementedException();
            }

            public Task<JToken> InvokeApiAsync(string apiName)
            {
                throw new NotImplementedException();
            }

            public Task<JToken> InvokeApiAsync(string apiName, JToken body)
            {
                throw new NotImplementedException();
            }

            public Task<JToken> InvokeApiAsync(string apiName, HttpMethod method, IDictionary<string, string> parameters)
            {
                throw new NotImplementedException();
            }

            public Task<JToken> InvokeApiAsync(string apiName, JToken body, HttpMethod method, IDictionary<string, string> parameters)
            {
                throw new NotImplementedException();
            }

            public Task<HttpResponseMessage> InvokeApiAsync(string apiName, HttpContent content, HttpMethod method, IDictionary<string, string> requestHeaders, IDictionary<string, string> parameters)
            {
                throw new NotImplementedException();
            }

            public Task<T> InvokeApiAsync<T>(string apiName)
            {
                throw new NotImplementedException();
            }

            public Task<T> InvokeApiAsync<T>(string apiName, HttpMethod method, IDictionary<string, string> parameters)
            {
                throw new NotImplementedException();
            }

            public Task<U> InvokeApiAsync<T, U>(string apiName, T body)
            {
                throw new NotImplementedException();
            }

            public Task<U> InvokeApiAsync<T, U>(string apiName, T body, HttpMethod method, IDictionary<string, string> parameters)
            {
                throw new NotImplementedException();
            }

            public Task<MobileServiceUser> LoginAsync(string provider, JObject token)
            {
                throw new NotImplementedException();
            }

            public async Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, JObject token)
            {
                await Task.Delay(500);
                return new MobileServiceUser("ABC123")
                {
                      
                };
            }

            public void Logout()
            {
                throw new NotImplementedException();
            }
        }
    }


}
