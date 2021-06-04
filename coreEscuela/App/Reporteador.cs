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
        public IEnumerable<Escuela> GetListaEvaluaciones()
        {
            IEnumerable<Escuela> rta;
            if (_diccionario.TryGetValue(LlaveDiccionario.Escuela, out IEnumerable<ObjetoEscuelaBase> lista))
            {
                rta = lista.Cast<Escuela>();
            }
            else
            {
                rta = null;
            }
            return rta;
        }
    }
}
