using RealEstate.Domain.Models;
using RealEstate.Infrasctructure.Data;
using RealEstate.Infrasctructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.WebApi.Services
{
    public class OwnerService : BaseRepository<Owner>,  IOwnerService
    {
        public OwnerService(IRepository<Owner> repository) : base (repository)
        {
        }
    }
}
