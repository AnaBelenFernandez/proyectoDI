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
    public partial class FrmPropiedadesGuardia : Form
    {
        public FrmPropiedadesGuardia()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        public FrmPropiedadesGuardia(Guardia guardia)
        {
            InitializeComponent();
        }

        private void btnProfesorFalta_Click(object sender, EventArgs e)
        {

        }

        private void btnProfesorGuardia_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {


                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool validar()
        {
            return true;
        }
    }
}
