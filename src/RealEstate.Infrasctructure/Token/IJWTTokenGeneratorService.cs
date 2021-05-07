using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrasctructure.Token
{
    public interface IJWTTokenGeneratorService
    {
        string GenerateToken(IdentityUser<int> user, IList<Claim> claims);
    }
}
