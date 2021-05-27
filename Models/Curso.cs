using System.Collections.Generic;
using System;

namespace curso_asp_netcore.Models
{
    public class Curso:ObjetoEscuelaBase
    {
        public TiposJornada Jornada { get; set; }

        public List<Asignatura> Asignaturas { get; set; }

        public List<Alumno> Alumnos { get; set; }
        public string Direccion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
  }
}