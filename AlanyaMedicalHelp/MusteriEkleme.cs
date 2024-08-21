using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AlanyaMedicalHelp
{
    public partial class MusteriEkleme : UserControl
    {
        private string selectedFilePath;
        private readonly AlanyaMedicalHelpEntities _dbContext;

        public MusteriEkleme(AlanyaMedicalHelpEntities dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

       

        private void MusteriEkleme_Load(object sender, EventArgs e)
        {

        }
        private void Temizle()
        {
            musteriAdi.Text = "";
            MusteriMail.Text = "";
            MusteriTel.Text = "";
            MusteriUlke.Text = "";
            pictureBox2.ImageLocation = string.Empty;
            pictureBox2.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer
            {
                Name = musteriAdi.Text, Country = MusteriUlke.Text, CreateDate = DateTime.Now, Mail = MusteriMail.Text, Phone = MusteriTel.Text, Visible = true, Image = selectedFilePath
            };
            _dbContext.Customer.Add(customer);
            _dbContext.SaveChanges();
            MessageBox.Show("Müşteri Başarılı bir şekilde kaydedildi.");
            Temizle();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Seçilen dosyanın yolunu al ve sakla
                    selectedFilePath = openFileDialog.FileName;

                    // Resmi PictureBox'ta göster
                    pictureBox2.Image = Image.FromFile(selectedFilePath);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                }
            }
        }
    }
}
