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
      var evaluaciones = CargarEvaluaciones(cursos, asignaturas, alumnos);

      // Enviar los datos a la BD
      // HasData verifica que no tenga datos para ejecutar una accion
      //hasData debe recibir un array para funcionar 
      modelBuilder.Entity<Escuela>().HasData(escuela);
      modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
      modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
      modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
      modelBuilder.Entity<Evaluacion>().HasData(evaluaciones.ToArray());
    }

    private static List<Evaluacion> CargarEvaluaciones(List<Curso> cursos, List<Asignatura> asignaturas, List<Alumno> alumnos)
    {
      int evaluacionesTotales = 2;
      string[] prueba = { "Parcial", "Final", "Preparatorio", "Quiz", "Acumulativo", "Práctico" };
      Random rnd = new Random();
      Random nota = new Random();
      var listaEvaluaciones = new List<Evaluacion>();


      foreach (var curso in cursos)
      {
        foreach (var alumno in alumnos)
        {
          foreach (var asignatura in asignaturas)
          {
            for (var i = 0; i < evaluacionesTotales; i++)
            {
              int numNombre = rnd.Next(0, 6);
              var tmp = new List<Evaluacion>(){
                     new Evaluacion() {
                         Nombre = $"{prueba[numNombre] }" + " " + $"{asignatura.Nombre}",
                         AlumnoId = alumno.Id,
                         AsignaturaId = asignatura.Id,
                         Nota = Math.Round(nota.NextDouble() * 5, 2)
                    }
                   };
              listaEvaluaciones.AddRange(tmp);
            }
          }
        }
      }
      return listaEvaluaciones;
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
                new Curso(){ EscuelaId = escuela.Id, Nombre = "101", Direccion = "Avenida Siempre Viva", Jornada = TiposJornada.Mañana},
                new Curso(){ EscuelaId = escuela.Id, Nombre = "201", Direccion = "Avenida Siempre Viva", Jornada = TiposJornada.Tarde},
                new Curso(){ EscuelaId = escuela.Id, Nombre = "301", Direccion = "Avenida Siempre Viva", Jornada = TiposJornada.Noche},
                new Curso(){ EscuelaId = escuela.Id, Nombre = "401", Direccion = "Avenida Siempre Viva", Jornada = TiposJornada.Mañana},
                new Curso(){ EscuelaId = escuela.Id, Nombre = "501", Direccion = "Avenida Siempre Viva", Jornada = TiposJornada.Tarde}
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