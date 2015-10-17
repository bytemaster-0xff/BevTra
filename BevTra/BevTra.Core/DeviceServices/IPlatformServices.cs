using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.Core.DeviceServices
{
    public interface IPlatformServices
    {
        Task ShowPopupAsync(String title, String message);

        Task ConfirmAsync(String title, String message);
    }
}
