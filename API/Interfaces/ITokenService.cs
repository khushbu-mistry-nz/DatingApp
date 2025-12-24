using System;
using DatingApp.Entities;

namespace API.Interfaces;

public interface ITokenService
{
 string CreateToken(AppUser user);
}
