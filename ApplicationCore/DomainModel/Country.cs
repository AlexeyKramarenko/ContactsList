using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DomainModel
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Town> Towns { get; set; }
    }
}
