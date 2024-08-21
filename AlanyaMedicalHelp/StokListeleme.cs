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
    public partial class StokListeleme : UserControl
    {
        private readonly AlanyaMedicalHelpEntities _dbContext;
        public StokListeleme()
        {
            InitializeComponent();
            _dbContext = new AlanyaMedicalHelpEntities();
        }

        private void StokListeleme_Load(object sender, EventArgs e)
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
                if (name != null || !String.IsNullOrEmpty(name))
                {
                    var values = dbContext.Stock.Where(x => x.Name.Contains(name))
                        .Select(x => new
                        {
                            x.Id,
                            x.Name,
                            Count = x.StockCount.Sum(ss => ss.Type == 1 ? ss.Count : -ss.Count),
                            x.AlertCount,
                            x.Description,
                            x.CreateDate
                        })
                        .ToList();
                    dataGridView1.DataSource = values;
                }
                else
                {
                    var values = dbContext.Stock
                        .Select(x => new
                        {
                            x.Id,
                            x.Name,
                            Count = x.StockCount.Sum(ss => ss.Type == 1 ? ss.Count : -ss.Count),
                            x.AlertCount,
                            x.Description,
                            x.CreateDate
                        })
                        .ToList();
                    dataGridView1.DataSource = values;
                }

                dataGridView1.Columns["Id"].HeaderText = "ID";
                dataGridView1.Columns["Name"].HeaderText = "Stok Adı";
                dataGridView1.Columns["Count"].HeaderText = "Stok Sayısı";
                dataGridView1.Columns["AlertCount"].HeaderText = "Uyarı Sayısı";
                dataGridView1.Columns["Description"].HeaderText = "Açıklama";
                dataGridView1.Columns["CreateDate"].HeaderText = "Oluşturulma Tarihi";
                dataGridView1.Columns["Id"].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
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
                    string stokAdi = selectedRow.Cells["Name"].Value?.ToString() ?? string.Empty;
                    string stokSayisi = selectedRow.Cells["Count"].Value?.ToString() ?? string.Empty;
                    string StokUyariSayisi = selectedRow.Cells["AlertCount"].Value?.ToString() ?? string.Empty;
                    string StokAciklama = selectedRow.Cells["Description"].Value?.ToString() ?? string.Empty;
                    string StokOlusturmaTarihi = selectedRow.Cells["CreateDate"].Value?.ToString() ?? string.Empty;


                    StokIslemleri form2 = new StokIslemleri(Id);
                    form2.FormClosed += new FormClosedEventHandler(StokIslemleri_FormClosed);
                    form2.Show();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }
        }

        private void StokIslemleri_FormClosed(object sender, FormClosedEventArgs e)
        {
            Listele();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                Listele(textBox1.Text);
            }
            else
            {
                Listele();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
