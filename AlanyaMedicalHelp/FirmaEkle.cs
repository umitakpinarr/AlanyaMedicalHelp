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
    public partial class FirmaEkle : UserControl
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public FirmaEkle()
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Company company = new Company
            { 
                Name = textBox1.Text, CreateDate = DateTime.Now, Visible = true
            };
            _dbContext.Company.Add(company);
            _dbContext.SaveChanges();
            MessageBox.Show("Firma başarılı bir şekilde eklendi.");
            Temizle();
        }

        private void FirmaEkle_Load(object sender, EventArgs e)
        {

        }

        private void Temizle()
        {
            textBox1.Text = "";
      
        }
    }
}
