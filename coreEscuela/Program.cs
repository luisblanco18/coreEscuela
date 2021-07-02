using coreEscuela.App;
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
            //AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            //AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.Beep(2000, 1000, 1);
            //AppDomain.CurrentDomain.ProcessExit -= AccionDelEvento;
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            var reporteador = new Reporteador(engine.GetDiccionariosObjetos());

            var evaList = reporteador.GetListaEvaluaciones();
            var listAsg = reporteador.GetListaAsignaturas();
            var listEvalXAsig = reporteador.GetDicEvaluaXAsig();
            var listaPromXAsig = reporteador.GetPromeAlumnPorAsignatura();
            var listPorPromedio = reporteador.GetListaTopPromedio(5);


            Printer.WriteTitle("Captura de una evaluacion por consola");
            var newEval = new Evaluacion();
            string nombre;
            string notastring;
            float nota;

            WriteLine("Ingrese el nombre de la evaluacion");
            Printer.PresioneENTER();
            nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Printer.WriteTitle("El valor del nombre no puede ser vacio");
                WriteLine("Saliendo del programa");
            }
            else
            {
                newEval.Nombre = nombre.ToLower();
                WriteLine("El nombre de la evaluacion ha sido ingresado correctamente");

            }

            WriteLine("Ingrese el nota de la evaluacion");
            Printer.PresioneENTER();
            notastring = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(notastring))
            {
                Printer.WriteTitle("El valor de la nota no puede ser vacio");
                WriteLine("Saliendo del programa");
            }
            else
            {
                try
                {
                    newEval.Nota = float.Parse(notastring);
                    if (newEval.Nota < 0 || newEval.Nota > 5)
                    {
                        throw new ArgumentOutOfRangeException("La nota debe estar entre 0 y 5");
                    }
                    WriteLine("La nota de la evaluacion ha sido ingresado correctamente");
                }
                catch (ArgumentOutOfRangeException Arge)
                {
                    WriteLine(Arge.Message);
                    WriteLine("Saliendo del programa");
                }
                catch (Exception)
                {
                    Printer.WriteTitle("El valor de la nota no es un numero valido");
                    WriteLine("Saliendo del programa");
                }
                finally
                {
                    WriteLine("FINALLY");
                }

            }


        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("SALIENDO");
            Printer.Beep(3000, 1000, 3);
            Printer.WriteTitle("SALIO");
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
