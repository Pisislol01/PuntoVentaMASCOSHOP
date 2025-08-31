namespace MASCOSHOP
{
    partial class RelCroquetasBulto
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
            lIDCroqueta = new System.Windows.Forms.Label();
            lIDBulto = new System.Windows.Forms.Label();
            lKilosBulto = new System.Windows.Forms.Label();
            tbIDCroqueta = new System.Windows.Forms.TextBox();
            tbIDBulto = new System.Windows.Forms.TextBox();
            tbKilosBulto = new System.Windows.Forms.TextBox();
            bAgregarRelacion = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            bVerRelaciones = new System.Windows.Forms.Button();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lIDCroqueta
            // 
            lIDCroqueta.AutoSize = true;
            lIDCroqueta.Location = new System.Drawing.Point(45, 25);
            lIDCroqueta.Name = "lIDCroqueta";
            lIDCroqueta.Size = new System.Drawing.Size(70, 15);
            lIDCroqueta.TabIndex = 0;
            lIDCroqueta.Text = "ID Croqueta";
            lIDCroqueta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lIDBulto
            // 
            lIDBulto.AutoSize = true;
            lIDBulto.Location = new System.Drawing.Point(45, 48);
            lIDBulto.Name = "lIDBulto";
            lIDBulto.Size = new System.Drawing.Size(49, 15);
            lIDBulto.TabIndex = 1;
            lIDBulto.Text = "ID Bulto";
            lIDBulto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lKilosBulto
            // 
            lKilosBulto.AutoSize = true;
            lKilosBulto.Location = new System.Drawing.Point(45, 71);
            lKilosBulto.Name = "lKilosBulto";
            lKilosBulto.Size = new System.Drawing.Size(82, 15);
            lKilosBulto.TabIndex = 2;
            lKilosBulto.Text = "Kilos del Bulto";
            lKilosBulto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbIDCroqueta
            // 
            tbIDCroqueta.Location = new System.Drawing.Point(135, 24);
            tbIDCroqueta.MaxLength = 5;
            tbIDCroqueta.Name = "tbIDCroqueta";
            tbIDCroqueta.Size = new System.Drawing.Size(100, 23);
            tbIDCroqueta.TabIndex = 3;
            tbIDCroqueta.TextChanged += tbIDCroqueta_TextChanged;
            // 
            // tbIDBulto
            // 
            tbIDBulto.Location = new System.Drawing.Point(135, 47);
            tbIDBulto.MaxLength = 5;
            tbIDBulto.Name = "tbIDBulto";
            tbIDBulto.Size = new System.Drawing.Size(100, 23);
            tbIDBulto.TabIndex = 4;
            tbIDBulto.TextChanged += tbIDBulto_TextChanged;
            // 
            // tbKilosBulto
            // 
            tbKilosBulto.Location = new System.Drawing.Point(135, 70);
            tbKilosBulto.MaxLength = 5;
            tbKilosBulto.Name = "tbKilosBulto";
            tbKilosBulto.Size = new System.Drawing.Size(100, 23);
            tbKilosBulto.TabIndex = 5;
            // 
            // bAgregarRelacion
            // 
            bAgregarRelacion.Location = new System.Drawing.Point(45, 101);
            bAgregarRelacion.Name = "bAgregarRelacion";
            bAgregarRelacion.Size = new System.Drawing.Size(131, 23);
            bAgregarRelacion.TabIndex = 6;
            bAgregarRelacion.Text = "Agregar Relacion";
            bAgregarRelacion.UseVisualStyleBackColor = true;
            bAgregarRelacion.Click += bAgregarRelacion_Click;
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new System.Drawing.Point(252, 23);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(478, 23);
            textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Location = new System.Drawing.Point(252, 46);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(478, 23);
            textBox2.TabIndex = 8;
            // 
            // bVerRelaciones
            // 
            bVerRelaciones.Location = new System.Drawing.Point(186, 101);
            bVerRelaciones.Name = "bVerRelaciones";
            bVerRelaciones.Size = new System.Drawing.Size(131, 23);
            bVerRelaciones.TabIndex = 9;
            bVerRelaciones.Text = "Ver Relaciones";
            bVerRelaciones.UseVisualStyleBackColor = true;
            bVerRelaciones.Click += bVerRelacion_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(10, 128);
            dataGridView1.Margin = new System.Windows.Forms.Padding(1);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 102;
            dataGridView1.Size = new System.Drawing.Size(780, 312);
            dataGridView1.TabIndex = 10;
            // 
            // RelCroquetasBulto
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(bVerRelaciones);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(bAgregarRelacion);
            Controls.Add(tbKilosBulto);
            Controls.Add(tbIDBulto);
            Controls.Add(tbIDCroqueta);
            Controls.Add(lKilosBulto);
            Controls.Add(lIDBulto);
            Controls.Add(lIDCroqueta);
            Name = "RelCroquetasBulto";
            Text = "Relacion Croquetas Bulto";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lIDCroqueta;
        private System.Windows.Forms.Label lIDBulto;
        private System.Windows.Forms.Label lKilosBulto;
        private System.Windows.Forms.TextBox tbIDCroqueta;
        private System.Windows.Forms.TextBox tbIDBulto;
        private System.Windows.Forms.TextBox tbKilosBulto;
        private System.Windows.Forms.Button bAgregarRelacion;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button bVerRelaciones;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}