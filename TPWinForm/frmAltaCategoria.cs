
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
    public partial class frmAltaCategoria : Form
    {
        private Categoria categoria  = null;
        public frmAltaCategoria()
        {
            InitializeComponent();
        }
        public frmAltaCategoria(Categoria categoria)
        {
            InitializeComponent();
            this.categoria = categoria;
            Text = "Modificar Categoría";
        }

        private void frmAltaCategoria_Load(object sender, EventArgs e)
        {
            if (categoria != null)
            {
                txtDescripcion.Text = categoria.Descripcion;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("Ingrese una descripcion.");
                    return;
                }

                if (categoria == null)
                    categoria = new Categoria();

                categoria.Descripcion = txtDescripcion.Text.Trim();

                if (categoria.Id != 0)
                {
                    categoriaNegocio.modificar(categoria);
                    MessageBox.Show("Categoría modificada exitosamente");
                }
                else
                {
                    categoriaNegocio.agregar(categoria);
                    MessageBox.Show("Categoría agregada exitosamente");
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

