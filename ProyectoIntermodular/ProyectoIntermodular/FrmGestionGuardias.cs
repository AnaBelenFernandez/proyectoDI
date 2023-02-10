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

            //Actualizar();
        }

        private void Actualizar()
        {
            lvwGuardias.Items.Clear();

            foreach(Guardia guardia in _negocio.ObtenerGuardias())
            {
                string enumText=string.Empty;
                switch (guardia.Estado)
                {
                    case EstadoEnum.R:
                        enumText = "Realizado";
                        break;
                    case EstadoEnum.C:
                        enumText = "Confirmada y pendiente";
                        break;
                    case EstadoEnum.A:
                        enumText = "Anulada";
                        break;
                }

                ListViewItem item = new ListViewItem(new string[] {_negocio.ObtenerNombreProfesor(guardia.IdProfesorFalta), _negocio.ObtenerNombreProfesor(guardia.IdProfesorGuardia), enumText, guardia.Fecha.ToString(),});
                item.Tag = guardia.Id;
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
            if (lvwGuardias.SelectedItems.Count != 0)
            {
                Guardia guardia = _negocio.ObtenerGuardia(Convert.ToInt32(lvwGuardias.SelectedItems[0].Tag));
                FrmPropiedadesGuardia propGuardia = new FrmPropiedadesGuardia(guardia);
                propGuardia.ShowDialog();
                if (propGuardia.DialogResult == DialogResult.OK)
                {
                    _negocio.ActualizarGuardia(guardia);
                    Actualizar();
                }
            }
        }

        private void TsmiBorrar_Click(object sender, EventArgs e)
        {
            if (lvwGuardias.SelectedItems.Count != 0)
            {
                //pedir confirmación al usuario antes de borrar
                DialogResult resultado = MessageBox.Show("¿Deseas Eliminar esta Guardia?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.OK)
                {
                    _negocio.BorrarGuardia(Convert.ToInt32(lvwGuardias.SelectedItems[0].Tag));
                    Actualizar();
                }
            }
        }

        private void cmsContextual_Opening(object sender, CancelEventArgs e)
        {
            if (lvwGuardias.SelectedItems.Count == 0)
            {
                tsmiCrearContextual.Enabled = true;
                tsmiVer.Enabled = false;
                TsmiBorrar.Enabled = false;
            }
            else
            {
                TsmiBorrar.Enabled = true;
                tsmiVer.Enabled = true;
                tsmiCrearContextual.Enabled = false;
            }
        }
    }
}
