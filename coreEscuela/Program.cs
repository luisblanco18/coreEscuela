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

            var listaObjetos = engine.GetObjetosEscuela();

            var listaIlugar = from obj in listaObjetos
                              where obj is ILugar
                              select (ILugar)obj;

            //engine.Escuela.LimpiarLugar();
            

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
