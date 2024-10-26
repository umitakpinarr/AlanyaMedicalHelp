using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanyaMedicalHelp
{
    public partial class MusteriListeleme : UserControl
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;

        public MusteriListeleme(AlanyaMedicalHelpEntities dbContext)
        {
            InitializeComponent();

            _dbContext = dbContext;

        }



        private void MusteriListeleme_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            textBox1.Text = "";
            Listele();
            dataGridView1.Scroll += dataGridView1_Scroll;
        }
        int pageSize = 25;
        int pageNumber = 1;
        bool isLoading = false;

        public void Listele(string name = null, int pageNumber = 1)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Checked = false;
            }
            if (checkBox2.Checked)
            {
                checkBox2.Checked = false;
            }
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, int>(Listele), name, pageNumber);
                return;
            }

            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                var query = dbContext.Customer.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(x => x.Name.Contains(name));
                }

                var newValues = query
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Mail,
                        x.Phone,
                        BorcTl = x.Payment.Where(c => c.PriceType == 1).Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price),
                        BorcEur = x.Payment.Where(c => c.PriceType == 2).Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price),
                        BorcUsd = x.Payment.Where(c => c.PriceType == 3).Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price),
                        x.Country,
                        // Use FirstOrDefault to retrieve the appointment date safely
                        AppointmentDate = x.Appointment.OrderByDescending(a => a.AppointmentDate).Select(a => a.AppointmentDate).FirstOrDefault(),
                        x.CreateDate,
                    })
                    .GroupBy(x => x.Id)  // Group by ID to ensure unique results
                    .Select(g => g.FirstOrDefault())  // Get the first entry from each group
                    .OrderByDescending(x => x.AppointmentDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();  // Remove dynamic to avoid issues

               if (pageNumber == 1)
                {
                    // For the first page, create a new BindingList
                    dataGridView1.DataSource = new BindingList<object>(newValues.Cast<object>().ToList());
                }
                else
                {
                    // For subsequent pages, add new items to the existing BindingList
                    var bindingList = (BindingList<object>)dataGridView1.DataSource;

                    // Get existing IDs to prevent duplicates
                    var existingIds = bindingList.Select(item => ((dynamic)item).Id).ToHashSet();

                    // Add only new items
                    foreach (var item in newValues)
                    {
                        if (!existingIds.Contains(((dynamic)item).Id))
                        {
                            bindingList.Add(item);
                            existingIds.Add(((dynamic)item).Id);  // Add ID to existing IDs to track duplicates
                        }
                    }
                }

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                if(newValues != null && newValues.Count != 0)
                {
                    SetDataGridViewHeaders();

                }
            }



        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (isLoading || e.ScrollOrientation != ScrollOrientation.VerticalScroll) return;

            if (dataGridView1.Rows.Count > 0 &&
                dataGridView1.FirstDisplayedScrollingRowIndex + dataGridView1.DisplayedRowCount(false) >= dataGridView1.Rows.Count)
            {
                // Alt kısma ulaşıldığında bir sonraki sayfayı yükle
                pageNumber++;
                if(checkBox1.Checked)
                {

                }
                else if (checkBox2.Checked)
                {
                    BorcsuzListele(pageNumber);
                }
                else
                {
                    Listele(null, pageNumber);
                }
            }
        }




        private void SetDataGridViewHeaders()
        {
            dataGridView1.Columns["Name"].HeaderText = "Müşteri Adı Soyadı";
            dataGridView1.Columns["Mail"].HeaderText = "Müşteri Mail";
            dataGridView1.Columns["Phone"].HeaderText = "Müşteri Telefon";
            dataGridView1.Columns["BorcTl"].HeaderText = "TL Borç";
            dataGridView1.Columns["BorcEur"].HeaderText = "Borç Eur";
            dataGridView1.Columns["BorcUsd"].HeaderText = "Borç Usd";

            dataGridView1.Columns["Country"].HeaderText = "Müşteri Ülke";
            dataGridView1.Columns["AppointmentDate"].HeaderText = "Son Randevu Tarihi";
            dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
            dataGridView1.Columns["Id"].Visible = false;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Listele(textBox1.Text, 1);
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
                    


                    MusteriBilgileri form2 = new MusteriBilgileri(Id);
                    form2.FormClosed += new FormClosedEventHandler(MusteriBilgileri_FormClosed);
                    form2.Show();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }
        }

        private void MusteriBilgileri_FormClosed(object sender, FormClosedEventArgs e)
        {
            Listele(null, 1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            if (checkBox1.Checked)
            {
                var values = _dbContext.Customer
     .Select(x => new
     {
         x.Id,
         x.Name,
         x.Mail,
         x.Phone,
         BorcTl = x.Payment
             .Where(c => c.PriceType == 1)
             .Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price),
         BorcEur = x.Payment
             .Where(c => c.PriceType == 2)
             .Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price),
         BorcUsd = x.Payment
             .Where(c => c.PriceType == 3)
             .Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price),
         x.Country,
         AppointmentDate = x.Appointment
             .OrderByDescending(a => a.AppointmentDate)
             .Select(a => a.AppointmentDate)
             .FirstOrDefault(),
         x.CreateDate,
     })
     .Where(x => x.BorcTl < 0 || x.BorcEur < 0 || x.BorcUsd < 0 || x.BorcTl > 0 || x.BorcEur > 0 || x.BorcUsd > 0) // Filtreleme
     .OrderByDescending(x => x.BorcTl)
    .ThenByDescending(x => x.BorcUsd)
    .ThenByDescending(x => x.BorcEur)
     .ToList();


                dataGridView1.DataSource = values;
            }
            else
            {
                Listele(null, 1);
            }
            SetDataGridViewHeaders();
        }

        private void BorcsuzListele(int pageNumber)
        {
            var values = _dbContext.Customer
    .Select(x => new
    {
        x.Id,
        x.Name,
        x.Mail,
        x.Phone,
        BorcTl = x.Payment
            .Where(c => c.PriceType == 1)
            .Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price),
        BorcEur = x.Payment
            .Where(c => c.PriceType == 2)
            .Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price),
        BorcUsd = x.Payment
            .Where(c => c.PriceType == 3)
            .Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price),
        x.Country,
        AppointmentDate = x.Appointment
            .OrderByDescending(a => a.AppointmentDate)
            .Select(a => a.AppointmentDate)
            .FirstOrDefault(),
        x.CreateDate,
    })
    .Where(x => (x.BorcEur == null || x.BorcEur == 0) &&
                (x.BorcTl == null || x.BorcTl == 0) &&
                (x.BorcUsd == null || x.BorcUsd == 0))
    .OrderByDescending(x => x.AppointmentDate)
    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
    .ToList();

            if (pageNumber == 1)
            {
                // İlk sayfa için yeni bir BindingList oluştur
                dataGridView1.DataSource = new BindingList<object>(values.Cast<object>().ToList());
            }
            else
            {
                // Sonraki sayfalar için mevcut BindingList'e yeni öğeler ekle
                var bindingList = (BindingList<object>)dataGridView1.DataSource;

                // Mevcut ID'leri al, böylece tekrar eden öğeleri önleyebilirsin
                var existingIds = bindingList.Select(item => ((dynamic)item).Id).ToHashSet();

                // Sadece yeni öğeleri ekle
                foreach (var item in values)
                {
                    if (!existingIds.Contains(((dynamic)item).Id))
                    {
                        bindingList.Add(item);
                        existingIds.Add(((dynamic)item).Id);  // Duplicate takibi için ID'yi mevcut ID'lere ekle
                    }
                }
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            if (checkBox2.Checked)
            {
                BorcsuzListele(1);
            }
            else
            {
                Listele(null, 1);
            }
            SetDataGridViewHeaders();
        }
    }
}
