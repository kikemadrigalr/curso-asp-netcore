using System;
using System.Linq;
using System.Collections.Generic;
using curso_asp_netcore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Create()
        {
            ViewBag.items = obtenerListaCursos();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Alumno alumno)
        {
            // alumno.CursoId = alumno.CursoId.ToString();
            if(ModelState.IsValid)
            {
                Console.WriteLine(alumno.CursoId);

                var curso = from c in _Context.Cursos
                            where c.Id == alumno.CursoId
                            select c;
               Curso cursoSearch = curso.SingleOrDefault();
                alumno.Curso = cursoSearch;
                alumno.CursoId = cursoSearch.Id;
                alumno.CursoNombre = cursoSearch.Nombre;

                _Context.Alumnos.Add(alumno);
                _Context.SaveChanges();
                // console.WriteLine(alumno);
                ViewBag.Mensaje = "Alumno Creado con Éxito";
                return View("Index", alumno);
            }
            else
            {
                return View(alumno);
            }
        }

        [Route("Alumno/Update/{id}")]
        public IActionResult Update(string id)
        {
            Alumno alumno = obtenerAlumno(id);

            ViewBag.items = obtenerListaCursos();

            return View(alumno);
        }

        [HttpPost]
        [Route("Alumno/Update/{id}")]
        public IActionResult Update(string id, Alumno newData)
        {
            if (id != null && ModelState.IsValid)
            {
                Alumno alumno = obtenerAlumno(id);

                alumno.Nombre = newData.Nombre;
                alumno.Curso = newData.Curso;
                alumno.CursoId = newData.CursoId;
                // alumno.CursoNombre = newData
                _Context.Alumnos.Update(alumno);
                _Context.SaveChanges();

                return RedirectToAction("Multialumno");
            }
            else
            {
                ViewBag.MensajeError = "Error al modificar Curso";
                return View("Index");
            }
        }

        [Route("Alumno/Delete/{id}")]
        public IActionResult Delete(string id)
        {
            if(id != null)
            {
                 Alumno alumno = obtenerAlumno(id);

                if(alumno != null)
                {
                    return View("Delete", alumno);
                }
                else
                {
                    return View("NotFoud");
                }
            }
            else
            {
                return View("NotFound");
            }
        }

        [Route("Alumno/Delete/{id}")]
        [HttpPost]
        public IActionResult Delete(string id, Alumno alumnoDelete)
        {
            if(id != null)
            {
                Alumno alumno = obtenerAlumno(id);

                if(alumno != null && alumno.Id == alumnoDelete.Id)
                {
                    _Context.Alumnos.Remove(alumno);
                    _Context.SaveChanges();
                    return RedirectToAction("Multialumno");
                }
                else
                {
                    return View("NotFound");
                }
            }
            else{
                return View("Multicurso");
            }
            
        }

        private Alumno obtenerAlumno(string id)
        {
            var alumno = from alum in _Context.Alumnos
                            where alum.Id == id
                            select alum;
                Alumno alumnoSearch = alumno.SingleOrDefault();
                
                return alumnoSearch;
        }

        private List<SelectListItem> obtenerListaCursos()
        {
            List<Curso> listaCursos = null;
            listaCursos = (from c in _Context.Cursos
                            select new Curso
                            {
                                Id = c.Id,
                                Nombre = c.Nombre
                            }).ToList();

            List<SelectListItem> items = listaCursos.ConvertAll(d => {
                return new SelectListItem(){
                    Text = d.Nombre.ToString(),
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });

            return items;
        }
    }
}