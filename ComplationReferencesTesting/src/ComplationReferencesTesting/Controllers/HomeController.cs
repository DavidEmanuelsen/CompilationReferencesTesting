using Microsoft.AspNetCore.Mvc;

namespace CompilationReferencesTesting.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
