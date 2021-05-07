using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Models
{
    public class Property
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [Column(TypeName = "decimal(9, 2)")]
        public decimal Price { get; set; }

        public string CodeInterval { get; set; }

        public DateTime Year { get; set; }

        public int? OwnerId { get; set; }

        public Owner Owner { get; set; }

        public virtual IList<PropertyImage> PropertyImages { get; protected set; } = new List<PropertyImage>();

        public virtual IList<PropertyTrace> PropertyTraces { get; protected set; } = new List<PropertyTrace>();
    }
}
