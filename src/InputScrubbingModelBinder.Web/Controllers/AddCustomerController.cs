using InputScrubbingModelBinder.Web.Example;
using Microsoft.AspNetCore.Mvc;

namespace InputScrubbingModelBinder.Web.Controllers
{
    public class AddCustomerController : Controller
    {
        public IActionResult Index()
        {
            return View(new AddCustomer());
        }

        [HttpPost]
        public IActionResult Index(AddCustomer entity)
        {
            return View(entity);
        }
    }
}
