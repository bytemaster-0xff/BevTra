using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.Core.DeviceServices
{
    public class PlatformServices
    {
        static IPlatformServices _current;
        public static IPlatformServices Current
        {
            get
            {
                if (_current == null)
                    _current = GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<IPlatformServices>();

                return _current;
            }
        }
    }
}
