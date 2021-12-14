
using API.Model;
using API.Models;
using API.ViewModel;
using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    //[Authorize]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountController(AccountRepository repository) : base(repository)
        {
            this.accountRepository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM loginVM)
        {
            var jwtToken = await accountRepository.Login(loginVM);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("JWToken", token);
            return RedirectToAction("index", "dashboard");
        }

        public IActionResult Index()
        {
            return View("Account");
        }
    }
}