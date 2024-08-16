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
    public partial class FirmaListele : UserControl
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public FirmaListele()
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();
        }

        private void FirmaListele_Load(object sender, EventArgs e)
        {
            Listele();
        }
        public void Listele()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(Listele));
                return;
            }

            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                var values = dbContext.Company
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Payment = x.CompanyPayment.Sum(ss => ss.Type == 1 ? ss.Price : -ss.Price),
                    x.CreateDate
                })
                .ToList();
                dataGridView1.DataSource = values;
                dataGridView1.Columns["Id"].HeaderText = "ID";
                dataGridView1.Columns["Name"].HeaderText = "Firma Adı";
                dataGridView1.Columns["Payment"].HeaderText = "Borç";
                dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
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



                    FirmaBilgileri form2 = new FirmaBilgileri(Id);
                    form2.FormClosed += new FormClosedEventHandler(FirmaBilgileri_FormClosed);
                    form2.Show();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }
        }

        private void FirmaBilgileri_FormClosed(object sender, FormClosedEventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
