using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanyaMedicalHelp
{
    public partial class StokGuncelleme : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public StokGuncelleme(string stockStokId)
        {
            InitializeComponent();


            stockId.Text = stockStokId;

            _dbContext = new AlanyaMedicalHelpEntities();
        }

        private void Listele()
        {

            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                int convertStockId = Convert.ToInt32(stockId.Text);
                var values = dbContext.Stock.Where(x => x.Id == convertStockId).FirstOrDefault();
                stockAdi.Text = values.Name;
                stockUyariAdet.Text = values.AlertCount.ToString();
                stockAdet.Text = values.StockCount.Sum(ss => ss.Type == 1 ? ss.Count : -ss.Count).ToString();
                stokAciklama.Text = values.Description;
                textBox1.Text = values.Name;
                richTextBox1.Text = values.Description;
                numericUpDown2.Value = Convert.ToDecimal(values.AlertCount);
            }

        }

        private void StokGuncelleme_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int convertStockId = Convert.ToInt32(stockId.Text);
            var values = _dbContext.Stock.Where(x => x.Id == convertStockId).FirstOrDefault();
            values.AlertCount = Convert.ToInt32(numericUpDown2.Value);
            values.Name = textBox1.Text;
            values.Description = richTextBox1.Text;
            _dbContext.Stock.AddOrUpdate(values);
            _dbContext.SaveChanges();
            MessageBox.Show("Stok bilgileri başarılı bir şekilde güncellendi.");
            Listele();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
