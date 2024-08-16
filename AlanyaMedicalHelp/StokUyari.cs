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
    public partial class StokUyari : UserControl
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;

        public StokUyari()
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();
        }

        private void StokUyari_Load(object sender, EventArgs e)
        {
            Listele();
        }

        public void Listele()
        {
            var filteredData = _dbContext.Stock
    .Where(x => x.StockCount.Sum(ss => ss.Type == 1 ? ss.Count : -ss.Count) < x.AlertCount)
    .Select(x => new
    {
        x.Id,
        x.Name,
        Count = x.StockCount.Sum(ss => ss.Type == 1 ? ss.Count : -ss.Count),
        x.AlertCount,
        x.Description
    })
    .ToList();
            dataGridView1.DataSource = filteredData;
            dataGridView1.Columns["Id"].HeaderText = "ID";
            dataGridView1.Columns["Name"].HeaderText = "Stok Adı";
            dataGridView1.Columns["Count"].HeaderText = "Adet";
            dataGridView1.Columns["AlertCount"].HeaderText = "Uyarı Adet";
            dataGridView1.Columns["Description"].HeaderText = "Açıklama";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
    }
}
