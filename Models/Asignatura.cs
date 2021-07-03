using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace curso_asp_netcore.Models
{
    public class Asignatura:ObjetoEscuelaBase
    {
        [Required(ErrorMessage="El Nombre de la Asignatura es Requerido")]
        public override string Nombre { get; set; }

        public string CursoId { get; set; }

        public string CursoNombre { get; set; }

        public Curso Curso { get; set; }

        public List<Evaluacion> Evaluaciones { get; set; }

    }
}