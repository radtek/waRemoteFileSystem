using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using waRemoteFileSystem.DataBase;
using waRemoteFileSystem.Models;
using static waRemoteFileSystem.Utils.CHash;

namespace waRemoteFileSystem.Core
{
    public class UserService :IUserService
    {
        private MyDbContext db;
        private IConfiguration config;

        public UserService(MyDbContext dbContext, IConfiguration Configuration) 
        {
            db = dbContext;
            config = Configuration;
        }

        public async Task<UserResponse> AuthenticateAsync(UserRequest request)
        {
            UserResponse usr = new UserResponse();

            var res = await db.tbUsers.Include(x => x.Role)
                               .FirstOrDefaultAsync(x => x.Username == request.Username && x.Password == HashSha256.Get(request.Password) );

            if (res == null) return null;

            var SecretStr = config.GetSection("JwtToken:SecretKey").Value;
            var key = Encoding.ASCII.GetBytes(SecretStr);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                           {
                                new Claim(ClaimTypes.Name, res.Id.ToString()),
                                new Claim(ClaimTypes.Role, res.Role.UserAccess)
                           }),
                Expires = DateTime.UtcNow.AddYears(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            usr.Token = tokenHandler.WriteToken(token);
            usr.FirstName = res.FirstName;
            usr.LastName = res.LastName;
            usr.Username = res.Username;
            usr.Access = res.Role.UserAccess;
            usr.Id = res.Id;

            return usr;
        }

        public async Task<UserResponse> RegisterUserAsync(RegisterUserModel model)
        {
            var eu = await db.tbUsers.FirstOrDefaultAsync(x => x.Username == model.Username);
            if (eu != null) return new UserResponse() { Status = 0, StatusMessage = "User exits" };


            var u = new tbUser();
            u.FirstName = model.FirstName;
            u.LastName = model.LastName;
            u.Username = model.Username;
            u.Password = HashSha256.Get(model.Password);
            u.EMail = model.EMail;
            u.RoleId = 1;

            await db.tbUsers.AddAsync(u);
            await db.SaveChangesAsync();

            return await AuthenticateAsync(new UserRequest() { Username = model.Username, Password = model.Password });
        }
    }


    public interface IUserService
    {
        Task<UserResponse> AuthenticateAsync(UserRequest request);
        Task<UserResponse> RegisterUserAsync(RegisterUserModel model);
    }
}
