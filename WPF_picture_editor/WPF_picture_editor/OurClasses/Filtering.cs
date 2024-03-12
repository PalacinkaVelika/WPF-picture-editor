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
        /// Filter logic
        /// </summary>

        // Utilities
        public BitmapImage picture;

        // Constructor
        public Filtering() {
            this.picture = null;
        }


        // Main Method
        public BitmapImage ApplyFilter(Filter filter)
        {
            BitmapSource bitmapSource = picture;
            WriteableBitmap filteredBitmap = new WriteableBitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, bitmapSource.DpiX, bitmapSource.DpiY, PixelFormats.Bgra32, null);

            // Get array of bites from picture
            byte[] pixels = new byte[bitmapSource.PixelWidth * bitmapSource.PixelHeight * 4];
            bitmapSource.CopyPixels(pixels, bitmapSource.PixelWidth * 4, 0);

            switch (filter)
            {
                case Filter.BlackWhite:
                    pixels = BlackWhite(pixels);

                case Filter.Sepia:
                  
            }

            BitmapSource resultBitmap = BitmapSource.Create(filteredBitmap.PixelWidth, filteredBitmap.PixelHeight, filteredBitmap.DpiX, filteredBitmap.DpiY, PixelFormats.Bgr32, null, pixels, filteredBitmap.PixelWidth * 4);
            BitmapImage resultImage = transformResult(resultBitmap)
            return resultImage;
        }
    }

        //private Methods
        BitmapImage BlackWhite(byte[] pixels) {

            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte gray = (byte)(0.30 * pixels[i + 2] + 0.59 * pixels[i + 1] + 0.11 * pixels[i]); // Calculate Brightness
                pixels[i] = pixels[i + 1] = pixels[i + 2] = gray; // Set R, G, B to brightness level
            }

            return pixels;
        }





        BitmapImage transformResult(BitmapSource resultBitmap)
        {
            BitmapImage resultImage = new BitmapImage();


            // transform from BitmapSoure to BitmapImage
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
        }
    }
}
