using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class MenuVO
    {
        public string module_name { get; set; }
        public string icon { get; set; }
        public List<SystemPages> page_list { get; set; }
    }
}
