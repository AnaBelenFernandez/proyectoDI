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
            Profesor profesor = await negocio.Login(txtUsuario.Text,txtContraseña.Text);
            if (profesor!=null )
            {
                FrmContenedor contenedor = new FrmContenedor();
                contenedor.ShowDialog();
                
            }
            else
            {
                grpError.Visible = true;
            }
        }

        private string Encriptar(String texto)
        {
            string key = "key";

            byte[] keyArray;
            byte[] Arreglo_a_Cifrar =
            UTF8Encoding.UTF8.GetBytes(texto);
            MD5CryptoServiceProvider hashmd5 =
            new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(
            UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes =
            new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform =
            tdes.CreateEncryptor();
            byte[] ArrayResultado =
            cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
            0, Arreglo_a_Cifrar.Length);
            tdes.Clear();
            return Convert.ToBase64String(ArrayResultado,
            0, ArrayResultado.Length);
        }
    }
}
