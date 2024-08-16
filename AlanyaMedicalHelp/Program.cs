using SimpleInjector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlanyaMedicalHelp
{
    internal static class Program
    {
        private static readonly SimpleInjector.Container container = new SimpleInjector.Container();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitializeContainer();

            var mainForm = container.GetInstance<Form1>();
            Application.Run(mainForm);
        }

        private static void InitializeContainer()
        {
            // DbContext ve diğer bağımlılıkları kaydet
            container.Register<AlanyaMedicalHelpEntities>(Lifestyle.Singleton);

            // Form ve kontrolleri kaydet
            container.Register<Form1>(Lifestyle.Singleton);
            container.Register<MusteriEkleme>(Lifestyle.Singleton);
            container.Register<MusteriListeleme>(Lifestyle.Singleton);
            container.Register<StokListeleme>(Lifestyle.Singleton);
            container.Register<StokEkleme>(Lifestyle.Singleton);
            container.Register<FirmaListele>(Lifestyle.Singleton);
            container.Register<FirmaEkle>(Lifestyle.Singleton);
            container.Register<GunlukRandevular>(Lifestyle.Singleton);
            container.Register<StokUyari>(Lifestyle.Singleton);

            // Container doğrulaması
            container.Verify();
        }
    }
}
