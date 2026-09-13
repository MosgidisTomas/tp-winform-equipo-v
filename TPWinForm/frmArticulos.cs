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
    public partial class frmArticulos : Form
    {
        private List<Articulo> listaArticulos;
        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.Listar();
            dgvArticulos.DataSource = listaArticulos;
            pbxArticulo.Load(listaArticulos[0].Imagenes[0].ImagenUrl);

        }

        private void dgvArticulos_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private int indiceImagen = 0;
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                indiceImagen = 0; // Reiniciamos al cambiar de artículo

                if (seleccionado.Imagenes != null && seleccionado.Imagenes.Count > 0)
                {
                    cargarImagen(seleccionado.Imagenes[indiceImagen].ImagenUrl);
                }
                else
                {
                    cargarImagen("https://upload.wikimedia.org/wikipedia/commons/1/14/No_Image_Available.jpg");
                }

                // --- AGREGA ESTA LÍNEA AQUÍ ---
                actualizarContadorImagenes(seleccionado);
            }
        }
        

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Image = null;
            }
        }

        private void actualizarContadorImagenes(Articulo articulo)
        {
            if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
            {
                // Muestra por ejemplo "1/2", sumando +1 porque indiceImagen arranca en 0
                lblCantidadImagenes.Text = (indiceImagen + 1) + "/" + articulo.Imagenes.Count;
            }
            else
            {
                lblCantidadImagenes.Text = "0/0";
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                if (seleccionado.Imagenes != null && indiceImagen > 0)
                {
                    indiceImagen--;
                    cargarImagen(seleccionado.Imagenes[indiceImagen].ImagenUrl);

                    // <-- Y AQUÍ TAMBIÉN:
                    actualizarContadorImagenes(seleccionado);
                }
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                if (seleccionado.Imagenes != null && indiceImagen < seleccionado.Imagenes.Count - 1)
                {
                    indiceImagen++;
                    cargarImagen(seleccionado.Imagenes[indiceImagen].ImagenUrl);

                    // <-- ESTA LÍNEA ES LA QUE ACTUALIZA EL TEXTO A 2/2:
                    actualizarContadorImagenes(seleccionado);
                }
            }

        }



    }
}