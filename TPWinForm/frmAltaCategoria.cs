
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

                if (txtDescripcion.Text.Trim().Length > 50)
                {
                    MessageBox.Show("La descripcion no puede superar los 50 caracteres.");
                    return;
                }

                string descripcion = txtDescripcion.Text.Trim();

                if (categoria == null)
                    categoria = new Categoria();

                if (categoria.Id != 0)
                {
                    if (categoriaNegocio.existe(descripcion, categoria.Id))
                    {
                        MessageBox.Show("Ya existe una categoria con esa descripcion.");
                        return;
                    }
                    categoria.Descripcion = descripcion;
                    categoriaNegocio.modificar(categoria);
                    MessageBox.Show("Categoría modificada exitosamente");
                }
                else
                {
                    categoria.Descripcion = descripcion;
                    if (!categoriaNegocio.agregar(categoria))
                    {
                        MessageBox.Show("Ya existe una categoria con esa descripcion.");
                        return;
                    }
                    MessageBox.Show("Categoría agregada exitosamente");
                }

                Close();
            }
            catch (Exception ex)
            {
                Mensajes.Error("No se pudo completar la operacion con la categoria.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

