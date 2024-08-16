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
    public partial class StokAzaltma : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public StokAzaltma(string stockStokId)
        {
            InitializeComponent();

            stockId.Text = stockStokId;
            _dbContext = new AlanyaMedicalHelpEntities();
        }

        private void Temizle()
        {
            numericUpDown1.Value = 0;
            richTextBox1.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int convertStockId = Convert.ToInt32(stockId.Text);
            int convertStockCount = Convert.ToInt32(numericUpDown1.Value);
            StockCount stockCount = new StockCount
            {
                StockId = convertStockId, Type = 2, Count = convertStockCount, CreateDate = DateTime.Now, Description = richTextBox1.Text, Visible = true
            };
            _dbContext.StockCount.Add(stockCount);
            _dbContext.SaveChanges();
            MessageBox.Show("Stok sayısı başarılı bir şekilde azaltıldı..");
            Listele();
            Temizle();
        }

        private void Listele()
        {
            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                int convertStockId = Convert.ToInt32(stockId.Text);
                var values = dbContext.StockCount.Where(x => x.Type == 2 && x.StockId == convertStockId).Select(x => new
                {
                    x.Id,
                    x.Stock.Name,
                    x.Count,
                    Type = "Azaltma",
                    x.CreateDate
                }).ToList();
                dataGridView1.DataSource = values;
                dataGridView1.Columns["Id"].HeaderText = "ID";
                dataGridView1.Columns["Name"].HeaderText = "Stok Adı";
                dataGridView1.Columns["Count"].HeaderText = "Adet";
                dataGridView1.Columns["Type"].HeaderText = "İşlem";
                dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }

            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                int convertStockId = Convert.ToInt32(stockId.Text);
                var values = dbContext.Stock.Where(x => x.Id == convertStockId).FirstOrDefault();
                stockAdi.Text = values.Name;
                stockUyariAdet.Text = values.AlertCount.ToString();
                stockAdet.Text = values.StockCount.Sum(ss => ss.Type == 1 ? ss.Count : -ss.Count).ToString();
                stokAciklama.Text = values.Description;
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void StokAzaltma_Load(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
