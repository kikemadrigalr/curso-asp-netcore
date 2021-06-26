using System;
using System.Linq;
using System.Collections.Generic;
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
        [Route("Alumno")]
        [Route("Alumno/Index")]
        [Route("Alumno/{id}")]
        [Route("Alumno/Index/{id}")]
        public IActionResult Index(string id)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                var alumnos = from alum in _Context.Alumnos
                                where alum.Id == id
                                select alum;

            return View(alumnos.SingleOrDefault());
            }
            else
            {
                return View("MultiAlumno",_Context.Alumnos);
            }
            
        }

        public IActionResult MultiAlumno()
        {
            // var listaAlumnos = GenerarListaAlumnos();

                //Aqui estamos especificando entre comillas la vista que queremos mostrar
                return View("MultiAlumno", _Context.Alumnos);
        }
    }
}