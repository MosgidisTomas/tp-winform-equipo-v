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
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            cargarCategoria();
        }

        private void cargarCategoria()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                dgvCategorias.DataSource = negocio.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaCategoria altaCategoria = new frmAltaCategoria();
            altaCategoria.ShowDialog();
            cargarCategoria();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }

            Categoria seleccionada = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
            frmAltaCategoria modificarCategoria = new frmAltaCategoria(seleccionada);
            modificarCategoria.ShowDialog();
            cargarCategoria();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }

            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                Categoria seleccionada = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar esta categoría?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(seleccionada.Id);
                    cargarCategoria();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }




}
