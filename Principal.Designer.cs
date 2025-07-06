
namespace MASCOSHOP
{
    partial class Principal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            RBCantidad = new System.Windows.Forms.RadioButton();
            RBPrecio = new System.Windows.Forms.RadioButton();
            textBox3 = new System.Windows.Forms.TextBox();
            button2 = new System.Windows.Forms.Button();
            textBox4 = new System.Windows.Forms.TextBox();
            textBox5 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            textBox6 = new System.Windows.Forms.TextBox();
            RBAgregarProducto = new System.Windows.Forms.RadioButton();
            button3 = new System.Windows.Forms.Button();
            RBCancelarVenta = new System.Windows.Forms.RadioButton();
            button4 = new System.Windows.Forms.Button();
            button6 = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            button7 = new System.Windows.Forms.Button();
            button8 = new System.Windows.Forms.Button();
            bAgregarVentas = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(59, 226);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(126, 23);
            button1.TabIndex = 3;
            button1.Text = "Confirmar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(62, 61);
            textBox1.MaxLength = 5;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(49, 23);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyUp += textBox1_KeyUp;
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Location = new System.Drawing.Point(164, 61);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(624, 23);
            textBox2.TabIndex = 0;
            textBox2.TabStop = false;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(62, 40);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(70, 15);
            label1.TabIndex = 3;
            label1.Text = "ID producto";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(164, 40);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(56, 15);
            label2.TabIndex = 4;
            label2.Text = "Producto";
            // 
            // RBCantidad
            // 
            RBCantidad.AutoSize = true;
            RBCantidad.Checked = true;
            RBCantidad.Location = new System.Drawing.Point(62, 109);
            RBCantidad.Name = "RBCantidad";
            RBCantidad.Size = new System.Drawing.Size(73, 19);
            RBCantidad.TabIndex = 4;
            RBCantidad.TabStop = true;
            RBCantidad.Text = "Cantidad";
            RBCantidad.UseVisualStyleBackColor = true;
            RBCantidad.CheckedChanged += RBCantidad_CheckedChanged;
            // 
            // RBPrecio
            // 
            RBPrecio.AutoSize = true;
            RBPrecio.Location = new System.Drawing.Point(62, 135);
            RBPrecio.Name = "RBPrecio";
            RBPrecio.Size = new System.Drawing.Size(58, 19);
            RBPrecio.TabIndex = 5;
            RBPrecio.TabStop = true;
            RBPrecio.Text = "Precio";
            RBPrecio.UseVisualStyleBackColor = true;
            RBPrecio.CheckedChanged += RBPrecio_CheckedChanged;
            // 
            // textBox3
            // 
            textBox3.Location = new System.Drawing.Point(164, 121);
            textBox3.MaxLength = 9;
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(126, 23);
            textBox3.TabIndex = 2;
            textBox3.TextChanged += textBox3_TextChanged;
            textBox3.KeyUp += textBox3_KeyUp;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(373, 348);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 14;
            button2.Text = "Corte";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox4
            // 
            textBox4.Enabled = false;
            textBox4.Location = new System.Drawing.Point(383, 249);
            textBox4.Name = "textBox4";
            textBox4.Size = new System.Drawing.Size(312, 23);
            textBox4.TabIndex = 0;
            textBox4.TabStop = false;
            // 
            // textBox5
            // 
            textBox5.Enabled = false;
            textBox5.Location = new System.Drawing.Point(383, 278);
            textBox5.Name = "textBox5";
            textBox5.Size = new System.Drawing.Size(312, 23);
            textBox5.TabIndex = 0;
            textBox5.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(294, 252);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(74, 15);
            label3.TabIndex = 15;
            label3.Text = "Venta del dia";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(294, 281);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(49, 15);
            label4.TabIndex = 16;
            label4.Text = "Efectivo";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(294, 311);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(75, 15);
            label5.TabIndex = 17;
            label5.Text = "Ganancia dia";
            // 
            // textBox6
            // 
            textBox6.Enabled = false;
            textBox6.Location = new System.Drawing.Point(383, 308);
            textBox6.Name = "textBox6";
            textBox6.Size = new System.Drawing.Size(312, 23);
            textBox6.TabIndex = 0;
            textBox6.TabStop = false;
            // 
            // RBAgregarProducto
            // 
            RBAgregarProducto.AutoSize = true;
            RBAgregarProducto.Location = new System.Drawing.Point(62, 164);
            RBAgregarProducto.Name = "RBAgregarProducto";
            RBAgregarProducto.Size = new System.Drawing.Size(123, 19);
            RBAgregarProducto.TabIndex = 6;
            RBAgregarProducto.TabStop = true;
            RBAgregarProducto.Text = "Agregar Inventario";
            RBAgregarProducto.UseVisualStyleBackColor = true;
            RBAgregarProducto.CheckedChanged += RBAgregarProducto_CheckedChanged;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(59, 302);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(126, 23);
            button3.TabIndex = 9;
            button3.Text = "Agregar Producto";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // RBCancelarVenta
            // 
            RBCancelarVenta.AutoSize = true;
            RBCancelarVenta.Location = new System.Drawing.Point(62, 192);
            RBCancelarVenta.Name = "RBCancelarVenta";
            RBCancelarVenta.Size = new System.Drawing.Size(103, 19);
            RBCancelarVenta.TabIndex = 7;
            RBCancelarVenta.TabStop = true;
            RBCancelarVenta.Text = "Cancelar Venta";
            RBCancelarVenta.UseVisualStyleBackColor = true;
            RBCancelarVenta.CheckedChanged += RBCancelarVenta_CheckedChanged;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(59, 273);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(126, 23);
            button4.TabIndex = 8;
            button4.Text = "Buscar Producto";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button6
            // 
            button6.Location = new System.Drawing.Point(59, 331);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(123, 23);
            button6.TabIndex = 10;
            button6.Text = "Actualiza Precios";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(59, 361);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(123, 23);
            button5.TabIndex = 11;
            button5.Text = "Compras";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button7
            // 
            button7.Location = new System.Drawing.Point(59, 390);
            button7.Name = "button7";
            button7.Size = new System.Drawing.Size(123, 23);
            button7.TabIndex = 12;
            button7.Text = "Ajustes";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Location = new System.Drawing.Point(59, 419);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(123, 23);
            button8.TabIndex = 13;
            button8.Text = "Buscar Ventas";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // bAgregarVentas
            // 
            bAgregarVentas.Location = new System.Drawing.Point(662, 419);
            bAgregarVentas.Name = "bAgregarVentas";
            bAgregarVentas.Size = new System.Drawing.Size(126, 23);
            bAgregarVentas.TabIndex = 18;
            bAgregarVentas.Text = "Agregar Ventas";
            bAgregarVentas.UseVisualStyleBackColor = true;
            bAgregarVentas.Click += bAgregarVentas_Click;
            // 
            // Principal
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(bAgregarVentas);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button5);
            Controls.Add(button6);
            Controls.Add(button4);
            Controls.Add(RBCancelarVenta);
            Controls.Add(button3);
            Controls.Add(RBAgregarProducto);
            Controls.Add(textBox6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(button2);
            Controls.Add(textBox3);
            Controls.Add(RBPrecio);
            Controls.Add(RBCantidad);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Principal";
            Text = "MASCOSHOP";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton RBCantidad;
        private System.Windows.Forms.RadioButton RBPrecio;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.RadioButton RBAgregarProducto;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RadioButton RBCancelarVenta;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button bAgregarVentas;
    }
}

