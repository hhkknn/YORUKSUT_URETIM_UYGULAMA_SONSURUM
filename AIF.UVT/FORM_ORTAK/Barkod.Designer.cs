namespace AIF.UVT
{
    partial class Barkod
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.lblYazici = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnArti = new System.Windows.Forms.Button();
            this.txtPrintMik = new System.Windows.Forms.TextBox();
            this.btnEksi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(24, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox1, 9);
            this.pictureBox1.Size = new System.Drawing.Size(355, 344);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(24, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(290, 44);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.Location = new System.Drawing.Point(24, 403);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(355, 44);
            this.button2.TabIndex = 3;
            this.button2.Text = "YAZDIR";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button3.Location = new System.Drawing.Point(406, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 44);
            this.button3.TabIndex = 4;
            this.button3.Text = "YAZDIR";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.button4, 3);
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button4.Location = new System.Drawing.Point(406, 403);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(288, 44);
            this.button4.TabIndex = 5;
            this.button4.Text = "KAPAT";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.button4, 3, 10);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbPrinter, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblYazici, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnArti, 5, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtPrintMik, 4, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnEksi, 3, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(722, 501);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // cmbPrinter
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cmbPrinter, 3);
            this.cmbPrinter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(406, 78);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(288, 21);
            this.cmbPrinter.TabIndex = 6;
            this.cmbPrinter.SelectedValueChanged += new System.EventHandler(this.cmbPrinter_SelectedValueChanged);
            // 
            // lblYazici
            // 
            this.lblYazici.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblYazici, 3);
            this.lblYazici.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYazici.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYazici.Location = new System.Drawing.Point(406, 50);
            this.lblYazici.Name = "lblYazici";
            this.lblYazici.Size = new System.Drawing.Size(288, 25);
            this.lblYazici.TabIndex = 7;
            this.lblYazici.Text = "Yazıcı Seçimi";
            this.lblYazici.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(406, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Yazdırılacak Miktar";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnArti
            // 
            this.btnArti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnArti.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArti.ForeColor = System.Drawing.Color.Green;
            this.btnArti.Location = new System.Drawing.Point(578, 158);
            this.btnArti.Name = "btnArti";
            this.tableLayoutPanel1.SetRowSpan(this.btnArti, 3);
            this.btnArti.Size = new System.Drawing.Size(116, 69);
            this.btnArti.TabIndex = 10;
            this.btnArti.Text = "+";
            this.btnArti.UseVisualStyleBackColor = true;
            this.btnArti.Click += new System.EventHandler(this.btnArti_Click);
            // 
            // txtPrintMik
            // 
            this.txtPrintMik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrintMik.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrintMik.Location = new System.Drawing.Point(514, 183);
            this.txtPrintMik.Name = "txtPrintMik";
            this.txtPrintMik.Size = new System.Drawing.Size(58, 26);
            this.txtPrintMik.TabIndex = 12;
            this.txtPrintMik.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrintMik.Click += new System.EventHandler(this.txtPrintMik_Click);
            this.txtPrintMik.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrintMik_KeyPress);
            // 
            // btnEksi
            // 
            this.btnEksi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEksi.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEksi.ForeColor = System.Drawing.Color.Red;
            this.btnEksi.Location = new System.Drawing.Point(406, 158);
            this.btnEksi.Name = "btnEksi";
            this.tableLayoutPanel1.SetRowSpan(this.btnEksi, 3);
            this.btnEksi.Size = new System.Drawing.Size(102, 69);
            this.btnEksi.TabIndex = 13;
            this.btnEksi.Text = "-";
            this.btnEksi.UseVisualStyleBackColor = true;
            this.btnEksi.Click += new System.EventHandler(this.btnEksi_Click);
            // 
            // Barkod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(722, 501);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Barkod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Barkod Görünüle";
            this.Load += new System.EventHandler(this.Barkod_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.Label lblYazici;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnArti;
        private System.Windows.Forms.TextBox txtPrintMik;
        private System.Windows.Forms.Button btnEksi;
    }
}