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
    public partial class FirmaGuncelleme : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public FirmaGuncelleme(string companyId)
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();
            firmaId.Text = companyId;

        }

        private void FirmaGuncelleme_Load(object sender, EventArgs e)
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
                int convertCompanyId = Convert.ToInt32(firmaId.Text);
                var values = _dbContext.Company.Where(x => x.Id == convertCompanyId).FirstOrDefault();
                firmaAdi.Text = values.Name;
                firmaBorc.Text = values.CompanyPayment.Sum(ss => ss.Type == 1 ? ss.Price : -ss.Price).ToString();
                textBox1.Text = values.Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int convertCompanyId = Convert.ToInt32(firmaId.Text);
            var values = _dbContext.Company.Where(x => x.Id == convertCompanyId).FirstOrDefault();
            values.Name = textBox1.Text;
            _dbContext.Company.AddOrUpdate(values);
            _dbContext.SaveChanges();
            MessageBox.Show("Firma bilgileri başarılı bir şekilde güncellendi.");
            Listele();

        }
    }
}
