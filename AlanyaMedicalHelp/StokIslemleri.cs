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
    public partial class StokIslemleri : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public StokIslemleri(string stockStokId)
        {
            InitializeComponent();

            stockId.Text = stockStokId;
            _dbContext = new AlanyaMedicalHelpEntities();

        }

        private void StokIslemleri_Load(object sender, EventArgs e)
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
                int convertStockId = Convert.ToInt32(stockId.Text);
                var values = dbContext.Stock.Where(x => x.Id == convertStockId).FirstOrDefault();
                stockAdi.Text = values.Name;
                stockAdet.Text = values.StockCount.Sum(ss => ss.Type == 1 ? ss.Count : -ss.Count).ToString();
                stockUyariAdet.Text = values.AlertCount.ToString();
                stokAciklama.Text = values.Description;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Id = stockId.Text;



            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                StokGuncelleme form2 = new StokGuncelleme(Id);
                form2.FormClosed += new FormClosedEventHandler(StokGuncelleme_FormClosed);
                form2.Show();
            }

           
        }

        private void StokGuncelleme_FormClosed(object sender, FormClosedEventArgs e)
        {
            Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Id = stockId.Text;
            string stokAdi = stockAdi.Text;
            string stokSayisi = stockAdet.Text;
            string StokUyariSayisi = stockUyariAdet.Text;
            string StokAciklama = stokAciklama.Text;


            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                StokAzaltma form2 = new StokAzaltma(Id);
                form2.FormClosed += new FormClosedEventHandler(StokGuncelleme_FormClosed);
                form2.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string Id = stockId.Text;
            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                StokArttırma form2 = new StokArttırma(Id);
                form2.FormClosed += new FormClosedEventHandler(StokGuncelleme_FormClosed);
                form2.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string Id = stockId.Text;
            StokGecmisiGoruntule form2 = new StokGecmisiGoruntule(Id);
            form2.Show();
        }
    }
}
