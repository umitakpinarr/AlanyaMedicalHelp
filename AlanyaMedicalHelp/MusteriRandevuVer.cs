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
    public partial class MusteriRandevuVer : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public MusteriRandevuVer(string id, string customerName, string customerMail, string customerPhone, string customerCountry, string customerImage)
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();

            musteriId.Text = id;
            musteriAdi.Text = customerName;
            musteriMail.Text = customerMail;
            MusteriTel.Text = customerPhone;
            MusteriUlke.Text = customerCountry;
            pictureBox1.ImageLocation = customerImage;
           
        }

        private void MusteriRandevuVer_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            dateTimePicker2.ShowUpDown = true;
        }

        private void Temizle()
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now.Date;
            dateTimePicker2.Text = DateTime.Now.TimeOfDay.ToString();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            int convertCustomerId = Convert.ToInt32(musteriId.Text);
            DateTime convertTime = Convert.ToDateTime(dateTimePicker2.Text);
            DateTime ConvertDatetimePicker1 = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime dateTime = ConvertDatetimePicker1.Date + convertTime.TimeOfDay;
            Appointment appointment = new Appointment
            {
                AppointmentDate = dateTime, CreateDate = DateTime.Now, CustomerId = convertCustomerId, Descriptions = richTextBox2.Text, Note = richTextBox1.Text, Visible = true
            };
            _dbContext.Appointment.Add(appointment);
            _dbContext.SaveChanges();
            MessageBox.Show("Randevu başarılı bir şekilde verildi.");
            Temizle();
        }
    }
}
