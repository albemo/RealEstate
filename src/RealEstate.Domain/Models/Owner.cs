using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Models
{
    public record Owner
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Photo{ get; set; }

        public DateTime Bithday { get; set; }

        public virtual IList<Property> Properties { get; protected set; } = new List<Property>();
    }
}
