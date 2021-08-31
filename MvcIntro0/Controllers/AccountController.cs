using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcIntro0.Models;
using MvcIntro0.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcIntro0.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Account> _accountManager;
        private readonly SignInManager<Account> _loginManager;


        public AccountController(UserManager<Account> acntMngr, SignInManager<Account> lgInMngr)
        {
            _accountManager = acntMngr;
            _loginManager = lgInMngr;
        }


        public IActionResult Registration()
            => View();

        public IActionResult Login(string returnURL)
            => View(new LoginingViewModel { ReturnURL = returnURL });


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                Account currentAccount = new Account { Email = rvm.Name, UserName = rvm.Name, RoleId = 1 };
                IdentityResult identityResult = await _accountManager.CreateAsync(currentAccount, rvm.Password);

                if (identityResult.Succeeded)
                {
                    //await _registrationManager.SignInAsync(currentAccount, false);
                    return RedirectToAction("Login");

                }
                else
                {
                    foreach (IdentityError err in identityResult.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }

            return View(rvm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginingViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult loginRes
                    = await _loginManager.PasswordSignInAsync(lvm.Name, lvm.Password, false, false);

                if (loginRes.Succeeded)
                {
                    if (!string.IsNullOrEmpty(lvm.ReturnURL) && Url.IsLocalUrl(lvm.ReturnURL))
                    {
                        return Redirect(lvm.ReturnURL);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong name or password");
                }
            }

            return View(lvm);
        }


        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _loginManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
