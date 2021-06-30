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

        public IActionResult Create(){
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Curso curso){

            if(ModelState.IsValid)
            {
                var escuela = _Context.Escuelas.FirstOrDefault();
                curso.EscuelaId = escuela.Id;

                _Context.Cursos.Add(curso);
                _Context.SaveChanges();
                ViewBag.Mensaje = "Curso Creado con Éxito";
                return View("Index", curso);
            }
            else
            {
                return  View(curso);
            }

            
        }

        [Route("Curso/Update/{id}")]
        public IActionResult Update(string id)
        {
            var curso = from cur in _Context.Cursos
                            where cur.Id == id
                            select cur;
            return View(curso.SingleOrDefault());
        }

        [HttpPost]
        [Route("Curso/Update/{id}")]
        public IActionResult Update(string id, Curso newData)
        {
            if (id != null && ModelState.IsValid)
            {
                var cursoSearch = from cur in _Context.Cursos
                            where cur.Id == id
                            select cur;

                var curso = cursoSearch.SingleOrDefault();

                //Update
                curso.Nombre = newData.Nombre;
                curso.Direccion = newData.Direccion;
                curso.Jornada = newData.Jornada;
                _Context.Cursos.Update(curso);
                _Context.SaveChanges();

                // return View("Index", newData);
                return RedirectToAction("MultiCurso");
            }
            else
            {
                ViewBag.MensajeError = "Error al modificar Curso";
                return View("Index");
            }
        }

        [Route("Curso/Delete/{id}")]
        public IActionResult Delete(string id)
        {
            if(id != null)
            {
                var cursoSearch = from cur in _Context.Cursos
                            where cur.Id == id
                            select cur;

                var curso = cursoSearch.SingleOrDefault();
                if(curso != null)
                {
                    return View("Delete", curso);
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

        [Route("Curso/Delete/{id}")]
        [HttpPost]
        public IActionResult Delete(string id, Curso cursoDelete)
        {
            if(id != null)
            {
                var cursoSearch = from cur in _Context.Cursos
                            where cur.Id == id
                            select cur;

                var curso = cursoSearch.SingleOrDefault();
                if(curso != null && curso.Id == cursoDelete.Id)
                {
                    _Context.Cursos.Remove(curso);
                    _Context.SaveChanges();
                    return RedirectToAction("Multicurso");
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
    }
}