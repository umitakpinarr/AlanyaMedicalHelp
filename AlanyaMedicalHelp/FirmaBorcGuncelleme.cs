using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanyaMedicalHelp
{
    public partial class FirmaBorcGuncelleme : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;

        public FirmaBorcGuncelleme(string Id)
        {
            InitializeComponent();
            firmaId.Text = Id;
            _dbContext = new AlanyaMedicalHelpEntities();
        }

        private void FirmaBorcGuncelleme_Load(object sender, EventArgs e)
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
                var values = dbContext.CompanyPayment.Where(x => x.Id == convertCompanyId).FirstOrDefault();
                firmaAdi.Text = values.Company.Name;
                textBox1.Text = values.PaymentNo;
                numericUpDown1.Value = Convert.ToDecimal(values.Price);
                richTextBox1.Text = values.Description;
                dateTimePicker1.Value = Convert.ToDateTime(values.CreateDate).Date;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int convertCompanyId = Convert.ToInt32(firmaId.Text);
            var values = _dbContext.CompanyPayment.Where(x => x.Id == convertCompanyId).FirstOrDefault();
            values.CreateDate = Convert.ToDateTime(values.CreateDate);
            values.Description = richTextBox1.Text;
            values.Price = numericUpDown1.Value;
            values.PaymentNo = textBox1.Text;
            _dbContext.CompanyPayment.AddOrUpdate(values);
            _dbContext.SaveChanges();
            MessageBox.Show("Firma borcu başarılı bir şekilde güncellendi.");
        }
    }
}
