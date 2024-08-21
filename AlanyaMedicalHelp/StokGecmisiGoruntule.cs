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
    public partial class StokGecmisiGoruntule : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public StokGecmisiGoruntule(string stockId)
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();
            stokId.Text = stockId;
        }

        private void StokGecmisiGoruntule_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            var convertStockId = Convert.ToInt32(stokId.Text);
            var values = _dbContext.StockCount.Where(x => x.StockId == convertStockId)
                .Select(x => new
                {
                    x.Id,
                    x.Stock.Name,
                    x.Count,
                    Type = x.Type == 1 ? "Arttırma" : x.Type == 2 ? "Azaltma" : "Bulunamadı",
                    x.Description,
                    x.CreateDate
                })
                .OrderByDescending(x => x.CreateDate).ToList();
            dataGridView1.DataSource = values;
            dataGridView1.Columns["Id"].HeaderText = "ID";
            dataGridView1.Columns["Name"].HeaderText = "Stok Adı";
            dataGridView1.Columns["Count"].HeaderText = "Adet";
            dataGridView1.Columns["Type"].HeaderText = "İşlem";
            dataGridView1.Columns["Description"].HeaderText = "Açıklama";
            dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                var convertStockId = Convert.ToInt32(stokId.Text);
                var values = _dbContext.StockCount.Where(x => x.StockId == convertStockId && x.Type == 1)
                    .Select(x => new
                    {
                        x.Id,
                        x.Stock.Name,
                        x.Count,
                        Type = x.Type == 1 ? "Arttırma" : x.Type == 2 ? "Azaltma" : "Bulunamadı",
                        x.Description,
                        x.CreateDate
                    })
                    .OrderByDescending(x => x.CreateDate).ToList();
                dataGridView1.DataSource = values;
            }
            else
            {
                Listele();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                var convertStockId = Convert.ToInt32(stokId.Text);
                var values = _dbContext.StockCount.Where(x => x.StockId == convertStockId && x.Type == 2)
                    .Select(x => new
                    {
                        x.Id,
                        x.Stock.Name,
                        x.Count,
                        Type = x.Type == 1 ? "Arttırma" : x.Type == 2 ? "Azaltma" : "Bulunamadı",
                        x.Description,
                        x.CreateDate
                    })
                    .OrderByDescending(x => x.CreateDate).ToList();
                dataGridView1.DataSource = values;
            }
            else
            {
                Listele();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var convertToStartDate = dateTimePicker3.Value.Date;
            var convertToEndDate = dateTimePicker4.Value.Date;
            if (checkBox1.Checked)
            {
                var convertStockId = Convert.ToInt32(stokId.Text);
                var values = _dbContext.StockCount.Where(x => x.StockId == convertStockId && x.Type == 1 && x.CreateDate >= convertToStartDate && x.CreateDate <= convertToEndDate)
                    .Select(x => new
                    {
                        x.Id,
                        x.Stock.Name,
                        x.Count,
                        Type = x.Type == 1 ? "Arttırma" : x.Type == 2 ? "Azaltma" : "Bulunamadı",
                        x.Description,
                        x.CreateDate
                    })
                    .OrderByDescending(x => x.CreateDate).ToList();
                dataGridView1.DataSource = values;
            }
            else if (checkBox2.Checked)
            {
                var convertStockId = Convert.ToInt32(stokId.Text);
                var values = _dbContext.StockCount.Where(x => x.StockId == convertStockId && x.Type == 2 && x.CreateDate >= convertToStartDate && x.CreateDate <= convertToEndDate)
                    .Select(x => new
                    {
                        x.Id,
                        x.Stock.Name,
                        x.Count,
                        Type = x.Type == 1 ? "Arttırma" : x.Type == 2 ? "Azaltma" : "Bulunamadı",
                        x.Description,
                        x.CreateDate
                    })
                    .OrderByDescending(x => x.CreateDate).ToList();
                dataGridView1.DataSource = values;
            }
            else
            {
                var convertStockId = Convert.ToInt32(stokId.Text);
                var values = _dbContext.StockCount.Where(x => x.StockId == convertStockId && x.CreateDate >= convertToStartDate && x.CreateDate <= convertToEndDate)
                    .Select(x => new
                    {
                        x.Id,
                        x.Stock.Name,
                        x.Count,
                        Type = x.Type == 1 ? "Arttırma" : x.Type == 2 ? "Azaltma" : "Bulunamadı",
                        x.Description,
                        x.CreateDate
                    })
                    .OrderByDescending(x => x.CreateDate).ToList();
                dataGridView1.DataSource = values;
            }
        }
    }
}
