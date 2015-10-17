using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.Core.Models
{
    public class User
    {
        public String Id { get; set; }
        public String AccountId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastAcceessed { get; set; }
    }   
}
