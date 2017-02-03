using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DomainModel
{
    public class Town
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CountryID { get; set; }
        public List<Company> Companies { get; set; }
    }
}
