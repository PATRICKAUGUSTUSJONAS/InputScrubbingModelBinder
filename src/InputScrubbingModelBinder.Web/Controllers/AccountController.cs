using InputScrubbingModelBinder.Web.Example;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InputScrubbingModelBinder.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View(GetStoredAccounts());
        }

        public IActionResult Create()
        {
            var dto = new Account();

            return View(dto);
        }

        [HttpPost]
        public IActionResult Create(Account dto)
        {
            if (ModelState.IsValid)
            {
                var storedAccounts = GetStoredAccounts();
                storedAccounts.Add(dto);
                storedAccounts.Sort((a1, a2) => a1.Name.CompareTo(a2.Name));
                HttpContext.Session.SetObjectAsJson("accounts", storedAccounts);

                return RedirectToAction("Index");
            }

            return View(dto);
        }


        private List<Account> GetStoredAccounts()
        {
            var storedAccounts = HttpContext.Session.GetObjectFromJson<List<Account>>("accounts");
            if (storedAccounts == null) storedAccounts = new List<Account>();
            return storedAccounts;
        }
    }
}
