using System;
using curso_asp_netcore.Models;
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
            //instancia de la escuela para que sea enviada a la vista
            var escuela = new Escuela();
            escuela.AnioCreacion = 2005;
            escuela.UniqueId = Guid.NewGuid().ToString();
            escuela.Nombre = "Escuela Primaria de Springfield";
            escuela.Ciudad = "Springfield";
            escuela.Pais = "Estados Unidos";
            escuela.TipoEscuela = TiposEscuela.Primaria;
            escuela.Direccion = "Avenida Siempre Viva";

            //envio de la instancia escuela a la vista Index
            return View(escuela);
        }
    }
}