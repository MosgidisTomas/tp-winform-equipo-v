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
        private const int LargoMaximoUrl = 1000;
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

        private bool validarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Ingrese el codigo del articulo.");
                return false;
            }

            if (txtCodigo.Text.Trim().Length > 50)
            {
                MessageBox.Show("El codigo no puede superar los 50 caracteres.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del articulo.");
                return false;
            }

            if (txtNombre.Text.Trim().Length > 50)
            {
                MessageBox.Show("El nombre no puede superar los 50 caracteres.");
                return false;
            }

            if (txtDescripcion.Text.Trim().Length > 150)
            {
                MessageBox.Show("La descripcion no puede superar los 150 caracteres.");
                return false;
            }

            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Ingrese un precio valido.");
                return false;
            }

            if (precio < 0)
            {
                MessageBox.Show("El precio no puede ser negativo.");
                return false;
            }

            if (cboMarca.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una marca.");
                return false;
            }

            if (cboCategoria.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una categoria.");
                return false;
            }

            foreach (string url in imagenes)
            {
                if (!string.IsNullOrWhiteSpace(url) && url.Trim().Length > LargoMaximoUrl)
                {
                    MessageBox.Show("Las URL de imagen no pueden superar los " + LargoMaximoUrl + " caracteres.");
                    return false;
                }
            }

            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                int idActual = (articulo == null) ? 0 : articulo.Id;

                if (negocio.existeCodigo(txtCodigo.Text.Trim(), idActual))
                {
                    MessageBox.Show("Ya existe otro articulo con ese codigo.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

            return true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!validarCampos())
                return;

            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
                if(articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text.Trim();
                articulo.Nombre = txtNombre.Text.Trim();
                articulo.Descripcion = txtDescripcion.Text.Trim();
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

                // Quitar imagenes solo tiene sentido al modificar un articulo existente
                btnQuitarImagen.Visible = (articulo != null);

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
                lblUrlImagen.Text = "URL Imagen:";
            }
            else
            {
                if (indiceImagenActual < 0) indiceImagenActual = 0;
                if (indiceImagenActual >= imagenes.Count) indiceImagenActual = imagenes.Count - 1;

                txtUrlImagen.Text = imagenes[indiceImagenActual];
                cargarImagen(imagenes[indiceImagenActual]);
                lblUrlImagen.Text = "URL Imagen (" + (indiceImagenActual + 1) + " de " + imagenes.Count + "):";
            }

            btnAnterior.Enabled = indiceImagenActual > 0;
            btnSiguiente.Enabled = indiceImagenActual < imagenes.Count - 1;
            btnQuitarImagen.Enabled = imagenes.Count > 0;
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
            if (imagenes.Count > 0 && string.IsNullOrWhiteSpace(imagenes[imagenes.Count - 1]))
            {
                MessageBox.Show("Complete la URL de la imagen actual antes de agregar otra.");
                indiceImagenActual = imagenes.Count - 1;
                MostrarImagenActual();
                txtUrlImagen.Focus();
                return;
            }

            imagenes.Add("");
            indiceImagenActual = imagenes.Count - 1;
            MostrarImagenActual();
            txtUrlImagen.Focus();
        }

        private void btnQuitarImagen_Click(object sender, EventArgs e)
        {
            if (imagenes.Count == 0)
                return;

            imagenes.RemoveAt(indiceImagenActual);

            if (indiceImagenActual > imagenes.Count - 1)
                indiceImagenActual = imagenes.Count - 1;

            MostrarImagenActual();
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
