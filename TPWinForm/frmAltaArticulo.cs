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
        private const int MaximoImagenes = 3;
        private Articulo articulo = null;
        private List<string> imagenes = new List<string>();
        private int indiceImagenActual = 0;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
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
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
                if(articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                
                
                if(articulo.Id != 0)
                {
                    articuloNegocio.modificar(articulo);

                    ImagenNegocio imagenNegocioMod = new ImagenNegocio();
                    imagenNegocioMod.eliminarPorArticulo(articulo.Id);
                    foreach (string url in imagenes)
                    {
                        if (string.IsNullOrWhiteSpace(url)) continue;

                        Imagen imagen = new Imagen();
                        imagen.IdArticulo = articulo.Id;
                        imagen.ImagenUrl = url;
                        imagenNegocioMod.agregar(imagen);
                    }

                    MessageBox.Show("Articulo modificado exitosamente");
                }else
                {
                    articulo.Id = articuloNegocio.agregar(articulo);

                    ImagenNegocio imagenNegocio = new ImagenNegocio();
                    foreach (string url in imagenes)
                    {
                        if (string.IsNullOrWhiteSpace(url)) continue;

                        Imagen imagen = new Imagen();
                        imagen.IdArticulo = articulo.Id;
                        imagen.ImagenUrl = url;
                        imagenNegocio.agregar(imagen);
                    }

                    MessageBox.Show("Articulo agregado exitosamente");
                }
                
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

                if(articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;

                    if (articulo.Imagenes != null)
                    {
                        foreach (Imagen imagen in articulo.Imagenes)
                            imagenes.Add(imagen.ImagenUrl);
                    }
                }

                indiceImagenActual = 0;
                MostrarImagenActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MostrarImagenActual()
        {
            if (imagenes.Count == 0)
            {
                txtUrlImagen.Text = "";
                cargarImagen(ImagenPorDefecto);
            }
            else
            {
                if (indiceImagenActual < 0) indiceImagenActual = 0;
                if (indiceImagenActual >= imagenes.Count) indiceImagenActual = imagenes.Count - 1;

                txtUrlImagen.Text = imagenes[indiceImagenActual];
                cargarImagen(imagenes[indiceImagenActual]);
            }

            btnAnterior.Enabled = indiceImagenActual > 0;
            btnSiguiente.Enabled = indiceImagenActual < imagenes.Count - 1;
            btnAgregarImagen.Text = "Agregar (" + imagenes.Count + "/" + MaximoImagenes + ")";
            btnAgregarImagen.Enabled = imagenes.Count < MaximoImagenes;
        }
        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            if (imagenes.Count > 0)
                imagenes[indiceImagenActual] = txtUrlImagen.Text.Trim();

            cargarImagen(txtUrlImagen.Text);
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (imagenes.Count >= MaximoImagenes)
            {
                MessageBox.Show("Ya alcanzo el maximo de " + MaximoImagenes + " imagenes.");
                return;
            }

            imagenes.Add("");
            indiceImagenActual = imagenes.Count - 1;
            MostrarImagenActual();
            txtUrlImagen.Focus();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (indiceImagenActual > 0)
            {
                indiceImagenActual--;
                MostrarImagenActual();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (indiceImagenActual < imagenes.Count - 1)
            {
                indiceImagenActual++;
                MostrarImagenActual();
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

        private void pbxArticulo_Click(object sender, EventArgs e)
        {

        }
    }
}
