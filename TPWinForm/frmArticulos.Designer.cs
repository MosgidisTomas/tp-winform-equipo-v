namespace TPWinForm
{
    partial class frmArticulos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvArticulos = new System.Windows.Forms.DataGridView();
            this.pbxArticulo = new System.Windows.Forms.PictureBox();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.lblCantidadImagenes = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminarFisico = new System.Windows.Forms.Button();
            this.btnMarcas = new System.Windows.Forms.Button();
            this.lblFiltroRapido = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCriterio = new System.Windows.Forms.ComboBox();
            this.lblCampo = new System.Windows.Forms.Label();
            this.lblFiltroAvanzado = new System.Windows.Forms.Label();
            this.lblCriterio = new System.Windows.Forms.Label();
            this.txtFiltroAvanzado = new System.Windows.Forms.TextBox();
            this.cbCampo = new System.Windows.Forms.ComboBox();
            this.btnCategorias = new System.Windows.Forms.Button();
            this.btnImagenes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxArticulo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvArticulos
            // 
            this.dgvArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArticulos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvArticulos.Location = new System.Drawing.Point(12, 45);
            this.dgvArticulos.MultiSelect = false;
            this.dgvArticulos.Name = "dgvArticulos";
            this.dgvArticulos.RowHeadersWidth = 51;
            this.dgvArticulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArticulos.Size = new System.Drawing.Size(754, 314);
            this.dgvArticulos.TabIndex = 0;
            this.dgvArticulos.SelectionChanged += new System.EventHandler(this.dgvArticulos_SelectionChanged);
            this.dgvArticulos.SystemColorsChanged += new System.EventHandler(this.dgvArticulos_SystemColorsChanged);
            // 
            // pbxArticulo
            // 
            this.pbxArticulo.Location = new System.Drawing.Point(812, 45);
            this.pbxArticulo.Name = "pbxArticulo";
            this.pbxArticulo.Size = new System.Drawing.Size(323, 330);
            this.pbxArticulo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxArticulo.TabIndex = 1;
            this.pbxArticulo.TabStop = false;
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(872, 394);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 23);
            this.btnAnterior.TabIndex = 2;
            this.btnAnterior.Text = "<";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(1002, 394);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(75, 23);
            this.btnSiguiente.TabIndex = 3;
            this.btnSiguiente.Text = ">";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // lblCantidadImagenes
            // 
            this.lblCantidadImagenes.AutoSize = true;
            this.lblCantidadImagenes.Location = new System.Drawing.Point(963, 28);
            this.lblCantidadImagenes.Name = "lblCantidadImagenes";
            this.lblCantidadImagenes.Size = new System.Drawing.Size(24, 13);
            this.lblCantidadImagenes.TabIndex = 4;
            this.lblCantidadImagenes.Text = "0/0";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(12, 381);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(90, 23);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(102, 381);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(2);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(90, 23);
            this.btnModificar.TabIndex = 6;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminarFisico
            // 
            this.btnEliminarFisico.Location = new System.Drawing.Point(196, 381);
            this.btnEliminarFisico.Margin = new System.Windows.Forms.Padding(2);
            this.btnEliminarFisico.Name = "btnEliminarFisico";
            this.btnEliminarFisico.Size = new System.Drawing.Size(90, 23);
            this.btnEliminarFisico.TabIndex = 7;
            this.btnEliminarFisico.Text = "Eliminar Físico";
            this.btnEliminarFisico.UseVisualStyleBackColor = true;
            this.btnEliminarFisico.Click += new System.EventHandler(this.btnEliminarFisico_Click);
            // 
            // btnMarcas
            // 
            this.btnMarcas.Location = new System.Drawing.Point(291, 381);
            this.btnMarcas.Name = "btnMarcas";
            this.btnMarcas.Size = new System.Drawing.Size(90, 23);
            this.btnMarcas.TabIndex = 8;
            this.btnMarcas.Text = "Marcas";
            this.btnMarcas.UseVisualStyleBackColor = true;
            this.btnMarcas.Click += new System.EventHandler(this.btnMarcas_Click);
            // 
            // lblFiltroRapido
            // 
            this.lblFiltroRapido.AutoSize = true;
            this.lblFiltroRapido.Location = new System.Drawing.Point(12, 20);
            this.lblFiltroRapido.Name = "lblFiltroRapido";
            this.lblFiltroRapido.Size = new System.Drawing.Size(66, 13);
            this.lblFiltroRapido.TabIndex = 10;
            this.lblFiltroRapido.Text = "Filtro Rapido";
            this.lblFiltroRapido.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(84, 17);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(124, 20);
            this.txtFiltro.TabIndex = 11;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            this.txtFiltro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltro_KeyPress);
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(387, 461);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(121, 21);
            this.lblBuscar.TabIndex = 12;
            this.lblBuscar.Text = "Buscar";
            this.lblBuscar.UseVisualStyleBackColor = true;
            this.lblBuscar.Click += new System.EventHandler(this.lblBuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 423);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Filtro Avanzado";
            // 
            // cbCriterio
            // 
            this.cbCriterio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCriterio.FormattingEnabled = true;
            this.cbCriterio.Location = new System.Drawing.Point(133, 461);
            this.cbCriterio.Name = "cbCriterio";
            this.cbCriterio.Size = new System.Drawing.Size(121, 21);
            this.cbCriterio.TabIndex = 15;
            this.cbCriterio.SelectedIndexChanged += new System.EventHandler(this.cbCriterio_SelectedIndexChanged);
            // 
            // lblCampo
            // 
            this.lblCampo.AutoSize = true;
            this.lblCampo.Location = new System.Drawing.Point(9, 445);
            this.lblCampo.Name = "lblCampo";
            this.lblCampo.Size = new System.Drawing.Size(40, 13);
            this.lblCampo.TabIndex = 16;
            this.lblCampo.Text = "Campo";
            // 
            // lblFiltroAvanzado
            // 
            this.lblFiltroAvanzado.AutoSize = true;
            this.lblFiltroAvanzado.Location = new System.Drawing.Point(260, 446);
            this.lblFiltroAvanzado.Name = "lblFiltroAvanzado";
            this.lblFiltroAvanzado.Size = new System.Drawing.Size(29, 13);
            this.lblFiltroAvanzado.TabIndex = 18;
            this.lblFiltroAvanzado.Text = "Filtro";
            // 
            // lblCriterio
            // 
            this.lblCriterio.AutoSize = true;
            this.lblCriterio.Location = new System.Drawing.Point(130, 446);
            this.lblCriterio.Name = "lblCriterio";
            this.lblCriterio.Size = new System.Drawing.Size(39, 13);
            this.lblCriterio.TabIndex = 19;
            this.lblCriterio.Text = "Criterio";
            // 
            // txtFiltroAvanzado
            // 
            this.txtFiltroAvanzado.Location = new System.Drawing.Point(260, 462);
            this.txtFiltroAvanzado.Name = "txtFiltroAvanzado";
            this.txtFiltroAvanzado.Size = new System.Drawing.Size(121, 20);
            this.txtFiltroAvanzado.TabIndex = 20;
            // 
            // cbCampo
            // 
            this.cbCampo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCampo.FormattingEnabled = true;
            this.cbCampo.Location = new System.Drawing.Point(9, 461);
            this.cbCampo.Name = "cbCampo";
            this.cbCampo.Size = new System.Drawing.Size(121, 21);
            this.cbCampo.TabIndex = 21;
            this.cbCampo.SelectedIndexChanged += new System.EventHandler(this.cbCampo_SelectedIndexChanged);
            // 
            // btnCategorias
            // 
            this.btnCategorias.Location = new System.Drawing.Point(387, 381);
            this.btnCategorias.Name = "btnCategorias";
            this.btnCategorias.Size = new System.Drawing.Size(90, 23);
            this.btnCategorias.TabIndex = 22;
            this.btnCategorias.Text = "Categorias";
            this.btnCategorias.UseVisualStyleBackColor = true;
            this.btnCategorias.Click += new System.EventHandler(this.btnCategorias_Click);
            //
            // btnImagenes
            //
            this.btnImagenes.Location = new System.Drawing.Point(483, 381);
            this.btnImagenes.Name = "btnImagenes";
            this.btnImagenes.Size = new System.Drawing.Size(90, 23);
            this.btnImagenes.TabIndex = 23;
            this.btnImagenes.Text = "Imagenes";
            this.btnImagenes.UseVisualStyleBackColor = true;
            this.btnImagenes.Click += new System.EventHandler(this.btnImagenes_Click);
            //
            // frmArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 494);
            this.Controls.Add(this.btnImagenes);
            this.Controls.Add(this.btnCategorias);
            this.Controls.Add(this.cbCampo);
            this.Controls.Add(this.btnMarcas);
            this.Controls.Add(this.txtFiltroAvanzado);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEliminarFisico);
            this.Controls.Add(this.lblCriterio);
            this.Controls.Add(this.txtFiltro);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.lblFiltroRapido);
            this.Controls.Add(this.lblFiltroAvanzado);
            this.Controls.Add(this.lblCantidadImagenes);
            this.Controls.Add(this.cbCriterio);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.lblCampo);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.pbxArticulo);
            this.Controls.Add(this.dgvArticulos);
            this.Name = "frmArticulos";
            this.Text = "Articulos";
            this.Load += new System.EventHandler(this.frmArticulos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticulos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxArticulo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvArticulos;
        private System.Windows.Forms.PictureBox pbxArticulo;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Label lblCantidadImagenes;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminarFisico;
        private System.Windows.Forms.Button btnMarcas;
        private System.Windows.Forms.Label lblFiltroRapido;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Button lblBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCriterio;
        private System.Windows.Forms.Label lblCampo;
        private System.Windows.Forms.Label lblFiltroAvanzado;
        private System.Windows.Forms.Label lblCriterio;
        private System.Windows.Forms.TextBox txtFiltroAvanzado;
        private System.Windows.Forms.ComboBox cbCampo;
        private System.Windows.Forms.Button btnCategorias;
        private System.Windows.Forms.Button btnImagenes;
    }
}