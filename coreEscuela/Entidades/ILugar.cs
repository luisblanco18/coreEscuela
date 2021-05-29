using System;
using System.Collections.Generic;
using System.Text;

namespace coreEscuela.Entidades
{
    public interface ILugar
    {
        public string Direccion { get; set; }
        void LimpiarLugar();
    }
}
