using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIntermodular
{
    public partial class FrmLogIn : Form
    {
        Negocio negocio;
        public FrmLogIn()
        {
            InitializeComponent();
            negocio= new Negocio();
            grpError.Visible = false;
        }

        private async void btnLogIn_Click(object sender, EventArgs e)
        {
            Profesor profesor = await negocio.Login(txtUsuario.Text,Encriptar(txtContraseña.Text));
            if (profesor!=null )
            {
                FrmContenedor contenedor = new FrmContenedor();
                this.Hide();
                contenedor.ShowDialog();
                if (contenedor.DialogResult == DialogResult.OK)
                {
                    this.Show();
                }
                else
                {
                    this.Close();
                }

            }
            else
            {
                grpError.Visible = true;
                txtContraseña.Text = string.Empty;
                txtUsuario.Text = string.Empty;

                //pruebas
                //FrmContenedor contenedor = new FrmContenedor();
                //this.Hide();
                //contenedor.ShowDialog();
                //if(contenedor.DialogResult== DialogResult.OK)
                //{
                //    this.Show();
                //}
                //else
                //{
                //    this.Close();
                //}
                // pruebas
                
                
            }
        }

        private string Encriptar(String texto)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] array= Encoding.UTF8.GetBytes(texto );
            array=md5.ComputeHash(array); 
            StringBuilder sb=new StringBuilder();
            foreach(byte b in array)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
    }
}
