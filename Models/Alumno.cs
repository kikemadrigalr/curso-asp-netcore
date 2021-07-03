using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace curso_asp_netcore.Models
{
  //:ObjetoEscuelaBase indica que la clase Alumno hereda de ObjetoEscuelaBAse
    public class Alumno:ObjetoEscuelaBase 
    {
        [Required(ErrorMessage="El Nombre del Alumno es Requerido")]
        public override string Nombre { get; set; }

        public string CursoId { get; set; }

        public string CursoNombre { get; set; }

        public Curso Curso { get; set; }

        public List<Evaluacion> Evaluaciones { get; set; }
    }
}