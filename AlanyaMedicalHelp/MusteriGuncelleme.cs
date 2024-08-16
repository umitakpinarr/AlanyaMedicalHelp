using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanyaMedicalHelp
{
    public partial class MusteriGuncelleme : Form, IDisposable
    {
        private string selectedFilePath;
        private readonly AlanyaMedicalHelpEntities _dbContext;

      

        public MusteriGuncelleme(string id)
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();

            musteriId.Text = id;
            Listele();


        }

        private void Listele()
        {
            int convertCustomerId = Convert.ToInt32(musteriId.Text);

            var values = _dbContext.Customer.Where(x => x.Id == convertCustomerId).FirstOrDefault();
            musteriAdi.Text = values.Name;
            MusteriAdres.Text = values.Adress;
            musteriMail.Text = values.Mail;
            MusteriTel.Text = values.Phone;
            MusteriUlke.Text = values.Country;
            pictureBox1.ImageLocation = values.Image;
            textBox1.Text = values.Name;
            textBox2.Text = values.Mail;
            textBox3.Text = values.Phone;
            textBox4.Text = values.Country;
            textBox5.Text = values.Adress;
        }


        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _dbContext?.Dispose(); // Form kapandığında DbContext'i yok et
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

        private void MusteriGuncelleme_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int convertCustomerId = Convert.ToInt32(musteriId.Text);
            var values = _dbContext.Customer.Where(x => x.Id == convertCustomerId).FirstOrDefault();
            if(selectedFilePath != null || !string.IsNullOrEmpty(selectedFilePath))
            {
                values.Image = selectedFilePath;
            }
            values.Name = textBox1.Text;
            values.Mail = textBox2.Text;
            values.Phone = textBox3.Text;
            values.Country = textBox4.Text;
            values.Adress = textBox5.Text;
            _dbContext.Customer.AddOrUpdate(values);
            _dbContext.SaveChanges();
            MessageBox.Show("Müşteri Bilgileri Başarılı bir şekilde güncellendi.");
            Listele();
        }
    }
}
