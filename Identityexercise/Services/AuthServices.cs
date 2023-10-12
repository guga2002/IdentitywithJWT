using Identityexercise.Contexts;
using Identityexercise.Models;
using Identityexercise.ResponseAndRequest.Request;
using Identityexercise.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identityexercise.Services
{
    public class AuthServices:IAuth
    {
        private readonly UserManager<User> _usermanager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IConfiguration config;
        private readonly DbContextForManager _Database;
        public AuthServices(UserManager<User> _usr,IConfiguration con,DbContextForManager database,RoleManager<IdentityRole> role)
        {
            _usermanager = _usr;
            config = con;
            _Database = database;
            _rolemanager = role;
        }

        public async Task<bool> Register(RegisterRequest user)
        {
            User usra = new User()
            {
                Email = user.Email,
                UserName = user.Username,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = user.Phone,
                Name = user.Name,
                age = user.age,
                Surname = user.Surname,
            };

            if (usra != null)
            {
                var res = await _usermanager.CreateAsync(usra, user.Password);

                if (res.Succeeded)
                {
                    // Check if the role exists, and create it if it doesn't
                    string role = user.Role.ToUpper();
                    if (role == "ADMIN" || role == "USER" || role == "MODERATOR" || role == "GUEST" || role == "MANAGER")
                    {
                        await Console.Out.WriteLineAsync(  "roli  validuria");

                        var roleExists = await _rolemanager.RoleExistsAsync(user.Role.ToUpper());

                        await Console.Out.WriteLineAsync( roleExists.ToString());

                        if (!roleExists)
                        {
                            var roleResult = await _rolemanager.CreateAsync(new IdentityRole(user.Role.ToUpper()));

                            if (!roleResult.Succeeded)
                            {

                                return false;
                            }
                        }

                        await _usermanager.AddToRoleAsync(usra, user.Role.ToUpper());
                        await Console.Out.WriteLineAsync( "warmatebit daemata");
                        await _Database.SaveChangesAsync();
                        return true;
                    }
                }
            }

            return false;
        }


        public async Task<string> SignIn(SIgnInRequest sign)
        {
            var res= await _usermanager.FindByNameAsync(sign.Username);
            if (res == null) return null;

            var checkedpass =await _usermanager.CheckPasswordAsync(res, sign.Password);
            if(checkedpass)
            {
                var rolik = await _usermanager.GetRolesAsync(res);
                if (rolik.FirstOrDefault() != null)
                {
                    await Console.Out.WriteLineAsync( rolik.First());
                    var re = await GenerateJwtToken(res, rolik.First());
                    if (re == null) return null;
                    return re;
                }
                await Console.Out.WriteLineAsync( "role is null there");
            }

            return null;
           
        }

        private  async Task<string> GenerateJwtToken(User user, string Role)
        {
            if (user != null)
            {
                var claims = new[]
                {
              new Claim(ClaimTypes.NameIdentifier, user.Id),
              new Claim(ClaimTypes.Name, user.UserName),
              new Claim(ClaimTypes.Role,Role),
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:key").Value));

                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: config.GetSection("Jwt:Issuer").Value,
                    audience: config.GetSection("Jwt:Audience").Value,
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;
        }
    }
}
