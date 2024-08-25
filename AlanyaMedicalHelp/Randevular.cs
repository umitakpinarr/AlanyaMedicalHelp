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

namespace AlanyaMedicalHelp
{
    public partial class Randevular : UserControl
    {
        public Randevular()
        {
            InitializeComponent();
        }

        public void Listele()
        {
            using (var dbContext = new AlanyaMedicalHelpEntities())
            {

                var convertToStartDate = dateTimePicker1.Value.Date;
                var convertToEndDate = dateTimePicker2.Value.Date.AddDays(1);
                var values = dbContext.Appointment.Where(x => x.AppointmentDate >= convertToStartDate && x.AppointmentDate <= convertToEndDate).Select(x => new
                {
                    x.Id,
                    x.Customer.Name,
                    x.Descriptions,
                    x.Note,
                    PaymentStatus = x.PaymentStatus == true ? "Ödendi" : x.PaymentStatus == false ? "Ödenmedi" : "Bulunamadı",  // Koşullu ifade
                    PaymentType = x.PaymentType == 1 ? "Nakit" : x.PaymentType == 2 ? "Sigorta" : x.PaymentType == 3 ? "Kredi Kartı" : x.PaymentType == 4 ? "EFT" : "Bulunamadı",  // Koşullu ifade
                    x.AppointmentDate,
                    x.CreateDate,
                }).OrderByDescending(x => x.AppointmentDate).ToList();
                dataGridView1.DataSource = values;
                dataGridView1.DataSource = values;
                dataGridView1.Columns["Id"].HeaderText = "ID";
                dataGridView1.Columns["Name"].HeaderText = "Müşteri Adı";
                dataGridView1.Columns["Descriptions"].HeaderText = "Açıklama";
                dataGridView1.Columns["Note"].HeaderText = "Not";
                dataGridView1.Columns["PaymentStatus"].HeaderText = "Ödeme Durumu";
                dataGridView1.Columns["PaymentType"].HeaderText = "Ödeme Türü";
                dataGridView1.Columns["AppointmentDate"].HeaderText = "Randevu Tarihi";
                dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturma Tarihi";
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Eğer tıklanan satır geçerli bir satırsa
                if (e.RowIndex >= 0)
                {
                    var selectedRow = dataGridView1.Rows[e.RowIndex];

                    string Id = selectedRow.Cells["Id"].Value?.ToString() ?? string.Empty;



                    RandevuGuncelle form2 = new RandevuGuncelle(Id);
                    form2.FormClosed += new FormClosedEventHandler(RandvuGuncelle_FormClosed);
                    form2.Show();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }
        }

        private void RandvuGuncelle_FormClosed(object sender, FormClosedEventArgs e)
        {
            Listele();
        }
    }
}
