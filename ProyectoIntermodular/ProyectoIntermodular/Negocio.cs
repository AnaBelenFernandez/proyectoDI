using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntermodular
{
    public class Negocio
    {
        public Negocio() 
        { 
        }

        // comprueba si el usuario y contraseña son correctas ademas de si su perfil es el indicado para acceder a la aplicacion
        public bool ComprobarUsuario()
        {
            throw new NotImplementedException();
        }

        //obtiene una lista de guardias ordenadas por fecha
        public List<Guardia> ObtenerGuardias()
        {
            throw new NotImplementedException();
        }

        public Guardia ObtenerGuardia(int guardiaId)
        {
            throw new NotImplementedException();
        }

        public void CrearGuardia(Guardia nuevaGuardia)
        {
            throw new NotImplementedException();
        }

        public void ActualizarGuardia(Guardia guardia)
        {
            throw new NotImplementedException();
        }

        public void BorrarGuardia(int guardiaId)
        {
            throw new NotImplementedException();
        }

        // obtiene nombre y apellidos del profesor en base a la Id de un profesor
        public string ObtenerNombreProfesor(int idProfesor)
        {
            throw new NotImplementedException();
        }

        public int ObtenerIdProfesor(string nombreProfesor)
        {
            throw new NotImplementedException();
        }
    }
}
