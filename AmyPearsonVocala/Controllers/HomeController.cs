using Microsoft.AspNetCore.Mvc;

namespace AmyPearsonVocala.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}
