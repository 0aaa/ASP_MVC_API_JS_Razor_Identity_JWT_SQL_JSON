using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Title { get; set; }
        public List<Account> Accounts { get; set; }


        public Role()
            => Accounts = new List<Account>();
    }
}
