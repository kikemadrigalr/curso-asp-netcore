using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        //Ingresar datos en la base de datos en memoria con Entity ramework
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // invocamos al mismo metodo debido a que se esta sobreescribiendo
            //de esta manera el metodo ejecuta su funcionalidad por defecto
            base.OnModelCreating(modelBuilder);

            // Luego en la sobreescritura hacemos lo que necesitamos 
             var escuela = new Escuela();
            escuela.AnioCreacion = 2005;
            escuela.Nombre = "Escuela Primaria de Springfield";
            escuela.Ciudad = "Springfield";
            escuela.Pais = "Estados Unidos";
            escuela.TipoEscuela = TiposEscuela.Primaria;
            escuela.Direccion = "Avenida Siempre Viva";

            // Enviar los datos a la BD
            // HasData verifica que no tenga datos para ejecutar una accion
            //hasData debe recibir un array para funcionar 
            modelBuilder.Entity<Escuela>().HasData(escuela);

            // Crear Lista de asignaturas en la BD
            modelBuilder.Entity<Asignatura>().HasData(
                new Asignatura { Nombre = "Matemáticas" },
                new Asignatura { Nombre = "Educación Física" },
                new Asignatura { Nombre = "Castellano" },
                new Asignatura { Nombre = "Ciencias Naturales" },
                new Asignatura { Nombre = "Programacion" }
            );

            //Generar lista de alumnos en la BD
            modelBuilder.Entity<Alumno>().HasData( GenerarListaAlumnos().ToArray());
        }

        private List<Alumno> GenerarListaAlumnos()
         {
            string[] nombre1 = { "Bart", "Homero", "Ned", "Lisa", "Marge", "Maggy", "Milhouse", "Bob", "Montgomery" };
            string[] apellido1 = { "Simpson", "Van Houten", "Flanders", "Patiño", "Bruns"};

            var listaAlumnos =  from n1 in nombre1
                                from a1 in apellido1
                                select new Alumno(){ Nombre = $"{n1} {a1}"};

            return listaAlumnos.OrderBy(alum => alum.Id).ToList();
        }
        
    }
}