using RealEstate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.ViewModels
{
    public record OwnerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Photo { get; set; }

        public DateTime Bithday { get; set; }

        public IList<PropertyViewModel> Properties { get; set; } = new List<PropertyViewModel>();
    }
}
