using System;
using System.Linq;
using System.Collections.Generic;
using curso_asp_netcore.Models;
using Microsoft.AspNetCore.Mvc;

namespace curso_asp_netcore.Controllers
{
    //Todo controlador debe Heredar de la clase Controller
    public class CursoController : Controller
    {
        private EscuelaContext _Context;
        //acceder al servicio de Base de datos en memoria
        //mediante un constructor especial que recibe el contexto de Base de datos
        public CursoController(EscuelaContext context)
        {
            _Context = context;
        }

        //Cada metodo que ejecute una vista debe devolver un tipo de dato
        //En ese caso IActionResult
    //    [Route("Asignatura/Index")]
    //     [Route("Asignatura/Index/{asignaturaId}")]
        [Route("Curso")]
        [Route("Curso/{id}")]
        [Route("Curso/Index")]
        [Route("Curso/Index/{id}")]
        public IActionResult Index(string id)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                var cursos = from cur in _Context.Cursos
                                where cur.Id == id
                                select cur;

            return View(cursos.SingleOrDefault());
            }
            else
            {
                return View("MultiCurso",_Context.Cursos);
            }
            
        }

        public IActionResult MultiCurso()
        {
            // var listaAlumnos = GenerarListaAlumnos();

                //Aqui estamos especificando entre comillas la vista que queremos mostrar
                return View("MultiCurso", _Context.Cursos);
        }
    }
}