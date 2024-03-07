using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
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
            SetPicture(fileLoader.LoadPictureAsBitmap(file_path));

        }

        void SetPicture(BitmapImage pic) {
            picture = pic;
            filtering.picture = pic;
        }

        public BitmapImage GetPicture() {
            return picture;

        }

        public BitmapImage ApplyFilter(Filter filter) {
            switch(filter) {
                case Filter.Cernobily:
                    picture = filtering.Cernobily();
                    return picture;
                case Filter.Sepia:
                    return picture;
            }
            return picture;
        }

        public void SavePictureAsFile() {

        }

    }

    public enum Filter {
        Cernobily,
        Sepia
    }

}
