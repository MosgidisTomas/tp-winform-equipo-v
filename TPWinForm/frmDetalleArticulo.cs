using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm
{
    public partial class frmDetalleArticulo : Form
    {
        private const string ImagenPorDefecto = "https://placehold.co/300x300.png?text=Sin+Imagen";
        private Articulo articulo;
        private int indiceImagen = 0;
        public frmDetalleArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void frmDetalleArticulo_Load(object sender, EventArgs e)
        {
            cargarDetalle();
            mostrarImagen();
        }
        private void cargarDetalle()
        {
            var detalle = new[]
            {
                new { Campo = "Código",      Valor = articulo.Codigo ?? "-" },
                new { Campo = "Nombre",      Valor = articulo.Nombre ?? "-" },
                new { Campo = "Descripción", Valor = articulo.Descripcion ?? "-" },
                new { Campo = "Marca",       Valor = articulo.Marca?.Descripcion ?? "-" },
                new { Campo = "Categoría",   Valor = articulo.Categoria?.Descripcion ?? "-" },
                new { Campo = "Precio",      Valor = articulo.Precio.ToString("C2") }
            }.ToList();

            dgvDetalle.DataSource = detalle;

            dgvDetalle.ReadOnly = true;
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.AllowUserToResizeRows = false;
            dgvDetalle.RowHeadersVisible = false;
            dgvDetalle.ColumnHeadersVisible = false;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.MultiSelect = false;

            dgvDetalle.Columns["Campo"].Width = 110;
            dgvDetalle.Columns["Campo"].DefaultCellStyle.Font =
                new Font(dgvDetalle.Font, FontStyle.Bold);
            dgvDetalle.Columns["Valor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetalle.Columns["Valor"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDetalle.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        private void mostrarImagen()
        {
            if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
            {
                cargarImagen(articulo.Imagenes[indiceImagen].ImagenUrl);
                lblContador.Text = (indiceImagen + 1) + "/" + articulo.Imagenes.Count;
            }
            else
            {
                cargarImagen(ImagenPorDefecto);
                lblContador.Text = "0/0";
            }
        }
        private void cargarImagen(string url)
        {
            try
            {
                pbxArticulo.Load(url);
            }
            catch (Exception)
            {
                try { pbxArticulo.Load(ImagenPorDefecto); }
                catch (Exception) { pbxArticulo.Image = null; }
            }
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (indiceImagen > 0)
            {
                indiceImagen--;
                mostrarImagen();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (articulo.Imagenes != null && indiceImagen < articulo.Imagenes.Count - 1)
            {
                indiceImagen++;
                mostrarImagen();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
