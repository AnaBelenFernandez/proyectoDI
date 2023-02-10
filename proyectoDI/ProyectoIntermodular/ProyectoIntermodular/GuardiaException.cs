using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntermodular
{
    public class GuardiaException : Exception
    {
        public GuardiaException(string message) : base(message)
        {
        }
    }
}

