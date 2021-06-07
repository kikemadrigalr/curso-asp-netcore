using System;
using System.Collections.Generic;
using System.Linq;
using curso_asp_netcore.Models;
using Microsoft.AspNetCore.Mvc;

namespace curso_asp_netcore.Controllers
{
    //Todo controlador debe Heredar de la clase Controller
    public class AlumnoController : Controller
    {
        private EscuelaContext _Context;
        //acceder al servicio de Base de datos en memoria
        //mediante un constructor especial que recibe el contexto de Base de datos
        public AlumnoController(EscuelaContext context)
        {
            _Context = context;
        }

        //Cada metodo que ejecute una vista debe devolver un tipo de dato
        //En ese caso IActionResult
        public IActionResult Index()
        {
            return View(
                // new Alumno {
                //     Nombre = "Bart Simpson",
                //     Id = Guid.NewGuid ().ToString()
                // }
                _Context.Alumnos.FirstOrDefault()
            );
        }
        public IActionResult MultiAlumno()
        {
            // var listaAlumnos = GenerarListaAlumnos();

                //Aqui estamos especificando entre comillas la vista que queremos mostrar
                return View("MultiAlumno", _Context.Alumnos);
        }
    }
}