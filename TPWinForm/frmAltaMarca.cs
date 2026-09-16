using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TPWinForm
{
    public partial class frmAltaMarca : Form
    {
        private Marca marca = null;

        public frmAltaMarca()
        {
            InitializeComponent();
        }

        public frmAltaMarca(Marca marca)
        {
            InitializeComponent();
            this.marca = marca;
            Text = "Modificar Marca";
        }

        private void frmAltaMarca_Load(object sender, EventArgs e)
        {
            if (marca != null)
            {
                txtDescripcion.Text = marca.Descripcion;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Ingrese una descripcion.");
                    return;
                }

                if (marca == null)
                    marca = new Marca();

                marca.Descripcion = txtDescripcion.Text.Trim();

                if (marca.Id != 0)
                {
                    marcaNegocio.modificar(marca);
                    MessageBox.Show("Marca modificada exitosamente");
                }
                else
                {
                    marcaNegocio.agregar(marca);
                    MessageBox.Show("Marca agregada exitosamente");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
