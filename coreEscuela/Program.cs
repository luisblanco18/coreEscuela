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

            //ImprimirCursosEscuela(engine.Escuela);

            Dictionary<int, string> diccionario = new Dictionary<int, string>();

            var dictmp = engine.GetDiccionariosObjetos();
            engine.ImprimirDiccionario(dictmp,true);
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
