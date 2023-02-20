using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        private async void Actualizar()
        {
            lvwGuardias.Items.Clear();
            List<Guardia> guardias = await _negocio.ObtenerGuardias();

            foreach (Guardia guardia in guardias)
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
                Profesor profeFalta = await _negocio.ObtenerProfesorID(guardia.IdProfesorFalta);
                Profesor profeGuardia = await _negocio.ObtenerProfesorID(guardia.IdProfesorGuardia);
                ListViewItem item = new ListViewItem(new string[] {(profeFalta.nombre+", "+profeFalta.ape1+" "+profeFalta.ape2), (profeGuardia.nombre + ", " + profeGuardia.ape1 + " " + profeGuardia.ape2), enumText, guardia.Fecha.ToString(),});
                item.Tag = guardia.Id;
                lvwGuardias.Items.Add(item);
            }
        }

        private async void tsmiCrear_Click(object sender, EventArgs e)
        {
            Guardia guardia = new Guardia();
            FrmPropiedadesGuardia frmProp=new FrmPropiedadesGuardia();
            frmProp.ShowDialog();

            if (frmProp.DialogResult == DialogResult.OK)
            {
                await _negocio.CrearGuardia(guardia);
                Actualizar();
            }
        }

        private async void tsmiVer_Click(object sender, EventArgs e)
        {
            if (lvwGuardias.SelectedItems.Count != 0)
            {
                Guardia guardia = await _negocio.ObtenerGuardiaID(Convert.ToInt32(lvwGuardias.SelectedItems[0].Tag));
                FrmPropiedadesGuardia propGuardia = new FrmPropiedadesGuardia(guardia);
                propGuardia.ShowDialog();
                if (propGuardia.DialogResult == DialogResult.OK)
                {
                     await _negocio.ActualizarGuardia(guardia);
                    Actualizar();
                }
            }
        }

        private async void TsmiBorrar_Click(object sender, EventArgs e)
        {
            if (lvwGuardias.SelectedItems.Count != 0)
            {
                //pedir confirmación al usuario antes de borrar
                DialogResult resultado = MessageBox.Show("¿Deseas Eliminar esta Guardia?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.OK)
                {
                    await _negocio.BorrarGuardia(Convert.ToInt32(lvwGuardias.SelectedItems[0].Tag));
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

        private async void tsmiRealizado_Click(object sender, EventArgs e)
        {
            await _negocio.CambiarEstadoGuardia(Convert.ToInt32(lvwGuardias.SelectedItems[0].Tag), EstadoEnum.R);
        }

        private async void tsmiConfirmada_Click(object sender, EventArgs e)
        {
            await _negocio.CambiarEstadoGuardia(Convert.ToInt32(lvwGuardias.SelectedItems[0].Tag), EstadoEnum.C);
        }

        private async void tsmiAnulado_Click(object sender, EventArgs e)
        {
            await _negocio.CambiarEstadoGuardia(Convert.ToInt32(lvwGuardias.SelectedItems[0].Tag), EstadoEnum.A);
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            //List<Guardia> guardias = await _negocio.ObtenerGuardias();

            //lvwGuardias.Items.Clear();
            //lvwGuardias.Items.AddRange(guardias.Where(i => string.IsNullOrEmpty(txtFiltrar.Text) || i.Estado.ToString().StartsWith())
            //.Select(c => new ListViewItem(c.Name)).ToArray());
        }


    }
}
