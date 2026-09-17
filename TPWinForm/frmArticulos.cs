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
        }


        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = negocio.Listar();
                dgvArticulos.DataSource = listaArticulos;
                pbxArticulo.Load(listaArticulos[0].Imagenes[0].ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                string campo = cbCampo.SelectedItem.ToString();
                string criterio = cbCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }


        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro.Length >= 2)
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Codigo.ToUpper().Contains(filtro.ToUpper()) || x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
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
            string opcion = cbCampo.SelectedItem.ToString();
            if(opcion == "Codigo")
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Mayor a");
                cbCriterio.Items.Add("Menor a");
                cbCriterio.Items.Add("Igual a");
            }
            else
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Comienza con");
                cbCriterio.Items.Add("Termina Con");
                cbCriterio.Items.Add("Contiene");
            }
        }
    }
}