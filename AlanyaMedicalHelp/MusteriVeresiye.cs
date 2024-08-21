using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanyaMedicalHelp
{
    public partial class MusteriVeresiye : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;

        public MusteriVeresiye(string id, string customerName, string customerMail, string customerPhone, string customerCountry, string customerImage)
        {
            InitializeComponent();
            musteriId.Text = id;
            musteriAdi.Text = customerName;
            musteriMail.Text = customerMail;
            MusteriTel.Text = customerPhone;
            MusteriUlke.Text = customerCountry;
            pictureBox1.ImageLocation = customerImage;
            _dbContext = new AlanyaMedicalHelpEntities();
        }

        private void Listele()
        {
            int convertCustomerId = Convert.ToInt32(musteriId.Text);

            var values = _dbContext.Payment.Where(x => x.MusteriId == convertCustomerId)
                .Select(x => new
                {
                    x.Id,
                    AppointmentDesc = x.Appointment.Descriptions,
                    x.Price,
                    PriceType = x.PriceType == 1 ? "TL" : x.PriceType == 2 ? "EUR" : "USD",
                    PaymentStatus = x.PaymentStatus == true ? "Ödendi" : x.PaymentStatus == false ? "Ödenmedi" : "Bulunamadı",  // Koşullu ifadei,
                    x.CreateDate
                })
                .ToList();
            dataGridView1.DataSource = values;
            label7.Text = values.Where(x => x.PriceType == "TL").Sum(ss => ss.PaymentStatus == "Ödenmedi" ? ss.Price : -ss.Price).ToString();
            label11.Text = values.Where(x => x.PriceType == "EUR").Sum(ss => ss.PaymentStatus == "Ödenmedi" ? ss.Price : -ss.Price).ToString();
            label13.Text = values.Where(x => x.PriceType == "USD").Sum(ss => ss.PaymentStatus == "Ödenmedi" ? ss.Price : -ss.Price).ToString();
            dataGridView1.Columns["Id"].HeaderText = "ID";
            dataGridView1.Columns["AppointmentDesc"].HeaderText = "Randevu Açıklaması";
            dataGridView1.Columns["Price"].HeaderText = "Fiyat";
            dataGridView1.Columns["PriceType"].HeaderText = "Para Birimi";
            dataGridView1.Columns["PaymentStatus"].HeaderText = "Ödeme Durumu";
            dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void Temizle()
        {
            numericUpDown1.Value = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int priceType = 1;
            if (radioButton2.Checked)
            {
                priceType = 2;
            }
            else if (radioButton3.Checked)
            {
                priceType = 3;
            }

            int convertCustomerId = Convert.ToInt32(musteriId.Text);
            double covnertPrice = Convert.ToDouble(numericUpDown1.Text);
            Payment payment = new Payment
            {
                MusteriId = convertCustomerId,
                PaymentStatus = true,
                Visible = true,
                CreateDate = DateTime.Now,
                PaymentType = 1,
                Price = covnertPrice,
                PriceType = priceType,
            };
            _dbContext.Payment.Add(payment);
            _dbContext.SaveChanges();
            MessageBox.Show("Veresiye başarılı bir şekilde güncellendi.");
            Listele();
            Temizle();
        }

        private void MusteriVeresiye_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            Listele();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                int convertCustomerId = Convert.ToInt32(musteriId.Text);

                var values = _dbContext.Payment.Where(x => x.MusteriId == convertCustomerId && x.PaymentStatus == false)
                    .Select(x => new
                    {
                        x.Id,
                        AppointmentDesc = x.Appointment.Descriptions,
                        x.Price,
                        PriceType = x.PriceType == 1 ? "TL" : x.PriceType == 2 ? "EUR" : "USD",
                        PaymentStatus = x.PaymentStatus == true ? "Ödendi" : x.PaymentStatus == false ? "Ödenmedi" : "Bulunamadı",  // Koşullu ifadei,
                        x.CreateDate
                    })
                    .ToList();
                dataGridView1.DataSource = values;
                
            }
            else
            {
                Listele();
            }
            dataGridView1.Columns["Id"].HeaderText = "ID";
            dataGridView1.Columns["AppointmentDesc"].HeaderText = "Randevu Açıklaması";
            dataGridView1.Columns["Price"].HeaderText = "Fiyat";
            dataGridView1.Columns["PaymentStatus"].HeaderText = "Ödeme Durumu";
            dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                int convertCustomerId = Convert.ToInt32(musteriId.Text);

                var values = _dbContext.Payment.Where(x => x.MusteriId == convertCustomerId && x.PaymentStatus == true)
                   .Select(x => new
                   {
                       x.Id,
                       AppointmentDesc = x.Appointment.Descriptions,
                       x.Price,
                       PriceType = x.PriceType == 1 ? "TL" : x.PriceType == 2 ? "EUR" : "USD",
                       PaymentStatus = x.PaymentStatus == true ? "Ödendi" : x.PaymentStatus == false ? "Ödenmedi" : "Bulunamadı",  // Koşullu ifadei,
                       x.CreateDate
                   })
                    .ToList();
                dataGridView1.DataSource = values;
                
            }
            else
            {
                Listele();
            }
            
            dataGridView1.Columns["Id"].HeaderText = "ID";
            dataGridView1.Columns["AppointmentDesc"].HeaderText = "Randevu Açıklaması";
            dataGridView1.Columns["Price"].HeaderText = "Fiyat";
            dataGridView1.Columns["PaymentStatus"].HeaderText = "Ödeme Durumu";
            dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
        }
    }
}
