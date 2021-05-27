using System;
using System.Collections.Generic;

namespace curso_asp_netcore.Models
{
    public class Escuela:ObjetoEscuelaBase
    {
        //otra forma de crear y encapsular las propiedades y hacer setter y getter
        public int AnioCreacion{ get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public List<Curso> Cursos { get; set; }

        // la propiedad TipoEscuela sera del TiposEscuelas
        //el cual es un objeto Enumeraable creado a nivel de las entidades
        public TiposEscuela TipoEscuela { get; set; }
        public string Direccion { get; set; }

    //otra forma de escribir el Constructor utilizando igualacion por tuplas
        public Escuela(string nombre, int anio) => (Nombre, AnioCreacion) = (nombre, anio);

        //crear un constructor diferenta, para inicializar el objeto de otra manera
        //y utilizar parametros opcionales}
        public Escuela(string nombre, int anio, TiposEscuela tipo, string pais="", string ciudad=""){
          (Nombre, AnioCreacion) = (nombre, anio);
          Pais = pais;
          Ciudad = ciudad;
        }

        public Escuela()
        {
            
        }

        //sobreescribiendo el metodo ToSting para la clase Escuela
        // (\n) nueva linea 
        //{System.Environment.NewLine} agrega el caracter nueva linea equivalente para el sistema operativo que este en uso
        public override string ToString()
        {
          return $"Nombre: \"{Nombre}\", Tipo: {TipoEscuela}, {System.Environment.NewLine}Pais: {Pais}, Ciudad: {Ciudad}";
        }

    }
}