using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanyaMedicalHelp
{
    public partial class RandevuGuncelle : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;



        public RandevuGuncelle(string id)
        {
            InitializeComponent();

            randevuId.Text = id;
           
            _dbContext = new AlanyaMedicalHelpEntities();

        }

        private void RandevuGuncelle_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            Listele();
        }

        private void Listele()
        {
            int convertAppointmentId = Convert.ToInt32(randevuId.Text);
            var values = _dbContext.Appointment.Where(x => x.Id == convertAppointmentId).FirstOrDefault();
            richTextBox1.Text = values.Note;
            richTextBox2.Text = values.Descriptions;
            dateTimePicker1.Value = values.AppointmentDate.Value.Date;
            dateTimePicker2.Value = values.AppointmentDate.Value;
            musteriAdi.Text = values.Customer.Name;
            musteriMail.Text = values.Customer.Mail;
            MusteriTel.Text = values.Customer.Phone;
            MusteriUlke.Text = values.Customer.Country;
            if(values.PaymentStatus == true)
            {
                checkBox1.Checked = true;
            }
            numericUpDown1.Value = Convert.ToDecimal(values.Price);
            if(values.PriceType == 1)
            {
                radioButton1.Checked = true;
            }
            else  if(values.PriceType == 2)
            {
                radioButton2.Checked = true;
            }
            else if (values.PriceType == 3)
            {
                radioButton3.Checked = true;
            }
            if (values.PaymentType == 1)
            {
                comboBox1.Text = "Nakit";
            }
            else if (values.PaymentType == 2)
            {
                comboBox1.Text = "Sigorta";

            }
            else if (values.PaymentType == 3)
            {
                comboBox1.Text = "Kredi kartı";

            }
            else if (values.PaymentType == 4)
            {
                comboBox1.Text = "EFT";

            }
            pictureBox1.ImageLocation = values.Customer.Image;
            if(values.AppointmentStatus == true)
            {
                checkBox2.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int convertAppointmentId = Convert.ToInt32(randevuId.Text);
            var values = _dbContext.Appointment.Where(x => x.Id == convertAppointmentId).FirstOrDefault();
            DateTime convertTime = Convert.ToDateTime(dateTimePicker2.Text);
            DateTime dateTime = dateTimePicker1.Value.Date + convertTime.TimeOfDay;
            values.AppointmentDate = dateTime;
            values.Descriptions = richTextBox2.Text;
            values.Note = richTextBox1.Text;
            int paymentType = 0;
            int priceType = 1;
            if (radioButton2.Checked)
            {
                priceType = 2;
            }
            else if (radioButton3.Checked)
            {
                priceType = 3;
            }
           
            if (comboBox1.Text == "Nakit")
            {
                paymentType = 1;
            }
            else if (comboBox1.Text == "Sigorta")
            {
                paymentType = 2;

            }
            else if (comboBox1.Text == "Kredi kartı")
            {
                paymentType = 3;

            }
            else if (comboBox1.Text == "EFT")
            {
                paymentType = 4;

            }

            
            values.Price = Convert.ToDouble(numericUpDown1.Value);
            values.PaymentStatus = checkBox1.Checked;
            values.PriceType = priceType;
            values.PaymentType = paymentType;
            values.AppointmentStatus = checkBox2.Checked;
            if (values.PaymentStatus == false && values.Payment.Where(x=> x.AppointmentId == values.Id).Any() == false && paymentType == 0)
            {
                Payment payment = new Payment
                {
                    CreateDate = DateTime.Now,
                    AppointmentId = convertAppointmentId,
                    MusteriId = values.CustomerId,
                    PaymentStatus = false,
                    PaymentType = paymentType,
                    Price = Convert.ToDouble(numericUpDown1.Value),
                    Visible = true,
                    PriceType = priceType
                };
                _dbContext.Payment.Add(payment);
                _dbContext.SaveChanges();
            }
            else if(values.Payment.Where(x => x.AppointmentId == values.Id).Any() == true && paymentType != 0)
            {
                var values2 = _dbContext.Payment.Where(x => x.AppointmentId == convertAppointmentId).FirstOrDefault();
                _dbContext.Payment.Remove(values2);
                _dbContext.SaveChanges();
            }

            _dbContext.Appointment.AddOrUpdate(values);
            _dbContext.SaveChanges();


            MessageBox.Show("Randevu bilgileri başarılı bir şekilde güncellendi.");

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
