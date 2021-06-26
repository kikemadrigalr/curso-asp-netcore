using System;
using System.Linq;
using System.Collections.Generic;
using curso_asp_netcore.Models;
using Microsoft.AspNetCore.Mvc;

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
        [Route("Asignatura/{iasignaturaId}")]
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
    }
}