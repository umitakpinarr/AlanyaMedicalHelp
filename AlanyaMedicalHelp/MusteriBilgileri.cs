using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AlanyaMedicalHelp
{
    public partial class MusteriBilgileri : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public MusteriBilgileri(string id)
        {
            InitializeComponent();

            musteriId.Text = id;
            
            _dbContext = new AlanyaMedicalHelpEntities();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void MusteriBilgileri_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Id = musteriId.Text;

            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                MusteriGuncelleme form2 = new MusteriGuncelleme(Id);
                form2.FormClosed += new FormClosedEventHandler(MusteriGuncelleme_FormClosed);
                form2.Show();
            }
        }

        private void Listele()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(Listele));
                return;
            }

            int convertCustomerId = Convert.ToInt32(musteriId.Text);

            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                var values = dbContext.Customer.Where(x => x.Id == convertCustomerId).FirstOrDefault();
                if (values != null)
                {
                    musteriAdi.Text = values.Name;
                    MusteriAdres.Text = values.Adress;
                    musteriMail.Text = values.Mail;
                    MusteriTel.Text = values.Phone;
                    MusteriUlke.Text = values.Country;
                    pictureBox1.ImageLocation = values.Image;
                    label7.Text = values.Payment.Where(x=> x.PriceType == 1).Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price).ToString();
                    label11.Text = values.Payment.Where(x => x.PriceType == 2).Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price).ToString();
                    label13.Text = values.Payment.Where(x => x.PriceType == 3).Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price).ToString();
                }
                else
                {
                    MessageBox.Show("Müşteri bulunamadı.");
                }
            }
        }

        private void MusteriGuncelleme_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Id = musteriId.Text;
            string musteriAd = musteriAdi.Text;
            string MusteriAdre = MusteriAdres.Text;
            string MusteriMai = musteriMail.Text;
            string MusteriTelefo = MusteriTel.Text;
            string MusteriUlk = MusteriUlke.Text;
            string imagePat = pictureBox1.ImageLocation;

            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                MusteriRandevuVer form2 = new MusteriRandevuVer(Id, musteriAd, MusteriAdre, MusteriMai, MusteriTelefo, MusteriUlk, imagePat);
                form2.Show();
            }
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            string Id = musteriId.Text;

            string musteriAd = musteriAdi.Text;
            string MusteriAdre = MusteriAdres.Text;
            string MusteriMai = musteriMail.Text;
            string MusteriTelefo = MusteriTel.Text;
            string MusteriUlk = MusteriUlke.Text;
            string imagePat = pictureBox1.ImageLocation;


            SecilenMusteriTumRandevular form2 = new SecilenMusteriTumRandevular(Id,true);
            form2.FormClosed += new FormClosedEventHandler(MusteriGuncelleme_FormClosed);
            form2.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string Id = musteriId.Text;

            string musteriAd = musteriAdi.Text;
            string MusteriAdre = MusteriAdres.Text;
            string MusteriMai = musteriMail.Text;
            string MusteriTelefo = MusteriTel.Text;
            string MusteriUlk = MusteriUlke.Text;
            string imagePat = pictureBox1.ImageLocation;


            SecilenMusteriTumRandevular form2 = new SecilenMusteriTumRandevular(Id,false);
            form2.FormClosed += new FormClosedEventHandler(MusteriGuncelleme_FormClosed);
            form2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string Id = musteriId.Text;

            string musteriAd = musteriAdi.Text;
            string MusteriAdre = MusteriAdres.Text;
            string MusteriMai = musteriMail.Text;
            string MusteriTelefo = MusteriTel.Text;
            string MusteriUlk = MusteriUlke.Text;
            string imagePat = pictureBox1.ImageLocation;


            MusteriVeresiye form2 = new MusteriVeresiye(Id, musteriAd, MusteriAdre, MusteriMai, MusteriTelefo, MusteriUlk, imagePat);
            form2.FormClosed += new FormClosedEventHandler(MusteriGuncelleme_FormClosed);
            form2.Show();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
