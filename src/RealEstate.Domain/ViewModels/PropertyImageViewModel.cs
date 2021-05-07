using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.ViewModels
{
    public class PropertyImageViewModel
    {
        public int Id { get; set; }

        public int? PropertyId { get; set; }

        public PropertyViewModel Property { get; set; }

        public string File { get; set; }

        public bool Enabled { get; set; }
    }
}
