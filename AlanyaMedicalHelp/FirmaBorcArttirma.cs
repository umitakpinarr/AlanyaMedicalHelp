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
    public partial class FirmaBorcArttirma : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public FirmaBorcArttirma(string companyId)
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();

            firmaId.Text = companyId;
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
                var values = dbContext.Company.Where(x => x.Id == convertCompanyId).FirstOrDefault();
                firmaAdi.Text = values.Name;
                firmaBorc.Text = values.CompanyPayment.Sum(ss => ss.Type == 1 ? ss.Price : -ss.Price).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int convertCompanyId = Convert.ToInt32(firmaId.Text);
            int convertPrice = Convert.ToInt32(numericUpDown1.Value);

            CompanyPayment companyPayment = new CompanyPayment
            {
                CompanyId = convertCompanyId, CreateDate = DateTime.Now, Description = richTextBox1.Text, PaymentNo = textBox1.Text, Price = convertPrice, Visible = true, Type = 1
            };
            _dbContext.CompanyPayment.Add(companyPayment);
            _dbContext.SaveChanges();
            MessageBox.Show("Firma borcu başarılı bir şekilde eklendi.");
            Listele();
            Temizle();

        }

        private void FirmaBorcArttirma_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Temizle()
        {
            textBox1.Text = "";
            richTextBox1.Text = "";
            numericUpDown1.Value = 0;
        }
    }
}
