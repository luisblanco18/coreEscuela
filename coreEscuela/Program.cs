using coreEscuela.Entidades;
using coreEscuela.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace coreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(5000, cantidad:3);
            ImprimirCursosEscuela(engine.Escuela);

            //var listaObjetos = engine.GetObjetosEscuela(
            //    out int conteoEvaluaciones,
            //    out int conteoCursos,
            //    out int conteoAsignaturas,
            //    out int conteoAlumnos
            //);

            Dictionary<int, string> diccionario = new Dictionary<int, string>();

            diccionario.Add(5,"Ejemplo");
            diccionario.Add(6,"Ejemplo2");

            foreach (var keyValPair in diccionario)
            {
                Console.WriteLine($"key: {keyValPair.Key}, value: {keyValPair.Value}");
            }

            Printer.WriteTitle("Acceso a Diccionario");
            diccionario[0] = "Ejemplo3";
            Console.WriteLine(diccionario[0]);

            Printer.WriteTitle("Otro Diccionario");
            var dic = new Dictionary<string,string>();
            dic["Luna"] = "Cuerpo celeste";
            Console.WriteLine(dic["Luna"]);
            dic["Luna"] = "Nombre Propio";
            Console.WriteLine(dic["Luna"]);

        }

        private static void ImprimirCursosEscuela(Escuela escuela)
        {
            Printer.WriteTitle("Cursos de la escuela");
            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre: {curso.Nombre}, Id: {curso.UniqueId}");
                }
            }
        }

    }
}
