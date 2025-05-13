using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Techan.Enums;
using Techan.Models;
using Techan.ViewModels.Account;

namespace Techan.Controllers
{
    public class AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager, RoleManager<Role> _roleManager) : Controller
    {
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View();
            User user = new User
            {
                Email = vm.Email,
                UserName = vm.Username,
                Fullname = vm.FullName
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            var roleResult = await _userManager.AddToRoleAsync(user, nameof(Roles.User));
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                await _userManager.DeleteAsync(user);
                return View();
            }
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View();
            User? user = null;
            if (vm.UsernameOrEmail.Contains("@"))
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
            else
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
            if (user is null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }

            //var passResult=await _userManager.CheckPasswordAsync(user, vm.Password);
            //if(!passResult)
            //{
            //    ModelState.AddModelError("", "Username or password is incorrect");
            //    return View();
            //}
            //await _signInManager.SignInAsync(user, vm.RememberMe);

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "You reached max attemp count. Wait until" + user.LockoutEnd);

                }
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "You cannot sign in. Contact with admin please");
                }
                else
                    ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //public async Task SeedDatas()
        //{
        //    if (await _roleManager.Roles.AnyAsync())
        //        return;
        //    foreach(var role in Enum.GetValues(typeof(Roles)))
        //    {
        //        await _roleManager.CreateAsync(new Role(role.ToString()));
        //    }
        //}

        public async Task SeedDatas()
        {
            if (await _roleManager.Roles.AnyAsync())
            {

                foreach (var role in Enum.GetValues(typeof(Roles)))
                {
                    await _roleManager.CreateAsync(new Role(role.ToString()));
                }

            }

            if (!await _userManager.Users.AnyAsync(x => x.NormalizedUserName == "SUPERADMIN"))
            {
               var user=new Models.User
                {
                    UserName = "superadmin",
                    Email = "superadmin@gmail.com",
                    Fullname = "Superadmin"
                };
                await _userManager.CreateAsync(user, "superadmin123");
                await _userManager.AddToRoleAsync(user, nameof(Roles.SuperAdmin));
            }

            if (!await _userManager.Users.AnyAsync(x => x.NormalizedUserName == "ADMIN"))
            {
                var user = new Models.User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Fullname = "admin"
                };
                await _userManager.CreateAsync(user, "admin123");
                await _userManager.AddToRoleAsync(user, nameof(Roles.Admin));
            }
        }
    }
}
