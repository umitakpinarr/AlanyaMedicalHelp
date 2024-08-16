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
    public partial class StokEkleme : UserControl
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public StokEkleme()
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int convertCount = Convert.ToInt32(numericUpDown1.Value);
            int convertCountAlert = Convert.ToInt32(numericUpDown2.Value);
            Stock stock = new Stock
            {
                Name = textBox1.Text,
                FaturaNo = textBox2.Text,
                Count = convertCount,
                AlertCount = convertCountAlert,
                CreateDate = DateTime.Now,
                Description = richTextBox1.Text,
                Visible = true
            };
            _dbContext.Stock.Add(stock);
            _dbContext.SaveChanges();
            StockCount stockCount = new StockCount
            {
                StockId = stock.Id, CreateDate = DateTime.Now, Description = richTextBox1.Text, Count = convertCount, Type = 1, Visible = true
            };
            _dbContext.StockCount.Add(stockCount);
            _dbContext.SaveChanges();
            MessageBox.Show("Stok başarılı bir şekilde eklendi.");
            Temizle();
        }

        private void StokEkleme_Load(object sender, EventArgs e)
        {

        }


        private void Temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            richTextBox1.Text = "";


        }
    }
}
