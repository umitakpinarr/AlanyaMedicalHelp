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
    public partial class MusteriListeleme : UserControl
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;

        public MusteriListeleme(AlanyaMedicalHelpEntities dbContext)
        {
            InitializeComponent();

            _dbContext = dbContext;

        }



        private void MusteriListeleme_Load(object sender, EventArgs e)
        {
            Listele();
        }

        public void Listele(string name = null)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(Listele), name);
                return;
            }

            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                if (name != null && !String.IsNullOrEmpty(name))
                {
                    var values = dbContext.Customer.Where(x => x.Name.Contains(name)).Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Mail,
                        x.Phone,
                        CustomerPayment = x.Payment.Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price),
                        x.Adress,
                        x.Country,
                        x.CreateDate,
                        x.Image

                    }).ToList();
                    dataGridView1.DataSource = values;
                    dataGridView1.Columns["Id"].HeaderText = "ID";
                    dataGridView1.Columns["Name"].HeaderText = "Müşteri Adı Soyadı";
                    dataGridView1.Columns["Mail"].HeaderText = "Müşteri Mail";
                    dataGridView1.Columns["Phone"].HeaderText = "Müşteri Telefon";
                    dataGridView1.Columns["CustomerPayment"].HeaderText = "Borç";
                    dataGridView1.Columns["Image"].HeaderText = "Resim Yolu";
                    dataGridView1.Columns["Country"].HeaderText = "Müşteri Ülke";
                    dataGridView1.Columns["Adress"].HeaderText = "Müşteri Adres";
                    dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
                    dataGridView1.Columns["Image"].Visible = false;
                }
                else
                {
                    var values = dbContext.Customer.Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Mail,
                        x.Phone,
                        CustomerPayment = x.Payment.Sum(ss => ss.PaymentStatus == false ? ss.Price : -ss.Price),
                        x.Adress,
                        x.Country,
                        x.CreateDate,
                        x.Image

                    }).ToList();
                    dataGridView1.DataSource = values;
                    dataGridView1.Columns["Id"].HeaderText = "ID";
                    dataGridView1.Columns["Name"].HeaderText = "Müşteri Adı Soyadı";
                    dataGridView1.Columns["Mail"].HeaderText = "Müşteri Mail";
                    dataGridView1.Columns["Phone"].HeaderText = "Müşteri Telefon";
                    dataGridView1.Columns["CustomerPayment"].HeaderText = "Borç";
                    dataGridView1.Columns["Image"].HeaderText = "Resim Yolu";
                    dataGridView1.Columns["Country"].HeaderText = "Müşteri Ülke";
                    dataGridView1.Columns["Adress"].HeaderText = "Müşteri Adres";
                    dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
                    dataGridView1.Columns["Image"].Visible = false;
                }

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Listele(textBox1.Text);
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Eğer tıklanan satır geçerli bir satırsa
                if (e.RowIndex >= 0)
                {
                    var selectedRow = dataGridView1.Rows[e.RowIndex];

                    string Id = selectedRow.Cells["Id"].Value?.ToString() ?? string.Empty;
                    

                    var imagePath = selectedRow.Cells["Image"].Value?.ToString();

                    MusteriBilgileri form2 = new MusteriBilgileri(Id);
                    form2.FormClosed += new FormClosedEventHandler(MusteriBilgileri_FormClosed);
                    form2.Show();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }
        }

        private void MusteriBilgileri_FormClosed(object sender, FormClosedEventArgs e)
        {
            Listele();
        }
    }
}
