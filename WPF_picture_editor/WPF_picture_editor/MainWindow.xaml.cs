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

namespace WPF_picture_editor {
    public partial class MainWindow : Window {
        /// <summary>
        /// tohle funguje jako main class
        /// Inicializuji se tu všechny classy 
        /// </summary>

        // Vlastnosti

        // Konstruktor
        public MainWindow() {
            InitializeComponent();
        }
        // Metody volané tlačítky (logika metod je lepší když je v jiné třídě)
        private void TlacitkemVolanaMetoda() {
            Console.WriteLine("hi mom");
        }
    }
}
