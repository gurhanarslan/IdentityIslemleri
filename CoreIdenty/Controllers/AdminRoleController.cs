using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreIdenty.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdenty.Controllers
{
    public class AdminRoleController : Controller
    {
        private RoleManager<IdentityRole> roleManager;

        public AdminRoleController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
       
        }
        public IActionResult Index()
        {
            return View(roleManager.Roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(name));
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
            return View(name);
        }
    }
}