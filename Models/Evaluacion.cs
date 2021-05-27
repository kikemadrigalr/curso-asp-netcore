using System;
namespace curso_asp_netcore.Models
{
    public class Evaluacion:ObjetoEscuelaBase
    {
        // public string UniqueId { get; private set; }

        // public string Nombre { get; set; }

        public Alumno Alumno { get; set; }

        public Asignatura Asignatura { get; set; }

        public Double Nota { get; set; }

         public override string ToString()
        {
            return $"{Nota}, {Alumno.Nombre}, {Asignatura.Nombre}";
        }
    }
}