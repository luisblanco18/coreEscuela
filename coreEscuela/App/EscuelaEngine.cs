using coreEscuela.Entidades;
using coreEscuela.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coreEscuela
{
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {
        }

        public void Inicializar()
        {
            Escuela = new Escuela("Platzi", 2012, TiposEscuela.Secundaria
                                      , ciudad: "La Paz", pais: "Bolivia");

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }
        public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dic,
            bool imprEval = false)
        {
            foreach (var objDic in dic)
            {
                Printer.WriteTitle(objDic.Key.ToString());
                foreach (var val in objDic.Value)
                {
                    switch (objDic.Key)
                    {
                        case LlaveDiccionario.Escuela:
                            Console.WriteLine("Escuela: " + val);
                            break;
                        case LlaveDiccionario.Curso:
                            var curtmp = val as Curso;
                            if (curtmp != null)//Si la conversion no es posible devuelve null
                            {
                                int count = curtmp.Alumnos.Count;
                                Console.WriteLine("Curso: " + val.Nombre + " Cantidad de alumnos: " + count);
                            }
                            break;
                        case LlaveDiccionario.Alumno:
                            Console.WriteLine("Alumno: " + val.Nombre);
                            break;
                        //case LlaveDiccionario.Asignatura:
                        //    break;
                        case LlaveDiccionario.Evaluacion:
                            if (imprEval)
                                Console.WriteLine(val);
                            break;
                        default:
                            Console.WriteLine(val);
                            break;
                    }
                }
            }
        }
        public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionariosObjetos()
        {
            var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();

            diccionario.Add(LlaveDiccionario.Escuela, new[] { Escuela });
            diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

            var lstTmpEv = new List<Evaluacion>();
            var lstTmpAs = new List<Asignatura>();
            var lstTmpAl = new List<Alumno>();

            foreach (var cur in Escuela.Cursos)
            {
                lstTmpAs.AddRange(cur.Asignaturas);
                lstTmpAl.AddRange(cur.Alumnos);
                foreach (var alum in cur.Alumnos)
                {
                    lstTmpEv.AddRange(alum.Evaluaciones);
                }
            }
            diccionario.Add(LlaveDiccionario.Evaluacion, lstTmpEv.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlaveDiccionario.Asignatura, lstTmpAs.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlaveDiccionario.Alumno, lstTmpAl.Cast<ObjetoEscuelaBase>());
            return diccionario;
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {
            return GetObjetosEscuela(out int dummy, out dummy, out dummy, out dummy);
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out int dummy, out dummy, out dummy);
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out int dummy, out dummy);
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out conteoAsignaturas, out int dummy);
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            out int conteoAlumnos,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {
            conteoAlumnos = conteoAsignaturas = conteoEvaluaciones = 0;
            var listaObj = new List<ObjetoEscuelaBase>();


            listaObj.Add(Escuela);
            if (traeCursos)
                listaObj.AddRange(Escuela.Cursos);

            conteoCursos = Escuela.Cursos.Count;
            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;
                if (traeAsignaturas)
                    listaObj.AddRange(curso.Asignaturas);
                if (traeAlumnos)
                    listaObj.AddRange(curso.Alumnos);
                if (traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
            }

            return listaObj.AsReadOnly();
        }

        #region Metodos de carga
        private void CargarEvaluaciones()
        {
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        var rnd = new Random(System.Environment.TickCount);
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluacion
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = MathF.Round((float)(5 * rnd.NextDouble()), 2),
                                Alumno = alumno

                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }
        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                List<Asignatura> listaAsignaturas = new List<Asignatura>() {
                new Asignatura{ Nombre="Matematica"},
                new Asignatura{ Nombre="Educacion Fisica"},
                new Asignatura{ Nombre="Castellano"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();

        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>()
            {
            new Curso(){ Nombre = "101",Jornada=TiposJornada.Mañana},
            new Curso(){ Nombre = "201",Jornada=TiposJornada.Mañana},
            new Curso(){ Nombre = "301",Jornada=TiposJornada.Mañana},
            new Curso(){ Nombre = "401",Jornada=TiposJornada.Tarde},
            new Curso(){ Nombre = "501",Jornada=TiposJornada.Tarde}
            };
            Random rnd = new Random();


            foreach (var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }
        #endregion
    }

}
