using System;
using API.DTOs;
using API.Interfaces;
using DatingApp.Entities;

namespace API.Extensions;

public static class AddUserExtensions
{
public static UserDto ToDto(this AppUser user, ITokenService tokenService)
{
    return new UserDto
    {
        Id = user.Id,
        Email = user.Email,
        DisplayName = user.DisplayName,
        imageUrl = user.ImageUrl,
        token = tokenService.CreateToken(user)
    };
}
}