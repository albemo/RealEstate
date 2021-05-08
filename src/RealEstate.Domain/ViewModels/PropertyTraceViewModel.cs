using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.ViewModels
{
    public record PropertyTraceViewModel
    {
        public int Id { get; set; }

        public DateTimeOffset DateSale { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public decimal Tax { get; set; }

        public int? PropertyId { get; set; }

        public PropertyViewModel Property { get; set; }
    }
}
