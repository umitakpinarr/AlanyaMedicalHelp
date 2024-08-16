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
    public partial class FirmaTumBorcGoruntuleme : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public FirmaTumBorcGoruntuleme(string companyId)
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();
            firmaId.Text = companyId;
        }

        private void FirmaTumBorcGoruntuleme_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            var convertCompanyId = Convert.ToInt32(firmaId.Text);
            var values = _dbContext.CompanyPayment.Where(x => x.CompanyId == convertCompanyId)
                .Select(x => new
                {
                    x.Id,
                    x.Company.Name,
                    x.Price,
                    Type = x.Type == 1 ? "Borç" : x.Type == 2 ? "Ödendi" : "Bulunamadı",
                    x.CreateDate
                    
                })
                .ToList();
            dataGridView1.DataSource = values;
            dataGridView1.Columns["Id"].HeaderText = "ID";
            dataGridView1.Columns["Name"].HeaderText = "Firma Adı";
            dataGridView1.Columns["Price"].HeaderText = "Ücret";
            dataGridView1.Columns["Type"].HeaderText = "İşlem";
            dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
    }
}
