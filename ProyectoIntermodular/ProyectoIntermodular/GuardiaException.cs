using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIntermodular
{
    public class GuardiaException : Exception
    {
        public GuardiaException(string message) : base(message)
        {
            MessageBox.Show(message,"Aviso",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
        }
    }
}
