using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AlanyaMedicalHelp
{
    public partial class SecilenMusteriTumRandevular : Form
    {
        bool AllListGrid = false;
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public SecilenMusteriTumRandevular(string Id, bool AllList)
        {
            InitializeComponent();
            label1.Text = Id;

            AllListGrid = AllList;
            _dbContext = new AlanyaMedicalHelpEntities();
        }

        private void SecilenMusteriTumRandevular_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(Listele));
                return;
            }

            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                if (AllListGrid == true)
                {
                    int convertCustomerId = Convert.ToInt32(label1.Text);
                    var values = dbContext.Appointment.Where(x => x.CustomerId == convertCustomerId).Select(x => new
                    {
                        x.Id,
                        x.Descriptions,
                        x.Note,
                        PaymentStatus = x.PaymentStatus == true ? "Ödendi" : x.PaymentStatus == false ? "Ödenmedi" : "Bulunamadı",  // Koşullu ifade
                        PaymentType = x.PaymentType == 0 ? "Veresiye" : x.PaymentType == 1 ? "Nakit" : x.PaymentType == 2 ? "Sigorta" : x.PaymentType == 3 ? "Kredi Kartı" : x.PaymentType == 4 ? "EFT" : "Bulunamadı",  // Koşullu ifade
                        x.AppointmentDate,
                        x.CreateDate,
                    }).ToList();
                    dataGridView1.DataSource = values;
                }
                else
                {
                    int convertCustomerId = Convert.ToInt32(label1.Text);
                    var today = DateTime.Today;

                    var values = dbContext.Appointment
        .Where(x => x.CustomerId == convertCustomerId && x.AppointmentDate >= today).Select(x => new
        {
            x.Id,
            x.Descriptions,
            x.Note,
            PaymentStatus = x.PaymentStatus == true ? "Ödendi" : x.PaymentStatus == false ? "Ödenmedi" : "Bulunamadı",  // Koşullu ifade
            PaymentType = x.PaymentType == 0 ? "Veresiye" : x.PaymentType == 1 ? "Nakit" : x.PaymentType == 2 ? "Sigorta" : x.PaymentType == 3 ? "Kredi Kartı" : x.PaymentType == 4 ? "EFT" : "Bulunamadı",  // Koşullu ifade
            x.AppointmentDate,
            x.CreateDate,
        }).ToList();
                    dataGridView1.DataSource = values;
                }
            }


            dataGridView1.Columns["Id"].HeaderText = "ID";
            dataGridView1.Columns["Descriptions"].HeaderText = "Yapılan";
            dataGridView1.Columns["Note"].HeaderText = "Öneriler";
            dataGridView1.Columns["PaymentStatus"].HeaderText = "Ödeme Durumu";
            dataGridView1.Columns["PaymentType"].HeaderText = "Ödeme Türü";
            dataGridView1.Columns["AppointmentDate"].HeaderText = "Randevu Tarihi";
            dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
