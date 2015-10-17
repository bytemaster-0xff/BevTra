using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BevTra.Core.Models;

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

    }
}
