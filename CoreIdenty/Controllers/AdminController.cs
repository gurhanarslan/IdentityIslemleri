using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreIdenty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdenty.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;



        public AdminController(UserManager<ApplicationUser> uManager, IPasswordHasher<ApplicationUser> _passwordHasher, IPasswordValidator<ApplicationUser> _passwordValidator)
        {
            userManager = uManager;
            passwordValidator = _passwordValidator;
            passwordHasher = _passwordHasher;
        }

    
        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = model.UserName;
                    user.Email = model.Email;

                var result = await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user!=null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
           
            }
            else
            {
                ModelState.AddModelError("","user not found");
            }
            return View("Index", userManager.Users);
        }
        [HttpGet]
        public async Task<IActionResult> Uptade(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Uptade(string Id,string Password,string Email)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                user.Email = Email;

                IdentityResult validpass = null;

                if (!string.IsNullOrEmpty(Password))
                {
                    validpass = await passwordValidator.ValidateAsync(userManager, user,Password);

                    if (validpass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, Password);
                    }
                    else
                    {
                        foreach (var item in validpass.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                if (validpass.Succeeded)
                {
                    var result = await userManager.UpdateAsync(user);


                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }

                }

             
            }
            
            else
            {
                ModelState.AddModelError("", "User Not Found");

            }
            return View(user);
        }
       
    }
}