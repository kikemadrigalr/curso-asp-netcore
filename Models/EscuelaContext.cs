using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

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

      //***********CARGAR CURSOS DE LA ESCUELA****************//
      var cursos = CargarCursos(escuela);

      //***********Por Cada Curso Cargar Asignatura****************//
      var asignaturas = CargarAsignaturas(cursos);

      //***********Por cada curso Cargar Alumnos****************//
      var alumnos = CargarAlumnos(cursos);

      //***********CARGAR Evaluciones****************//

       // Enviar los datos a la BD
      // HasData verifica que no tenga datos para ejecutar una accion
      //hasData debe recibir un array para funcionar 
      modelBuilder.Entity<Escuela>().HasData(escuela);
      modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
      modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
      modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
    }

    private List<Alumno> CargarAlumnos(List<Curso> cursos)
    {
        var listaAlumnos = new List<Alumno>();

        Random rnd = new Random();
        foreach (var curso in cursos)
        {
            int cantRandom = rnd.Next(5,20);
            var temList = GenerarListaAlumnos(curso, cantRandom);
            listaAlumnos.AddRange(temList);
        }
        return listaAlumnos;
    }

    private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
    {
        var listaCompleta = new List<Asignatura>();

      foreach (var curso in cursos)
      {
        var tempList = new List<Asignatura>(){
              new Asignatura() {CursoId = curso.Id, Nombre = "Matemáticas" },
              new Asignatura() {CursoId = curso.Id, Nombre = "Educación Física" },
              new Asignatura() {CursoId = curso.Id, Nombre = "Castellano" },
              new Asignatura() {CursoId = curso.Id, Nombre = "Ciencias Naturales" },
              new Asignatura() {CursoId = curso.Id, Nombre = "Programacion" },
          };

          listaCompleta.AddRange(tempList);
      }
      return listaCompleta;
    }

    private static List<Curso> CargarCursos(Escuela escuela)
    {
      var cursosEscuela = new List<Curso>(){
                new Curso(){ EscuelaId = escuela.Id, Nombre = "101", Jornada = TiposJornada.Mañana},
                new Curso(){ EscuelaId = escuela.Id, Nombre = "201", Jornada = TiposJornada.Tarde},
                new Curso(){ EscuelaId = escuela.Id, Nombre = "301", Jornada = TiposJornada.Noche},
                new Curso(){ EscuelaId = escuela.Id, Nombre = "401", Jornada = TiposJornada.Mañana},
                new Curso(){ EscuelaId = escuela.Id, Nombre = "501", Jornada = TiposJornada.Tarde}
            };

            return cursosEscuela;
    }

    private List<Alumno> GenerarListaAlumnos(Curso curso, int cantidad)
         {
            string[] nombre1 = { "Bart", "Homero", "Ned", "Lisa", "Marge", "Maggy", "Milhouse", "Bob", "Montgomery" };
            string[] apellido1 = { "Simpson", "Van Houten", "Flanders", "Patiño", "Bruns"};

            var listaAlumnos =  from n1 in nombre1
                                from a1 in apellido1
                                select new Alumno(){ CursoId = curso.Id, Nombre = $"{n1} {a1}"};

            return listaAlumnos.OrderBy(alum => alum.Id).Take(cantidad).ToList();
        }
        
    }
}