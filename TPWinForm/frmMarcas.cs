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
    public partial class frmMarcas : Form
    {
        public frmMarcas()
        {
            InitializeComponent();
        }

        private void frmMarcas_Load(object sender, EventArgs e)
        {
            cargarMarca();
        }

        private void cargarMarca()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                dgvMarcas.DataSource = negocio.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaMarca altaMarca = new frmAltaMarca();
            altaMarca.ShowDialog();
            cargarMarca();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una marca.");
                return;
            }

            Marca seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
            frmAltaMarca modificarMarca = new frmAltaMarca(seleccionada);
            modificarMarca.ShowDialog();
            cargarMarca();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una marca.");
                return;
            }

            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                Marca seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar esta marca?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(seleccionada.Id);
                    cargarMarca();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvMarcas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
