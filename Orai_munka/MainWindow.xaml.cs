using Microsoft.Win32;
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

namespace Orai_munka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void mentes_Click(object sender, RoutedEventArgs e)
        {
            // Az adatok összegyűjtése és tárolása
            string nev = txtNev.Text;
            string omAzon = txtOmAzon.Text;
            string szulDatum = txtSzulDatum.Text;
            string matekJegy = txtMatekJegy.Text;
            string magyarJegy = txtMagyarJegy.Text;

            // Ellenőrzés: legalább a nevet meg kell adni
            if (string.IsNullOrEmpty(nev))
            {
                MessageBox.Show("Kérlek add meg a nevedet!", "Hiányzó adat", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //  a név nem tartalmazhat számokat
            if (nev.Any(char.IsDigit))
            {
                MessageBox.Show("A név nem tartalmazhat számokat!", "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //  a matek jegy értéke
            if (!IsValidGrade(matekJegy))
            {
                MessageBox.Show("A matek jegy értéke nem megfelelő!", "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //  a magyar jegy értéke
            if (!IsValidGrade(magyarJegy))
            {
                MessageBox.Show("A magyar jegy értéke nem megfelelő!", "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            // Fájl mentése párbeszédablak megjelenítése
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV fájlok|*.csv";
            saveFileDialog.FileName = "adatok.csv";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // CSV fájl létrehozása 
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        // Fejléc írása
                        sw.WriteLine("Név,OM azonosító,Születési dátum,Matek jegy,Magyar jegy");

                        // Adatok írása
                        sw.WriteLine($"{nev},{omAzon},{szulDatum},{matekJegy},{magyarJegy}");
                    }

                    MessageBox.Show("Az adatok sikeresen mentve!", "Mentés", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba történt a fájl mentése közben: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool IsValidGrade(string jegy)
        {
            if (!double.TryParse(jegy, out double eredmeny))
            {
                // Nem szám
                return false;
            }

            // Az érték 1 és 5 között van
            return eredmeny >= 0 && eredmeny <= 50;
        }
    }
}



