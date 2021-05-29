using coreEscuela.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace coreEscuela.Entidades
{
    public class Curso : ObjetoEscuelaBase, ILugar
    {

        public TiposJornada Jornada { get; set; }

        public List<Asignatura> Asignaturas { get; set; }
        public List<Alumno> Alumnos { get; set; }

        public string Direccion { get; set; }

        public void LimpiarLugar() {
            Printer.DrawnLine();
            Console.WriteLine("Limpiando Establecimiento....");
            Console.WriteLine($"Curso {Nombre} Limpio");
        
        }

    }
}
