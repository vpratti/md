using System.Collections.ObjectModel;
using System.Web.Http.Results;
using System.Web.Security;
using AngularJSAuthentication.API.Entities;
using AngularJSAuthentication.API.Exceptions;
using AngularJSAuthentication.API.Extensions;
using AngularJSAuthentication.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using WebGrease.Css.Extensions;

namespace AngularJSAuthentication.API
{

    public class AuthRepository : IDisposable
    {
        private readonly AuthContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthRepository()
        {
            _context = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_context));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
        }

        public async Task<string> ResetPassword(IdentityUser user)
        {
            var provider = new DpapiDataProtectionProvider("VirtualClarityPNC"); //todo make configurable
            _userManager.UserTokenProvider =
                new DataProtectorTokenProvider<IdentityUser, string>(provider.Create("UserToken"));

            var resetToken = _userManager.GeneratePasswordResetToken(user.Id); //todo can send this token as part of initial email that user clicks and that then makes this call
            var temporaryPassword = Membership.GeneratePassword(10, 1);
            var result = await _userManager.ResetPasswordAsync(user.Id, resetToken, temporaryPassword);

            if (result.Succeeded)
            {
                return temporaryPassword;
            }

            throw new FailedResetPasswordException();
        }

        public async Task RegisterAdmin()
        {
            IdentityUser admin = await _userManager.FindByNameAsync("admin");

            if (admin == null)
            {
                if (!_roleManager.RoleExists("admin"))
                {
                    _roleManager.Create(new IdentityRole("admin"));
                }

                var user = new VirtualClarityUser
                {
                    UserName = "admin",
                    Email = string.Empty /*make configurable later*/,
                    FirstName = "admin",
                    LastName = "admin"
                };

                var adminResult = _userManager.Create(user, "password");

                if (adminResult.Succeeded)
                {
                    _userManager.AddToRole(user.Id, "admin");
                }   
            }
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new IdentityUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
            {
               AddRolesToUser(userModel, user);
            }

            return result;
        }

        public async Task<IdentityResult> DeleteUser(string userId)
        {
            var identityUser = _context.Users.First(i => i.Id.Equals(userId));
            var result = await _userManager.DeleteAsync(identityUser);
            return result;
        }

        public async Task<IdentityResult> UpdateUser(UpdateUserModel updateUserModel)
        {
            var identityUser = _context.Users.First(i => i.Id.Equals(updateUserModel.Id));
            identityUser.UserName = updateUserModel.UserName;
            identityUser.Email = updateUserModel.Email;
            identityUser.RemoveAllRoles();

            if (!string.IsNullOrEmpty(updateUserModel.Password))
            {
                _userManager.RemovePassword(identityUser.Id);
                _userManager.AddPassword(identityUser.Id, updateUserModel.Password);
            }

            var result = await _userManager.UpdateAsync(identityUser);

            if (result.Succeeded)
            {
                AddRoles(identityUser, updateUserModel);
            }

            return result;
        }

        public void AddRoles(IdentityUser identityUser, UpdateUserModel updateUserModel)
        {
            updateUserModel.Roles.ForEach(i => _userManager.AddToRole(identityUser.Id, i.Name));
        }

        private void AddRolesToUser(UserModel userModel, IdentityUser user)
        {
            userModel.Roles.ForEach(i =>
            {
                if (_roleManager.Roles.Any(j => j.Id.Equals(i.Id)))
                {
                    _userManager.AddToRole(user.Id, i.Name);
                }
            });
        }


        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = _context.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

           var existingToken = _context.RefreshTokens.SingleOrDefault(r => r.Subject == token.Subject && r.ClientId == token.ClientId);

           if (existingToken != null)
           {
             var result = await RemoveRefreshToken(existingToken);
           }
          
            _context.RefreshTokens.Add(token);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
           var refreshToken = await _context.RefreshTokens.FindAsync(refreshTokenId);

           if (refreshToken != null) {
               _context.RefreshTokens.Remove(refreshToken);
               return await _context.SaveChangesAsync() > 0;
           }

           return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
             return await _context.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _context.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
             return  _context.RefreshTokens.ToList();
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
            _userManager.Dispose();

        }
    }
}