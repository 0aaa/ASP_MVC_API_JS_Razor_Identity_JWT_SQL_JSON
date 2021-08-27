using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcIntro0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly UserManager<Account> _accountManager;
        private readonly SignInManager<Account> _registrationManager;
        public AccountsController(UserManager<Account> acntMngr, SignInManager<Account> rgstrtnMngr)
        {
            _registrationManager = rgstrtnMngr;
            _accountManager = acntMngr;
        }
        public IActionResult Index()
            => View(_accountManager.Users.Include(acnt => acnt.Role).ToList());
        public IActionResult Addition()
            => View();
        [HttpPost]
        public async Task<IActionResult> Addition(Account newAccount)
        {
            if (ModelState.IsValid)
            {
                Account currentAccount = new Account { Email = newAccount.UserName, UserName = newAccount.UserName, RoleId = null };
                IdentityResult identityResult = await _accountManager.CreateAsync(currentAccount);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError err in identityResult.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View(newAccount);
        }
        public IActionResult Deletion(string id)
        {
            _accountManager.DeleteAsync(_accountManager.FindByIdAsync(id).Result);
            return RedirectToAction("Index");
        }
    }
}
