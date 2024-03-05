using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using WPF_picture_editor.OurClasses;

namespace WPF_picture_editor {
    public partial class MainWindow : Window {
        /// <summary>
        /// tohle funguje jako main class
        /// Inicializuji se tu všechny classy 
        /// </summary>

        // Vlastnosti
        UtilityUI utility;
        // Konstruktor
        public MainWindow() {
            utility = new UtilityUI();
            InitializeComponent();
        }
        // Metody volané tlačítky (logika metod je lepší když je v jiné třídě)

        // Nahrát fotku
        private void Button_Click_1(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*"; // You can customize the file filter as needed

            if(openFileDialog.ShowDialog() == true) {
                string selectedFilePath = openFileDialog.FileName;
                utility.LoadPicture(selectedFilePath);
                BitmapImage chosenImage = utility.GetPicture();
                ImageHolder.Source = chosenImage;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            utility.ApplyFilter(Filter.Cernobily);
        }
    }
}
