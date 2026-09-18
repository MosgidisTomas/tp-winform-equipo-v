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
    public partial class frmImagenes : Form
    {
        private const string ImagenPorDefecto = "https://placehold.co/300x300.png?text=Sin+Imagen";

        public frmImagenes()
        {
            InitializeComponent();
        }

        private void frmImagenes_Load(object sender, EventArgs e)
        {
            cargarImagenes();
        }

        private void cargarImagenes()
        {
            ImagenNegocio negocio = new ImagenNegocio();
            try
            {
                dgvImagenes.DataSource = negocio.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvImagenes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvImagenes.CurrentRow != null)
            {
                Imagen seleccionada = (Imagen)dgvImagenes.CurrentRow.DataBoundItem;
                cargarImagen(seleccionada.ImagenUrl);
            }
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvImagenes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una imagen.");
                return;
            }

            Imagen seleccionada = (Imagen)dgvImagenes.CurrentRow.DataBoundItem;
            frmModificarImagen modificarImagen = new frmModificarImagen(seleccionada);
            modificarImagen.ShowDialog();
            cargarImagenes();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvImagenes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una imagen.");
                return;
            }

            ImagenNegocio negocio = new ImagenNegocio();
            try
            {
                Imagen seleccionada = (Imagen)dgvImagenes.CurrentRow.DataBoundItem;
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar esta imagen?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(seleccionada.Id);
                    cargarImagenes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
