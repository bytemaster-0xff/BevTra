using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.Core
{

    public interface IDataContext
    {
        Task AddFluid(Models.Fluid fluid);
        Task<List<Models.Fluid>> GetFluids(DateTime start, DateTime end);
        Task UpdateFluid(Models.Fluid fluid);
        Task DeleteFluid(Models.Fluid fluid);

        Task CreateUser(Models.User user);
        Task Update(Models.User user);
        Task<Models.User> Get(String accountId);
        void Init();
        Models.User CurrentUser { get; }

        Microsoft.WindowsAzure.MobileServices.IMobileServiceClient MobileServices { get; } 
    }
}
