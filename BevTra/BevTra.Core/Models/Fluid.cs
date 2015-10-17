using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.Core.Models
{
    public class Fluid
    {
        public int String { get; set; }
        public String UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public String Contents { get; set; }
        public int Ounces { get; set; }
    }
}
