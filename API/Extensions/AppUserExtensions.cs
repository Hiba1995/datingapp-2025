using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

public static class AppUserExtensions // static no nedd to create a new instance
{
    public static UserDto ToDto(this AppUser user, ITokenService tokenService) //it's an extension method 
    {
                return new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = tokenService.CreateToken(user)
        };
    }

}
