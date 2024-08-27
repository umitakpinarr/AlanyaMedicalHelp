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

namespace AlanyaMedicalHelp
{
    public partial class YedekAl : UserControl
    {
        public YedekAl()
        {
            InitializeComponent();
        }

        private async Task button1_ClickAsync(object sender, EventArgs e)
        {
           

           
        }

        public void BackupDatabase(string backupPath)
        {
            using (var dbContext = new AlanyaMedicalHelpEntities())
            {
                // Veritabanı adı alınıyor
                var dbName = dbContext.Database.Connection.Database;

                // SQL komutu hazırlanıyor
                var sql = $@"BACKUP DATABASE [{dbName}] TO DISK = '{backupPath}' 
                     WITH FORMAT, 
                     MEDIANAME = 'SQLServerBackups', 
                     NAME = 'Full Backup of {dbName}';";

                // SQL komutu çalıştırılıyor
                dbContext.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sql);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SQL Server Backup Files (*.bak)|*.bak";
            saveFileDialog.Title = "Yedekleme Dosyasını Kaydet";
            saveFileDialog.FileName = "DatabaseBackup.bak";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // ProgressBar'ı başlat ve label'ı güncelle
                progressBar1.Style = ProgressBarStyle.Marquee;

                // Yedekleme işlemini asenkron olarak gerçekleştir
                await Task.Run(() => BackupDatabase(saveFileDialog.FileName));

                // Yedekleme tamamlandığında ProgressBar ve label'ı güncelle
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 100;
                MessageBox.Show("Yedekleme tamamlandı!");
            }
        }
    }
}
