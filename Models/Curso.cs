using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace curso_asp_netcore.Models
{
    public class Curso:ObjetoEscuelaBase
    {
        [Required(ErrorMessage="El Nombre del Curso es Requerido")]
        [StringLength(5)]
        public override string Nombre { get; set; }

        [Required(ErrorMessage="Debe Incluir una direccion")]   
        [MinLength(10, ErrorMessage="La longitud minima es de 10 caracteres")]
        [Display(Prompt="Direccion Correspondencia", Name="Dirección")]
        public string Direccion { get; set; }

        public TiposJornada Jornada { get; set; }

        public List<Asignatura> Asignaturas { get; set; }

        public List<Alumno> Alumnos { get; set; }

        public string EscuelaId { get; set; }

        public Escuela Escuela { get; set; }
  }
}