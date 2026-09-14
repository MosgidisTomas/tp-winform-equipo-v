using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TPWinForm
{
    public partial class frmAltaArticulo : Form
    {

        private const string ImagenPorDefecto = "https://placehold.co/300x300.png?text=Sin+Imagen";
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        private void lblCodigo_Click(object sender, EventArgs e)
        {

        }

        private void lblNombre_Click(object sender, EventArgs e)
        {

        }

        private void lblDescripcion_Click(object sender, EventArgs e)
        {

        }

        private void lblPrecio_Click(object sender, EventArgs e)
        {

        }

        private void lblMarca_Click(object sender, EventArgs e)
        {

        }

        private void lblCategoria_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            Articulo nuevoArticulo = new Articulo();

            try
            {
                nuevoArticulo.Codigo = txtCodigo.Text;
                nuevoArticulo.Nombre = txtNombre.Text;
                nuevoArticulo.Descripcion = txtDescripcion.Text;
                nuevoArticulo.Precio = decimal.Parse(txtPrecio.Text);
                nuevoArticulo.Marca = (Marca)cboMarca.SelectedItem;
                nuevoArticulo.Categoria = (Categoria)cboCategoria.SelectedItem;

                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                articuloNegocio.agregar(nuevoArticulo);

                MessageBox.Show("Articulo agregado exitosamente");
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

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                cboCategoria.DataSource = categoriaNegocio.Listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboMarca.DataSource = marcaNegocio.Listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                try
                {
                    pbxArticulo.Load(ImagenPorDefecto);
                }
                catch (Exception)
                {
                    pbxArticulo.Image = null;
                }
            }
        }
    }
}
