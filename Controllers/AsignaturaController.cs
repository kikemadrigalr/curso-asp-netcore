using System;
using System.Collections.Generic;
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
            return View(
                new Asignatura {
                    Nombre = "Programacion",
                    // UniqueId = Guid.NewGuid ().ToString()
                }
            );
        }
        public IActionResult MultiAsignatura()
        {
            // var asignatura = new Asignatura();
            // asignatura.Nombre = "Programacion";
            // asignatura.UniqueId = Guid.NewGuid().ToString();

            var listaAsignaturas = new List<Asignatura> () {
                new Asignatura {
                    Nombre = "Matemáticas",
                    // UniqueId = Guid.NewGuid ().ToString()
                },
                new Asignatura {
                    Nombre = "Educación Física",
                    // UniqueId = Guid.NewGuid ().ToString()
                },
                new Asignatura {
                    Nombre = "Castellano",
                    // UniqueId = Guid.NewGuid ().ToString()
                },
                new Asignatura {
                    Nombre = "Ciencias Naturales",
                    // UniqueId = Guid.NewGuid ().ToString()
                },
                new Asignatura {
                    Nombre = "Programacion",
                    // UniqueId = Guid.NewGuid ().ToString()
                }
            }; 

                //Aqui estamos especificando entre comillas la vista que queremos mostrar
                return View("MultiAsignatura",listaAsignaturas);
        }
    }
}