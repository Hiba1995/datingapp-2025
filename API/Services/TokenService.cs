using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService //IConfiguration it read from appSettings.json
{
    public string CreateToken(AppUser user)
    {
       var tokenKey = config["TokenKey"] ?? throw new Exception ("Cannot get the token key");
        if (tokenKey.Length < 64)
        {
            throw new Exception("Your Token Key needs to be >= 64 characters"); // this only tell the developer that the key is small 

        }
           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)); //) Convert the secret key into a security key object

           var claims = new List<Claim> //Create the “claims” (user info inside token)
           {
               new (ClaimTypes.Email, user.Email),// the calim is of type email
               new (ClaimTypes.NameIdentifier, user.Id), // the claim is of type id 
               //WT payload is not encrypted, it’s only signed. So don’t put sensitive data like passwords.

           };

           var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); // create the signature
           var tokenDiscriptor = new SecurityTokenDescriptor //Build the token descriptor (token settings)
           {
               Subject = new ClaimsIdentity(claims), //the identity (claims)
               Expires = DateTime.UtcNow.AddDays(7), //IT WILL EXPIRE AFTER 7 DAYS 
               SigningCredentials = creds,
           };
            //Create the token using a token handler
           var tokenHandler = new JwtSecurityTokenHandler();
           var token = tokenHandler.CreateToken(tokenDiscriptor);
           return tokenHandler.WriteToken(token);
    }
}
