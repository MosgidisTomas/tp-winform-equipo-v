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
        private const string ImagenPorDefecto = "https://placehold.co/300x300.png?text=Sin+Imagen";
        private List<Articulo> listaArticulos;

        // Filtro avanzado en uso (null = sin filtro avanzado)
        private string campoActivo = null;
        private string criterioActivo = null;
        private string filtroActivo = null;

        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            cargar();
            cbCampo.Items.Add("Codigo");
            cbCampo.Items.Add("Nombre");
            cbCampo.Items.Add("Marca");
            cbCampo.Items.Add("Categoria");
            cbCampo.Items.Add("Precio");
        }


        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (campoActivo != null)
                    listaArticulos = negocio.filtrar(campoActivo, criterioActivo, filtroActivo);
                else
                    listaArticulos = negocio.Listar();

                aplicarFiltroRapido();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void aplicarFiltroRapido()
        {
            if (listaArticulos == null)
                return;

            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text.ToUpper();

            if (filtro.Length >= 2)
            {
                listaFiltrada = listaArticulos.FindAll(x =>
                    (x.Codigo ?? "").ToUpper().Contains(filtro) ||
                    (x.Nombre ?? "").ToUpper().Contains(filtro) ||
                    (x.Marca.Descripcion ?? "").ToUpper().Contains(filtro));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;

            if (listaFiltrada.Count == 0)
            {
                cargarImagen(ImagenPorDefecto);
                lblCantidadImagenes.Text = "0/0";
            }
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
                    cargarImagen(ImagenPorDefecto);
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo altaArticulo = new frmAltaArticulo();
            altaArticulo.ShowDialog();
            cargar(); // Recarga la lista de artículos después de agregar uno nuevo
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAltaArticulo modificarAriculo = new frmAltaArticulo(seleccionado);
            modificarAriculo.ShowDialog();
            cargar();
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            frmMarcas marcas = new frmMarcas();
            marcas.ShowDialog();
            cargar();
        }

        private void btnImagenes_Click(object sender, EventArgs e)
        {
            frmImagenes imagenes = new frmImagenes();
            imagenes.ShowDialog();
            cargar();
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar este artículo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }
            }
            catch (Exception ex)    
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblBuscar_Click(object sender, EventArgs e)
        {
            if (cbCampo.SelectedItem == null || cbCriterio.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un campo y un criterio.");
                return;
            }

            string campo = cbCampo.SelectedItem.ToString();
            string criterio = cbCriterio.SelectedItem.ToString();
            string filtro = txtFiltroAvanzado.Text.Trim();

            if (campo == "Precio")
            {
                decimal precio;
                if (!decimal.TryParse(filtro, out precio))
                {
                    MessageBox.Show("Ingrese un precio valido.");
                    return;
                }
            }

            campoActivo = campo;
            criterioActivo = criterio;
            filtroActivo = filtro;
            cargar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            campoActivo = null;
            criterioActivo = null;
            filtroActivo = null;

            cbCampo.SelectedIndex = -1;
            cbCriterio.Items.Clear();
            txtFiltroAvanzado.Text = "";
            txtFiltro.Text = "";

            cargar();
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            aplicarFiltroRapido();
        }

        private void gbFiltroAvanzado_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCriterio.Items.Clear();

            if (cbCampo.SelectedItem == null)
                return;

            string opcion = cbCampo.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                cbCriterio.Items.Add("Mayor a");
                cbCriterio.Items.Add("Menor a");
                cbCriterio.Items.Add("Igual a");
            }
            else
            {
                cbCriterio.Items.Add("Comienza con");
                cbCriterio.Items.Add("Termina con");
                cbCriterio.Items.Add("Contiene");
            }
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            frmCategoria categorias = new frmCategoria();
            categorias.ShowDialog();
            cargar();
        }
    }
}