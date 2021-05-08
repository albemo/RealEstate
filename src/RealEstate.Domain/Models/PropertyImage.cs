using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Models
{
    public record PropertyImage
    {
        public int Id { get; set; }

        public int? PropertyId { get; set; }

        public Property Property { get; set; }

        public string File { get; set; }

        public bool Enabled { get; set; }
    }
}
