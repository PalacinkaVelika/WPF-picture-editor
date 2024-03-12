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
        public Filter filter;

        // Constructor
        public Filtering() {
            this.picture = null;
            this.filter = Filter.None;
        }


        // Main Method
        public BitmapImage Apply(Filter filter)
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
                    break;

                case Filter.Sepia:
                    pixels = Sepia(pixels);
                    break;
            }

            BitmapSource resultBitmap = BitmapSource.Create(filteredBitmap.PixelWidth, filteredBitmap.PixelHeight, filteredBitmap.DpiX, filteredBitmap.DpiY, PixelFormats.Bgr32, null, pixels, filteredBitmap.PixelWidth * 4);
            BitmapImage resultImage = transformResult(resultBitmap);
            return resultImage;
        }
    

        //private Methods
        byte[] BlackWhite(byte[] pixels)
        {
            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte gray = (byte)(0.30 * pixels[i + 2] + 0.59 * pixels[i + 1] + 0.11 * pixels[i]); // Calculate Brightness
                pixels[i] = pixels[i + 1] = pixels[i + 2] = gray; // Set R, G, B to brightness level
            }

            return pixels;
        }

        byte[] Sepia(byte[] pixels)
        {
            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte gray = (byte)(0.30 * pixels[i + 2] + 0.59 * pixels[i + 1] + 0.11 * pixels[i]); // Calculate Brightness
                byte sepiaR, sepiaG, sepiaB;

                // Check if pixel is close to white
                if (gray > 240)
                {
                    sepiaR = pixels[i + 2]; // Keep original Red component
                    sepiaG = pixels[i + 1]; // Keep original Green component
                    sepiaB = pixels[i];     // Keep original Blue component
                }
                else
                {
                    // Calculate sepia effect for non-white pixels
                    sepiaR = (byte)(gray * 0.393 + pixels[i + 2] * 0.769 + pixels[i + 1] * 0.189); // Calculate Red component for sepia
                    sepiaG = (byte)(gray * 0.349 + pixels[i + 2] * 0.686 + pixels[i + 1] * 0.168); // Calculate Green component for sepia
                    sepiaB = (byte)(gray * 0.272 + pixels[i + 2] * 0.534 + pixels[i + 1] * 0.131); // Calculate Blue component for sepia
                                                                                                   // Limit values to 255
                    sepiaR = (sepiaR > 255) ? (byte)255 : sepiaR;
                    sepiaG = (sepiaG > 255) ? (byte)255 : sepiaG;
                    sepiaB = (sepiaB > 255) ? (byte)255 : sepiaB;
                }

                // Assign new RGB values to pixels
                pixels[i] = sepiaB;
                pixels[i + 1] = sepiaG;
                pixels[i + 2] = sepiaR;
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

            return resultImage;
        }
    }

    public enum Filter
    {
        None,
        BlackWhite,
        Sepia
    }
}
