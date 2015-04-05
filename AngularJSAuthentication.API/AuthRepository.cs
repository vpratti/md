using System.Web.Security;
using AngularJSAuthentication.API.Constants;
using AngularJSAuthentication.API.Entities;
using AngularJSAuthentication.API.Exceptions;
using AngularJSAuthentication.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGrease.Css.Extensions;

namespace AngularJSAuthentication.API
{

    public class AuthRepository : IDisposable
    {
        private readonly AuthContext _context;
        private readonly UserManager<VirtualClarityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthRepository()
        {
            _context = new AuthContext();
            _userManager = new UserManager<VirtualClarityUser>(new UserStore<VirtualClarityUser>(_context));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
        }

        public async Task<string> ResetPassword(IdentityUser user)
        {            
            var temporaryPassword = Membership.GeneratePassword(10, 1);
            await _userManager.RemovePasswordAsync(user.Id);
            var result = await _userManager.AddPasswordAsync(user.Id, temporaryPassword);

            if (result.Succeeded)
            {
                return temporaryPassword;
            }

            throw new FailedResetPasswordException();
        }

        public async Task RegisterAdmin()
        {
            IdentityUser admin = await _userManager.FindByNameAsync(UserConstants.Admin);

            if (admin == null)
            {
                if (!_roleManager.RoleExists(UserConstants.Admin))
                {
                    _roleManager.Create(new IdentityRole(UserConstants.Admin));
                }

                if (!_roleManager.RoleExists(UserConstants.Guest))
                {
                    _roleManager.Create(new IdentityRole(UserConstants.Guest));
                }

                var user = new VirtualClarityUser(UserConstants.Admin, string.Empty, UserConstants.Admin,
                    UserConstants.Admin, false);

                var adminResult = _userManager.Create(user, UserConstants.Password);

                if (adminResult.Succeeded)
                {
                    _userManager.AddToRole(user.Id, UserConstants.Admin);
                }   
            }
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new VirtualClarityUser(userModel);

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
            {
               AddRolesToUser(userModel, user);
            }

            return result;
        }

        public async Task<IdentityResult> RegisterAnonymousUser(UserModel userModel)
        {
            var user = new VirtualClarityUser(userModel)
            {
                LockoutEnabled = true,
                LockoutEndDateUtc = new DateTime(3000, 1, 1)
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
            var identityUser = _userManager.FindById(userId);
            var result = await _userManager.DeleteAsync(identityUser);
            return result;
        }

        public async Task<IdentityResult> UpdateUser(UpdateUserModel updateUserModel)
        {
            var identityUser = _userManager.FindByEmail(updateUserModel.Email);
            identityUser.FirstName = updateUserModel.FirstName;
            identityUser.LastName = updateUserModel.LastName;
            RemoveRoles(identityUser);
            AddRoles(identityUser, updateUserModel);

            if (!string.IsNullOrEmpty(updateUserModel.Password))
            {
                _userManager.RemovePassword(identityUser.Id);
                _userManager.AddPassword(identityUser.Id, updateUserModel.Password);
            }

            var result = await _userManager.UpdateAsync(identityUser);

            return result;
        }

        public void RemoveRoles(VirtualClarityUser user)
        {
            var roleNames = new List<string>();
            user.Roles.ForEach(i => roleNames.Add(_roleManager.FindById(i.RoleId).Name));
            roleNames.ForEach(i => _userManager.RemoveFromRole(user.Id, i));
        }

        //todo refactor for duplication
        public void AddRoles(VirtualClarityUser identityUser, UpdateUserModel updateUserModel)
        {
            updateUserModel.Roles.ForEach(i => _userManager.AddToRole(identityUser.Id, i.Name));
        }

        //todo refactor for duplication
        private void AddRolesToUser(UserModel userModel, IdentityUser user)
        {
            if (userModel.Roles != null && userModel.Roles.Any())
            {
                userModel.Roles.ForEach(i =>
                {
                    if (_roleManager.Roles.Any(j => j.Id.Equals(i.Id)))
                    {
                        _userManager.AddToRole(user.Id, i.Name);
                    }
                });
            }
            else
            {
                _userManager.AddToRole(user.Id, "guest");
            }
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
             await RemoveRefreshToken(existingToken);
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

        public async Task<IdentityResult> CreateAsync(VirtualClarityUser user)
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