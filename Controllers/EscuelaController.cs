using Microsoft.AspNetCore.Mvc;

namespace curso_asp_netcore.Controllers
{
    //Todo controlador debe Heredar de la clase Controller
    public class EscuelaController : Controller
    {
        //Cada metodo que ejecute una vista debe devolver un tipo de dato
        //En ese caso IActionResult
        public IActionResult Index()
        {
            return View();
        }
    }
}