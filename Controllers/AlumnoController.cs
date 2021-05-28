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
        //Cada metodo que ejecute una vista debe devolver un tipo de dato
        //En ese caso IActionResult
        public IActionResult Index()
        {
            return View(
                new Alumno {
                    Nombre = "Bart Simpson",
                    UniqueId = Guid.NewGuid ().ToString()
                }
            );
        }
        public IActionResult MultiAlumno()
        {
            var listaAlumnos = GenerarListaAlumnos();

                //Aqui estamos especificando entre comillas la vista que queremos mostrar
                return View("MultiAlumno", listaAlumnos);
        }

         private List<Alumno> GenerarListaAlumnos(){
            string[] nombre1 = { "Bart", "Homero", "Ned", "Lisa", "Marge", "Maggy", "Milhouse", "Bob", "Montgomery" };
            string[] apellido1 = { "Simpson", "Van Houten", "Flanders", "Patiño", "Bruns"};

            var listaAlumnos =  from n1 in nombre1
                                from a1 in apellido1
                                select new Alumno(){ Nombre = $"{n1} {a1}"};

            return listaAlumnos.OrderBy(alum => alum.UniqueId).ToList();
        }
    }
}