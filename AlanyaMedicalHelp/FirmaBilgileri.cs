using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AlanyaMedicalHelp
{
    public partial class FirmaBilgileri : Form
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public FirmaBilgileri(string companyId)
        {
            InitializeComponent();
            firmaId.Text = companyId;
            _dbContext = new AlanyaMedicalHelpEntities();
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

        private void FirmaBilgileri_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string companyId = firmaId.Text;
            FirmaGuncelleme firmaGuncelleme = new FirmaGuncelleme(companyId);
            firmaGuncelleme.FormClosed += new FormClosedEventHandler(FirmaGuncelleme_FormClosed);
            firmaGuncelleme.Show();

        }


        private void FirmaGuncelleme_FormClosed(object sender, FormClosedEventArgs e)
        {
            Listele();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string companyName = firmaAdi.Text;
            string companyPrice = firmaBorc.Text;
            string companyId = firmaId.Text;
            FirmaBorcArttirma firmaBorcArttirma = new FirmaBorcArttirma(companyId);
            firmaBorcArttirma.FormClosed += new FormClosedEventHandler(FirmaGuncelleme_FormClosed);
            firmaBorcArttirma.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string companyName = firmaAdi.Text;
            string companyPrice = firmaBorc.Text;
            string companyId = firmaId.Text;
            FirmaBorcAzaltma firmaBorcAzaltma = new FirmaBorcAzaltma(companyId);
            firmaBorcAzaltma.FormClosed += new FormClosedEventHandler(FirmaGuncelleme_FormClosed);
            firmaBorcAzaltma.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string companyId = firmaId.Text;
            FirmaTumBorcGoruntuleme firmaBorcAzaltma = new FirmaTumBorcGoruntuleme(companyId);
            firmaBorcAzaltma.FormClosed += new FormClosedEventHandler(FirmaGuncelleme_FormClosed);
            firmaBorcAzaltma.Show();
        }
    }
}
