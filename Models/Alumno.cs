using System;
using System.Collections.Generic;
namespace curso_asp_netcore.Models
{
  //:ObjetoEscuelaBase indica que la clase Alumno hereda de ObjetoEscuelaBAse
    public class Alumno:ObjetoEscuelaBase 
    {
        // public string UniqueId { get; private set; }

        // public string Nombre { get; set; }

        //los atributos UniqueId y Nombre los hereda de la clase padre

        public List<Evaluacion> Evaluaciones { get; set; }

        public Alumno()
        {
          // UniqueId = Guid.NewGuid().ToString(); // se inicializa en la clase padre
          Evaluaciones = new List<Evaluacion>();
        }
    }
}