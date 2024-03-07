using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace WPF_picture_editor.OurClasses {
    class Filtering {
        /// <summary>
        /// Logika filtrování nahraných obrázků
        /// </summary>

        // Vlastnosti
        public BitmapImage picture;

        // Konstruktor
        public Filtering() {
            this.picture = null;
        }


        // Metody
        public BitmapImage Cernobily() {
            BitmapSource bitmapSource = picture;
            WriteableBitmap blackAndWhiteBitmap = new WriteableBitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, bitmapSource.DpiX, bitmapSource.DpiY, PixelFormats.Bgra32, null);

            // Získat byte array z původního obrázku
            byte[] pixels = new byte[bitmapSource.PixelWidth * bitmapSource.PixelHeight * 4];
            bitmapSource.CopyPixels(pixels, bitmapSource.PixelWidth * 4, 0);

            // Projít všechny pixely a změnit je na černobílé
            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte gray = (byte)(0.30 * pixels[i + 2] + 0.59 * pixels[i + 1] + 0.11 * pixels[i]); // Vypočítat jas
                pixels[i] = pixels[i + 1] = pixels[i + 2] = gray; // Nastavit červenou, zelenou a modrou složku na hodnotu jasu
            }

            BitmapSource resultBitmap = BitmapSource.Create(blackAndWhiteBitmap.PixelWidth, blackAndWhiteBitmap.PixelHeight, blackAndWhiteBitmap.DpiX, blackAndWhiteBitmap.DpiY, PixelFormats.Bgr32, null, pixels, blackAndWhiteBitmap.PixelWidth * 4);

            // Vytvořit nový BitmapImage
            BitmapImage resultImage = new BitmapImage();

            // Načíst nově vytvořený BitmapSource do BitmapImage
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(resultBitmap));
                encoder.Save(memoryStream);
                memoryStream.Position = 0;

                resultImage.BeginInit();
                resultImage.CacheOption = BitmapCacheOption.OnLoad;
                resultImage.StreamSource = memoryStream;
                resultImage.EndInit();
            }

            return resultImage;
        }
    }
}
