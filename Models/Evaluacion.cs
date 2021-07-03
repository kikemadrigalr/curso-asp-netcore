using System;
using System.ComponentModel.DataAnnotations;
namespace curso_asp_netcore.Models
{
    public class Evaluacion:ObjetoEscuelaBase
    {
        [Required(ErrorMessage="El Nombre de la Evaluacion es Requerido")]
        public override string Nombre { get; set; }

        public string AlumnoId { get; set; }

        public Alumno Alumno { get; set; }

        public string AsignaturaId { get; set; }

        public string AsignaturaNombre { get; set; }

        public Asignatura Asignatura { get; set; }

        public Double Nota { get; set; }

         public override string ToString()
        {
            return $"{Nota}, {Alumno.Nombre}, {Asignatura.Nombre}";
        }
    }
}