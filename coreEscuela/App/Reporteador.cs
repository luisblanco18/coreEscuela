using coreEscuela.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace coreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObjEsc)
        {
            if (dicObjEsc == null)
                throw new ArgumentException(nameof(dicObjEsc));
            _diccionario = dicObjEsc;
        }
        public IEnumerable<Evaluacion> GetListaEvaluaciones()
        {
            if (_diccionario.TryGetValue(LlaveDiccionario.Evaluacion, out IEnumerable<ObjetoEscuelaBase> lista))
            {
                return lista.Cast<Evaluacion>();
            }
            else
            {
                return new List<Evaluacion>();
            }

        }
        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }
        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluacion ev in listaEvaluaciones
                    select ev.Asignatura.Nombre).Distinct();

        }
        public Dictionary<string, IEnumerable<Evaluacion>> GetDicEvaluaXAsig()
        {
            var dictRta = new Dictionary<string, IEnumerable<Evaluacion>>();

            var listAsig = GetListaAsignaturas(out var listEval);
            foreach (var asig in listAsig)
            {
                var evalsAsig = from eval in listEval
                                where eval.Asignatura.Nombre == asig
                                select eval;
                dictRta.Add(asig, evalsAsig);
            }


            return dictRta;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetPromeAlumnPorAsignatura()
        {
            var rta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var dicEvalXAsig = GetDicEvaluaXAsig();

            foreach (var asigConEval in dicEvalXAsig)
            {
                var promsAlum = from eval in asigConEval.Value
                                group eval by new
                                {
                                    eval.Alumno.UniqueId,
                                    eval.Alumno.Nombre
                                }
                            into grupoEvalsAlumno
                                select new AlumnoPromedio
                                {
                                    alumnoId = grupoEvalsAlumno.Key.UniqueId,
                                    alumnoNombre = grupoEvalsAlumno.Key.Nombre,
                                    promedio = grupoEvalsAlumno.Average(evaluacion => evaluacion.Nota)
                                };

                rta.Add(asigConEval.Key, promsAlum);
            }

            return rta;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetListaTopPromedio(int x)
        {
            var resp = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var dicPromAlumPorAsignatura = GetPromeAlumnPorAsignatura();

            foreach (var item in dicPromAlumPorAsignatura)
            {
                var dummy = (from ap in item.Value
                             orderby ap.promedio descending
                             select ap).Take(x);

                resp.Add(item.Key, dummy);
            }
            return resp;
        }
    }
}
