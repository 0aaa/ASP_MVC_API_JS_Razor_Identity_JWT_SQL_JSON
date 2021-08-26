using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
    public class AuthenticationController : Controller
    {
        private StoreContext _context;
        public AuthenticationController(StoreContext cntxt)
            => _context = cntxt;
        public IActionResult Registration()
            => View();
        public IActionResult Logining()
            => View();
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                Account currentAccount = await _context.Accounts
                    .FirstOrDefaultAsync(acnt => acnt.Name == rvm.Name && acnt.Password == rvm.Password);
                if (currentAccount == null)
                {
                    Account acntToAdd = new Account { Name = rvm.Name, Password = rvm.Password };
                    Role acntToAddRole = await _context.Roles.FirstOrDefaultAsync(rle => rle.Title == "customer");
                    if (acntToAddRole != null)
                    {
                        acntToAdd.Role = acntToAddRole;
                    }
                    _context.Accounts.Add(acntToAdd);
                    await _context.SaveChangesAsync();
                    await Authentication(acntToAdd);
                    return RedirectToAction("Index", "Crud");
                }
            }
            return View(rvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logining(LoginingViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                Account currentAccount = await _context.Accounts
                    .Include(acnt => acnt.Role)
                    .FirstOrDefaultAsync(acnt => acnt.Name == lvm.Name && acnt.Password == lvm.Password);
                if (currentAccount != null)
                {
                    await Authentication(currentAccount);
                    return RedirectToAction("Index", "Crud");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong name or password");
                }
            }
            return View(lvm);
        }
        private async Task Authentication(Account account)
        {
            List<Claim> claimsLst
                = new List<Claim> {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, account.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role?.Title)
                };
            ClaimsIdentity clmsIdntty
                = new ClaimsIdentity(claimsLst, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(clmsIdntty));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Logining", "Authentication");
        }
    }
}
