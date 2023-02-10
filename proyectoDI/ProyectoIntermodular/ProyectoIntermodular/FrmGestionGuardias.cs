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
    public partial class FrmGestionGuardias : Form
    {
        private Negocio _negocio;
        public FrmGestionGuardias()
        {
            InitializeComponent();

            _negocio=new Negocio();

            Actualizar();
        }

        private void Actualizar()
        {
            lvwGuardias.Items.Clear();

            foreach(Guardia guardia in _negocio.ObtenerGuardias())
            {
                ListViewItem item = new ListViewItem(new string[] { });
                //item.tag=
                lvwGuardias.Items.Add(item);
            }
        }

        private void tsmiCrear_Click(object sender, EventArgs e)
        {
            Guardia guardia = new Guardia();
            FrmPropiedadesGuardia frmProp=new FrmPropiedadesGuardia();
            frmProp.ShowDialog();

            if (frmProp.DialogResult == DialogResult.OK)
            {
                _negocio.CrearGuardia(guardia);
                Actualizar();
            }
        }

        private void tsmiVer_Click(object sender, EventArgs e)
        {

        }

        private void TsmiBorrar_Click(object sender, EventArgs e)
        {

        }
    }
}
