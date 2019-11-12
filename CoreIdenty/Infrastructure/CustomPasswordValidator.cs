using CoreIdenty.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdenty.Infrastructure
{
    public class CustomPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {
           List<IdentityError> errors = new List<IdentityError>();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError()
                {

                    Code = "PasswordContainsUsername",
                    Description = "Şifre içersinde kullanıcı adı olmamalıdır."
                });

            }
            if (password.Contains("123"))
            {
                errors.Add(new IdentityError()
                {
                    Code = "PasswordContainseSequance",
                    Description = "arda arda sayı yazılmamalıdır."
                });
            }
            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
