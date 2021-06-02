using Microsoft.EntityFrameworkCore;

namespace curso_asp_netcore.Models
{
    //Entity Framework
    // Esta clase se utilizara para manejar la base de datos
    //crea un contexto con lo que estariamos consultando en la BD
    //hereda de la clase DbContext que viene del namespace Microsoft.EntityFrameworkCore
    public class EscuelaContext : DbContext
    {
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }

        public DbSet<Alumno> Alumnos { get; set; }

        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Evaluacion> Evaluaciones { get; set; }

        // constructor especial integrandolo con ASP Netcore
        //Contexto Inyectado
        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
        {
            
        }
    }
}