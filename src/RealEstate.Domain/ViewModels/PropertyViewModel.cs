using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.ViewModels
{
    public class PropertyViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public string CodeInterval { get; set; }

        public DateTime Year { get; set; }

        public int? OwnerId { get; set; }

        public OwnerViewModel Owner { get; set; }

        public virtual IList<PropertyImageViewModel> PropertyImages { get; set; } = new List<PropertyImageViewModel>();

        public virtual IList<PropertyTraceViewModel> PropertyTraces { get; set; } = new List<PropertyTraceViewModel>();
    }
}
