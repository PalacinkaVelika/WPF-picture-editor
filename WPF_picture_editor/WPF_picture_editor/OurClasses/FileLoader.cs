using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPF_picture_editor.OurClasses {
    class FileLoader {
        /// <summary>
        /// Logika filtrování nahraných obrázků
        /// </summary>

        // Vlastnosti
        private string saving_path = ""; // Cesta k souboru kam se ukládá

        // Konstruktor
        public FileLoader() {

        }
        // Metody
        public BitmapImage LoadPictureAsBitmap(string path) {
            string format = DetectFileFormat(path);
            if(format == "png" || format == "jpeg" || format == "jpg") {
                return ConvertToBitmapImage(path);
            }
            return null;
        }

        string DetectFileFormat(string path) {
            string extension = Path.GetExtension(path).TrimStart('.');
            return extension.ToLower();
        }

        BitmapImage ConvertToBitmapImage(string imagePath) {
            try {
                using(Bitmap bitmap = new Bitmap(imagePath)) {
                    using(MemoryStream memoryStream = new MemoryStream()) {
                        bitmap.Save(memoryStream, ImageFormat.Png); // Save as PNG for consistent loading
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = new MemoryStream(memoryStream.ToArray());
                        bitmapImage.EndInit();
                        return bitmapImage;
                    }
                }
            } catch(Exception ex) {
                Console.WriteLine($"Error converting image to BitmapImage: {ex.Message}");
                return null;
            }
        }

    }
}
