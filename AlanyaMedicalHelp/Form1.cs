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
    public partial class Form1 : Form
    {
        private readonly MusteriListeleme _musteriListeleme;
        private readonly MusteriEkleme _musteriEkleme;
        private readonly StokEkleme _stokEkleme;
        private readonly StokListeleme _stokListeleme;
        private readonly FirmaEkle _firmaEkle;
        private readonly FirmaListele _firmaListele;
        private readonly GunlukRandevular _gunlukRandevular;
        private readonly StokUyari _stokUyari;

        public Form1(MusteriListeleme musteriListeleme, MusteriEkleme musteriEkleme, StokEkleme stokEkleme, StokListeleme stokListeleme, FirmaEkle firmaEkle, FirmaListele firmaListele, GunlukRandevular gunlukRandevular, StokUyari stokUyari)
        {
            InitializeComponent();
            _musteriListeleme = musteriListeleme;
            _musteriEkleme = musteriEkleme;
            _stokEkleme = stokEkleme;
            _stokListeleme = stokListeleme;
            _firmaEkle = firmaEkle;
            _firmaListele = firmaListele;
            _gunlukRandevular = gunlukRandevular;
            _stokUyari = stokUyari;
        }

       

        private void müşteriGörüntülemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            // Yeni CustomerOperationsControl'ü oluştur ve contentPanel'e ekle
            _musteriListeleme.Dock = DockStyle.Fill;
            _musteriListeleme.Listele();
            
            // Ana panelin içine kontrolü ekle
            panel1.Controls.Add(_musteriListeleme);
        }

        private void müşteriEklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            // Yeni CustomerOperationsControl'ü oluştur ve contentPanel'e ekle
            _musteriEkleme.Dock = DockStyle.Fill;

            // Ana panelin içine kontrolü ekle
            panel1.Controls.Add(_musteriEkleme);
        }

        private void stokEklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            // Yeni CustomerOperationsControl'ü oluştur ve contentPanel'e ekle
            _stokEkleme.Dock = DockStyle.Fill;

            // Ana panelin içine kontrolü ekle
            panel1.Controls.Add(_stokEkleme);
        }

        private void stokGörüntülemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            // Yeni CustomerOperationsControl'ü oluştur ve contentPanel'e ekle
            _stokListeleme.Dock = DockStyle.Fill;
            _stokListeleme.Listele();

            // Ana panelin içine kontrolü ekle
            panel1.Controls.Add(_stokListeleme);
        }

        private void firmaEklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            // Yeni CustomerOperationsControl'ü oluştur ve contentPanel'e ekle
            _firmaEkle.Dock = DockStyle.Fill;

            // Ana panelin içine kontrolü ekle
            panel1.Controls.Add(_firmaEkle);
        }

        private void firmaGörüntülemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            // Yeni CustomerOperationsControl'ü oluştur ve contentPanel'e ekle
            _firmaListele.Dock = DockStyle.Fill;
            _firmaListele.Listele();

            // Ana panelin içine kontrolü ekle
            panel1.Controls.Add(_firmaListele);
        }

        private void günlükRandevularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            // Yeni CustomerOperationsControl'ü oluştur ve contentPanel'e ekle
            _gunlukRandevular.Dock = DockStyle.Fill;
            _gunlukRandevular.Listele();

            // Ana panelin içine kontrolü ekle
            panel1.Controls.Add(_gunlukRandevular);
        }

        private void stokUyarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            // Yeni CustomerOperationsControl'ü oluştur ve contentPanel'e ekle
            _stokUyari.Dock = DockStyle.Fill;
            _stokUyari.Listele();

            // Ana panelin içine kontrolü ekle
            panel1.Controls.Add(_stokUyari);
        }
    }
}
