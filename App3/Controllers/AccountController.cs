using App3.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
          var user =  await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return Json(true);
            }
            return Json($"Email {email} already exist");
        }




        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {

                var User = new IdentityUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email
                };

               var result = await _userManager.CreateAsync(User, registerViewModel.Password);
                if(result.Succeeded)
                {
                   await _signInManager.SignInAsync(User, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        //public async Task<IHttpActionResult> changePassword(UsercredentialsModel usermodel)
        //{
        //    ApplicationUser user = await AppUserManager.FindByIdAsync(usermodel.Id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    user.PasswordHash = AppUserManager.PasswordHasher.HashPassword(usermodel.Password);
        //    var result = await AppUserManager.UpdateAsync(user);
        //    if (!result.Succeeded)
        //    {
        //        //throw exception......
        //    }
        //    return Ok();
        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, 
                   isPersistent: loginViewModel.RememberMe, false);
                if (result.Succeeded)
                {  
                    if(!string.IsNullOrEmpty(returnUrl))
                    {
                        //if we want to redirect to the local url then we can use the
                        //LocalRedirect method which will ensure if there any valunerable urls it will not redirect. 
                        //return Redirect(returnUrl);
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("AddIncome", "Income");
                }
                else
                {
                        ModelState.AddModelError("", "Invalid Login attempt!");
                }
            }
            return View(loginViewModel);
        }

    }
}
