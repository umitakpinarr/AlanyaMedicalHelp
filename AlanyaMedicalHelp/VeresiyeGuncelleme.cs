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
    public partial class VeresiyeGuncelleme : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public VeresiyeGuncelleme(string Id)
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();
            label3.Text = Id;
        }

        private void Listele()
        {
            int convertPaymentId = Convert.ToInt32(label3.Text);
            var values = _dbContext.Payment.Where(x => x.Id == convertPaymentId).FirstOrDefault();

            richTextBox1.Text = values.Description;
            numericUpDown1.Value = Convert.ToDecimal(values.Price);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int convertPaymentId = Convert.ToInt32(label3.Text);
            var values = _dbContext.Payment.Where(x => x.Id == convertPaymentId).FirstOrDefault();
            if (values != null)
            {
                _dbContext.Payment.Remove(values);
                _dbContext.SaveChanges();
                MessageBox.Show("Veresiye başarılı bir şekilde silindi.");
            }
            else
            {
                MessageBox.Show("Veresiye bilgileri bulunamadı.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int convertPaymentId = Convert.ToInt32(label3.Text);
            var values = _dbContext.Payment.Where(x => x.Id == convertPaymentId).FirstOrDefault();
            if (values != null)
            {
                values.Price = Convert.ToDouble(numericUpDown1.Value);
                values.Description = richTextBox1.Text;
                _dbContext.Payment.AddOrUpdate(values);
                _dbContext.SaveChanges();
                MessageBox.Show("Veresiye başarılı bir şekilde güncellendi.");
            }
            else
            {
                MessageBox.Show("Veresiye bilgileri bulunamadı.");
            }
        }

        private void VeresiyeGuncelleme_Load(object sender, EventArgs e)
        {
            Listele();
        }
    }
}
