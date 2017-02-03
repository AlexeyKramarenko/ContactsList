using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DomainModel
{
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TownID { get; set; }          
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string TownName { get; set; }
    }

  
}
