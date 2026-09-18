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
    public partial class frmModificarImagen : Form
    {
        private const string ImagenPorDefecto = "https://placehold.co/300x300.png?text=Sin+Imagen";
        private Imagen imagen = null;

        public frmModificarImagen(Imagen imagen)
        {
            InitializeComponent();
            this.imagen = imagen;
            Text = "Modificar Imagen";
        }

        private void frmModificarImagen_Load(object sender, EventArgs e)
        {
            lblArticulo.Text = "Articulo: " + imagen.IdArticulo;
            txtUrlImagen.Text = imagen.ImagenUrl;
            cargarImagen(imagen.ImagenUrl);
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagen.Load(imagen);
            }
            catch (Exception)
            {
                try
                {
                    pbxImagen.Load(ImagenPorDefecto);
                }
                catch (Exception)
                {
                    pbxImagen.Image = null;
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ImagenNegocio imagenNegocio = new ImagenNegocio();

            try
            {
                if (string.IsNullOrWhiteSpace(txtUrlImagen.Text))
                {
                    MessageBox.Show("Ingrese una URL de imagen.");
                    return;
                }

                imagen.ImagenUrl = txtUrlImagen.Text.Trim();
                imagenNegocio.modificar(imagen);

                MessageBox.Show("Imagen modificada exitosamente");
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
