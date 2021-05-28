using System;
using System.Collections.Generic;
namespace curso_asp_netcore.Models
{
  //:ObjetoEscuelaBase indica que la clase Alumno hereda de ObjetoEscuelaBAse
    public class Alumno:ObjetoEscuelaBase 
    {

        public List<Evaluacion> Evaluaciones { get; set; }

        public Alumno()
        {
          // UniqueId = Guid.NewGuid().ToString(); // se inicializa en la clase padre
          Evaluaciones = new List<Evaluacion>();
        }
    }
}