using System;
using System.Linq;
using System.Collections.Generic;
using curso_asp_netcore.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace curso_asp_netcore.Controllers
{
    //Todo controlador debe Heredar de la clase Controller
    public class AsignaturaController : Controller
    {

        private EscuelaContext _Context;
        //acceder al servicio de Base de datos en memoria
        //mediante un constructor especial que recibe el contexto de Base de datos
        public AsignaturaController(EscuelaContext context)
        {
            _Context = context;
        }
        //Cada metodo que ejecute una vista debe devolver un tipo de dato
        //En ese caso IActionResult
        // public IActionResult Index()
        // {
        //     return View(_Context.Asignaturas.FirstOrDefault());
        // }

        //*****ENRRUTAMENTO*****//
        [Route("Asignatura/")]
        [Route("Asignatura/{asignaturaId}")]
        [Route("Asignatura/Index")]
        [Route("Asignatura/Index/{asignaturaId}")]
        public IActionResult Index(string asignaturaId)
        {
            if(!string.IsNullOrWhiteSpace(asignaturaId))
            {
                var asignatura = from asig in _Context.Asignaturas
                                where asig.Id == asignaturaId
                                select asig;

            return View(asignatura.SingleOrDefault());
            }
            else
            {
                return View("MultiAsignatura",_Context.Asignaturas);
            }
            
        }


        public IActionResult MultiAsignatura()
        {
                //Aqui estamos especificando entre comillas la vista que queremos mostrar
                return View("MultiAsignatura",_Context.Asignaturas);
        }

        public IActionResult Create()
        {
            ViewBag.items = obtenerListaCursos();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Asignatura asignatura)
        {
            asignatura.CursoId = asignatura.CursoId.ToString();
            if(ModelState.IsValid)
            {
                Console.WriteLine(asignatura);

                var curso = from c in _Context.Cursos
                            where c.Id == asignatura.CursoId
                            select c;
                Curso cursoSearch = curso.SingleOrDefault();
                asignatura.Curso = cursoSearch;
                asignatura.CursoId = cursoSearch.Id;
                asignatura.CursoNombre = cursoSearch.Nombre;

                _Context.Asignaturas.Add(asignatura);
                _Context.SaveChanges();
                // console.WriteLine(alumno);
                ViewBag.Mensaje = "Asignatura Creada con Éxito";
                return View("Index", asignatura);
            }
            else
            {
                return View();
            }
        }

        [Route("Asignatura/Update/{id}")]
        public IActionResult Update(string id)
        {
            Asignatura asignatura = obtenerAsignatura(id);

            ViewBag.items = obtenerListaCursos();

            return View(asignatura);
        }

        [HttpPost]
        [Route("Asignatura/Update/{id}")]
        public IActionResult Update(string id, Asignatura newData)
        {
            if (id != null && ModelState.IsValid)
            {
                Asignatura asignatura = obtenerAsignatura(id);

                asignatura.Nombre = newData.Nombre;
                asignatura.Curso = newData.Curso;
                asignatura.CursoId = newData.CursoId.ToString();
                asignatura.CursoNombre = ObtenerNombreCurso(asignatura.CursoId);
                _Context.Asignaturas.Update(asignatura);
                _Context.SaveChanges();

                return RedirectToAction("MultiAsignatura");
            }
            else
            {
                ViewBag.MensajeError = "Error al modificar Curso";
                return View("Index");
            }
        }

        [Route("Asignatura/Delete/{id}")]
        public IActionResult Delete(string id)
        {
            if(id != null)
            {
                 Asignatura asignatura = obtenerAsignatura(id);

                if(asignatura != null)
                {
                    return View("Delete", asignatura);
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

        [Route("Asignatura/Delete/{id}")]
        [HttpPost]
        public IActionResult Delete(string id, Asignatura asignaturaDelete)
        {
            if(id != null)
            {
                Asignatura asignatura = obtenerAsignatura(id);

                if(asignatura != null && asignatura.Id == asignaturaDelete.Id)
                {
                    _Context.Asignaturas.Remove(asignatura);
                    _Context.SaveChanges();
                    return RedirectToAction("MultiAsignatura");
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

        private String ObtenerNombreCurso(string idCurso)
        {
            var curso = from c in _Context.Cursos
                            where c.Id == idCurso
                            select c;
                Curso cursoSearch = curso.SingleOrDefault();
                String nombre = cursoSearch.Nombre;
                return nombre;
        }

        private Asignatura obtenerAsignatura(string id)
        {
            var asignatura = from asign in _Context.Asignaturas
                            where asign.Id == id
                            select asign;
                Asignatura asignaturaSearch = asignatura.SingleOrDefault();
                return asignaturaSearch;
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