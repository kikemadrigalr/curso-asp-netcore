using System;
using curso_asp_netcore.Models;
using Microsoft.AspNetCore.Mvc;

namespace curso_asp_netcore.Controllers
{
    //Todo controlador debe Heredar de la clase Controller
    public class AsignaturaController : Controller
    {
        //Cada metodo que ejecute una vista debe devolver un tipo de dato
        //En ese caso IActionResult
        public IActionResult Index()
        {
            //instancia de la escuela para que sea enviada a la vista
            var asignatura = new Asignatura();
            asignatura.Nombre = "Programacion";
            asignatura.UniqueId = Guid.NewGuid().ToString();
            return View(asignatura);
        }
    }
}