using System;
using System.Linq;
using System.Collections.Generic;
using curso_asp_netcore.Models;
using Microsoft.AspNetCore.Mvc;

namespace curso_asp_netcore.Controllers
{
    //Todo controlador debe Heredar de la clase Controller
    public class EvaluacionController : Controller
    {

        private EscuelaContext _Context;
        //acceder al servicio de Base de datos en memoria
        //mediante un constructor especial que recibe el contexto de Base de datos
        public EvaluacionController(EscuelaContext context)
        {
            _Context = context;
        }
        //Cada metodo que ejecute una vista debe devolver un tipo de dato
        //En ese caso IActionResult
        public IActionResult Index()
        {
            return View(
                // new Asignatura {
                //     Nombre = "Programacion",
                //     // UniqueId = Guid.NewGuid ().ToString()
                // }
                _Context.Evaluaciones.FirstOrDefault()
            );
        }

        public IActionResult MultiEvaluacion()
        {
                //Aqui estamos especificando entre comillas la vista que queremos mostrar
                return View("MultiEvaluacion",_Context.Evaluaciones);
        }
    }
}