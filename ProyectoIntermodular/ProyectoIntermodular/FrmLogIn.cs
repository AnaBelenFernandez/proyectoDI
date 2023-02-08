using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIntermodular
{
    public partial class FrmLogIn : Form
    {
        public FrmLogIn()
        {
            InitializeComponent();
            grpError.Visible = false;
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (ValidarUsuario())
            {
                FrmContenedor contenedor = new FrmContenedor();
                contenedor.ShowDialog();
                
            }
            else
            {
                grpError.Visible = true;
            }
        }

        private string[] Encriptar()
        {
            throw new NotImplementedException();
        }

        private bool ValidarUsuario()
        {
            return true;
        }
    }
}
