using System;
using System.Collections.Generic;
using System.Text;

namespace coreEscuela.Entidades
{
    public class Evaluacion : ObjetoEscuelaBase
    {
        public Alumno Alumno { get; set; }

        public Asignatura Asignatura { get; set; }

        public float Nota { get; set; }

        public override string ToString()
        {
            return $"{Nota},{Alumno.Nombre},{Asignatura.Nombre}";
        }

    }
}
