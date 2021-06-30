using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace curso_asp_netcore.Models
{
  //:ObjetoEscuelaBase indica que la clase Alumno hereda de ObjetoEscuelaBAse
    public class Alumno:ObjetoEscuelaBase 
    {
        [Required]
        public override string Nombre { get; set; }

        public string CursoId { get; set; }

        public Curso Curso { get; set; }

        public List<Evaluacion> Evaluaciones { get; set; }
    }
}