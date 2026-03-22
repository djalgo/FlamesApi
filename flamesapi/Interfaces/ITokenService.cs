using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flamesapi.Entities;

namespace flamesapi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}