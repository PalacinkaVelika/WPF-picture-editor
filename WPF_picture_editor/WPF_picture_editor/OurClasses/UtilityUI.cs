using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPF_picture_editor.OurClasses {
    class UtilityUI {
        /// <summary>
        /// Metody volané UI komponentami (Logika pro ty metody!!!!)
        /// </summary>

        // Vlastnosti
        Filtering filtering;
        FileLoader fileLoader;
        BitmapImage picture;

        // Konstruktor
        public UtilityUI() {
            filtering = new Filtering();
            fileLoader = new FileLoader();
        }
        // Metody
        public void LoadPicture(string file_path) {
            // Call FileLoader function that handles correct format and such

            // Save it as ThePicture
            SetPicture(null);
        }

        void SetPicture(BitmapImage pic) {

        }
        public BitmapImage GetPicture() {
            return null;

        }

        public void ApplyFilter(Filter filter) {
            switch(filter) {
                case Filter.Cernobily:
                    filtering.Cernobily();
                    break;
                case Filter.Sepia:
                    break;
            }
        }

        public void SavePictureAsFile() {

        }

    }

    public enum Filter {
        Cernobily,
        Sepia
    }

}
