using Atletika.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Atletika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext _appDbContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            using (_appDbContext = new AppDbContext())
            {
                cbHelyszin.ItemsSource = _appDbContext.Helyszinek.ToList();
                cbVersenyzo.ItemsSource = _appDbContext.Versenyzok.ToList();
            }
        }

        private void OnOkClcik(object sender, RoutedEventArgs e)
        {
            Eredmenyek data = (Eredmenyek)DataContext;

            try
            {
                using (_appDbContext = new AppDbContext())
                {
                    Eredmenyek record = _appDbContext.Eredmenyek.Single(x => x.VersId == data.VersId && x.HelyId == data.HelyId);
                    record.Vsenyszam = data.Vsenyszam;
                    _appDbContext.SaveChanges();

                    MessageBox.Show("Sikeres modisistas", "", MessageBoxButton.OK);

                    data.Vsenyszam = string.Empty;
                    cbHelyszin.SelectedIndex = -1;
                    cbVersenyzo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nem volt sikeres a modisistas", "", MessageBoxButton.OK);
            }
        }

        private void OnExportClcik(object sender, RoutedEventArgs e)
        {
            IEnumerable<Eredmenyek> eredmenyek = null;

            using (_appDbContext = new AppDbContext())
            {
                eredmenyek = _appDbContext.Eredmenyek
                                                                           .Include("Helyszin")
                                                                           .Include("Versenyzo")
                                                                           .ToList();
            }

            List<string> kiment = new List<string>();
            string sor = string.Empty;

            foreach (Eredmenyek eredmeny in eredmenyek)
            {
                sor = $"{eredmeny.Versenyzo.Nev}\t{eredmeny.Helyszin.Ev}\t{eredmeny.Helyszin.Orszag}\t{eredmeny.Helyszin.Varos}\t{eredmeny.Vsenyszam}\t{(eredmeny.Helyezes.HasValue ? eredmeny.Helyezes : 0)}";
                kiment.Add(sor);
            }

            File.WriteAllLines("eredmenyek.txt", kiment);
            MessageBox.Show("Sikeres export", "", MessageBoxButton.OK);
        }
    }
}
