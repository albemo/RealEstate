using RealEstate.Domain.Models;
using RealEstate.Infrasctructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.WebApi.Services
{
    public class PropertyService : BaseRepository<Property>, IPropertyService
    {
        public PropertyService(IRepository<Property> repository) : base(repository)
        {
        }
    }
}
