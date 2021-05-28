using System;
using System.Diagnostics;
namespace curso_asp_netcore.Models
{   
    //enviar informacion al debuger
    // [DebuggerDisplay("{Nota}, {Alumno.Nombre}, {Asignatura.Nombre}")]
    
    //declarar la clase como abstract permite heredar sus caracteristicas a otras clases
    //pero no permite que la clase sea instanciada
    public abstract class ObjetoEscuelaBase
    {
        public string UniqueId { get; set; }

        public string Nombre { get; set; }

        public ObjetoEscuelaBase()
        {
            UniqueId = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{Nombre}, {UniqueId}";
        }
    }
}