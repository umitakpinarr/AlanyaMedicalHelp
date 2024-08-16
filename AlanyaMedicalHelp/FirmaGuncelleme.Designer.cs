namespace AlanyaMedicalHelp
{
    partial class FirmaGuncelleme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirmaGuncelleme));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.firmaId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.firmaAdi = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.firmaBorc = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.firmaId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.firmaAdi);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.firmaBorc);
            this.groupBox1.Location = new System.Drawing.Point(14, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(751, 118);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Firma Bilgileri";
            // 
            // firmaId
            // 
            this.firmaId.AutoSize = true;
            this.firmaId.Location = new System.Drawing.Point(479, 38);
            this.firmaId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.firmaId.Name = "firmaId";
            this.firmaId.Size = new System.Drawing.Size(83, 17);
            this.firmaId.TabIndex = 12;
            this.firmaId.Text = "Ümit Akpınar";
            this.firmaId.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Firma Adı :";
            // 
            // firmaAdi
            // 
            this.firmaAdi.AutoSize = true;
            this.firmaAdi.Location = new System.Drawing.Point(130, 38);
            this.firmaAdi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.firmaAdi.Name = "firmaAdi";
            this.firmaAdi.Size = new System.Drawing.Size(83, 17);
            this.firmaAdi.TabIndex = 1;
            this.firmaAdi.Text = "Ümit Akpınar";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Borç :";
            // 
            // firmaBorc
            // 
            this.firmaBorc.AutoSize = true;
            this.firmaBorc.Location = new System.Drawing.Point(131, 67);
            this.firmaBorc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.firmaBorc.Name = "firmaBorc";
            this.firmaBorc.Size = new System.Drawing.Size(156, 17);
            this.firmaBorc.TabIndex = 3;
            this.firmaBorc.Text = "umitakpinarr@icloud.com";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(112)))), ((int)(((byte)(67)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(93, 86);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 35);
            this.button1.TabIndex = 15;
            this.button1.Text = "Güncelle";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 51);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(136, 25);
            this.textBox1.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Firma Adı :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(14, 141);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(751, 208);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Firma Güncelleme";
            // 
            // FirmaGuncelleme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(813, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FirmaGuncelleme";
            this.Text = "Firma Güncelleme";
            this.Load += new System.EventHandler(this.FirmaGuncelleme_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label firmaId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label firmaAdi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label firmaBorc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}