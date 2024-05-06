using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class UserData
    {
        public List<(string pageAddress, int status)> PageStatus { get; set; }

        public UserData()
        {
            PageStatus = new List<(string, int)>();
        }
    }
}
