using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreeImageAPI;

namespace BniIconBuilder
{
    class Targa
    {

        public Targa()
        {
            // Check if FreeImage.dll is available (can be in %path%).
            if (!FreeImage.IsAvailable())
            {
                throw new Exception("FreeImage.dll seems to be missing. Aborting.");
            }
        }

        /// <summary>
        /// Create tga buffer from the images files
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        public byte[] CreateTga(string[] fileNames)
        {
            var bytes = GetBytes(ImplodeImages(LoadImages(fileNames)));
            return bytes;
        }

        /// <summary>
        /// Create tga buffer from the bitmaps
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public byte[] CreateTga(Bitmap[] images)
        {
            var bytes = GetBytes(ImplodeImages(images));
            return bytes;
        }

        public void Convert2Tga(string[] fileNames, string outFileName)
        {
            Save(ImplodeImages(LoadImages(fileNames)), outFileName);
        }

        /// <summary>
        /// Save images to a single tga file
        /// </summary>
        /// <param name="images"></param>
        public void ExportTga(Bitmap[] images, string outFileName)
        {
            Save(ImplodeImages(images), outFileName);
        }

        /// <summary>
        /// Save each image to one tga file
        /// </summary>
        /// <param name="images"></param>
        /// <param name="outFileNames"></param>
        public void ExportTgaMany(Bitmap[] images, string[] outFileNames)
        {
            if (images.Length != outFileNames.Length)
                return;

            // convert each image to tga
            for(var i = 0; i < images.Length; i++)
            {
                Save(Bitmap2Tga(images[i]), outFileNames[i]);
            }
        }

        public Bitmap[] LoadImages(string[] fileNames)
        {
            var bitmaps = new Bitmap[fileNames.Length];
            FIBITMAP dib;

            int i = 0;
            foreach (var f in fileNames)
            {
                if (!File.Exists(f))
                {
                    throw new Exception(f + " does not exist. Aborting.");
                }

                // load image
                dib = new FIBITMAP();
                dib = FreeImage.Load(FREE_IMAGE_FORMAT.FIF_TARGA, f, FREE_IMAGE_LOAD_FLAGS.DEFAULT);

                if (dib.IsNull)
                {
                    throw new Exception("Loading bitmap failed. Aborting.");
                }

                // convert to bitmap
                bitmaps[i] = FreeImage.GetBitmap(dib);
                i++;

                // free resources
                if (!dib.IsNull)
                    FreeImage.Unload(dib);
                // Make sure to set the handle to null so that it is clear that the handle is not pointing to a bitmap.
                dib.SetNull();
            }


            return bitmaps;
        }


        /// <summary>
        /// Stick many images to a single by vertical
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public FIBITMAP ImplodeImages(Bitmap[] images)
        {
            FIBITMAP dib;

            // size of single bitmap
            var width = images[0].Width;
            var height = images[0].Height;
            // size of canvas
            var cwidth = width;
            var cheight = height * images.Length;

            using (var bitmap = new Bitmap(cwidth, cheight))
            {
                using (var canvas = Graphics.FromImage(bitmap))
                {
                    // draw bitmaps on canvas
                    canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    for (int i = 0; i < images.Length; i++)
                    {
                        canvas.DrawImage(images[i], new Rectangle(0, height * i, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
                    }
                    canvas.Save();

                    // convert canvas to freeimage
                    dib = FreeImage.CreateFromBitmap(bitmap);
                }
            }

            return dib;
        }



        /// <summary>
        /// Split an image to many
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public Bitmap[] SplitImage(byte[] data, int chunkHeight)
        {
            var bm = Tga2Bitmap(data);

            // count of small parts to split
            var chunkCount = (int)(bm.Height / chunkHeight);
            var images = new Bitmap[chunkCount];

            // split bitmap to chunks
            for (int i = 0; i < chunkCount; i++)
            {
                var bitmap = new Bitmap(bm.Width, chunkHeight);
                using (var canvas = Graphics.FromImage(bitmap))
                {
                    canvas.DrawImage(bm, new Rectangle(0, 0, bm.Width, chunkHeight), new Rectangle(0, chunkHeight * i, bm.Width, chunkHeight), GraphicsUnit.Pixel);
                    canvas.Save();
                }
                images[i] = bitmap;
            }

            return images;
        }


        /// <summary>
        /// Convert bitmap -> tga
        /// </summary>
        /// <param name="image">image to convert</param>
        /// <returns></returns>
        public FIBITMAP Bitmap2Tga(Bitmap image)
        {
            FIBITMAP dib;
            using (var bitmap = new Bitmap(image.Width, image.Height))
            {
                using (var canvas = Graphics.FromImage(bitmap))
                {
                    // draw bitmap on canvas
                    canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    canvas.DrawImage(image, 0, 0, image.Width, image.Height);
                    canvas.Save();

                    // convert canvas to freeimage
                    dib = FreeImage.CreateFromBitmap(bitmap);
                }
            }
            return dib;
        }

        /// <summary>
        /// Convert tga -> bitmap
        /// </summary>
        /// <param name="data">bytes of tga image file</param>
        /// <returns></returns>
        public Bitmap Tga2Bitmap(byte[] data)
        {
            Bitmap bm;
            using(var ms = new MemoryStream(data))
            { 
                // create targa
                FIBITMAP dib = FreeImage.LoadFromStream(ms, FREE_IMAGE_LOAD_FLAGS.TARGA_LOAD_RGB888);
                if (dib.IsNull)
                    throw new Exception("Targa image is corrupted");

                // convert targa->bitmap
                bm = FreeImage.GetBitmap(dib);
            }
            return bm;
        }


        /// <summary>
        /// Return bytes of tga image
        /// </summary>
        /// <param name="dib"></param>
        /// <returns></returns>
        public byte[] GetBytes(FIBITMAP dib)
        {
            byte[] data;
            using (var ms = new MemoryStream())
            {
                FreeImage.SaveToStream(ref dib, ms, FREE_IMAGE_FORMAT.FIF_TARGA, FREE_IMAGE_SAVE_FLAGS.TARGA_SAVE_RLE, FREE_IMAGE_COLOR_DEPTH.FICD_24_BPP, true);
                data = ms.GetBuffer();
                // FIXME: data allocates always fixed size, so tga file has a lot of null bytes at the end
                data = Helper.RemoveNullBytes(data, true); // remove null bytes (and add one null byte after "TRUEVISION-XFILE." at the end of the tga)
            }
            return data;    
        }

        public void Save(FIBITMAP dib, string fileName)
        {

            // Store the bitmap back to disk
            // TARGA_SAVE_RLE = 2
            FreeImage.Save(FREE_IMAGE_FORMAT.FIF_TARGA, dib, fileName, FREE_IMAGE_SAVE_FLAGS.TARGA_SAVE_RLE);

            // The bitmap was saved to disk but is still allocated in memory, so the handle has to be freed.
            if (!dib.IsNull)
                FreeImage.Unload(dib);

            // Make sure to set the handle to null so that it is clear that the handle is not pointing to a bitmap.
            dib.SetNull();
        }

        public Bitmap Open(string fileName)
        {
            FREE_IMAGE_FORMAT format = FREE_IMAGE_FORMAT.FIF_BMP;
            var ext = Path.GetExtension(fileName);
            switch (ext)
            {
                case ".bmp":
                    format = FREE_IMAGE_FORMAT.FIF_BMP;
                    break;
                case ".ico":
                    format = FREE_IMAGE_FORMAT.FIF_ICO;
                    break;
                case ".jpg":
                case ".jif":
                case ".jpeg":
                case ".jpe":
                    format = FREE_IMAGE_FORMAT.FIF_JPEG;
                    break;
                case ".mng":
                    format = FREE_IMAGE_FORMAT.FIF_MNG;
                    break;
                case ".pcx":
                    format = FREE_IMAGE_FORMAT.FIF_PCX;
                    break;
                case ".png":
                    format = FREE_IMAGE_FORMAT.FIF_PNG;
                    break;
                case ".tga":
                case ".targa":
                    format = FREE_IMAGE_FORMAT.FIF_TARGA;
                    break;
                case ".tif":
                case ".tiff":
                    format = FREE_IMAGE_FORMAT.FIF_TIFF;
                    break;
                case ".psd":
                    format = FREE_IMAGE_FORMAT.FIF_PSD;
                    break;
                case ".gif":
                    format = FREE_IMAGE_FORMAT.FIF_GIF;
                    break;
            }
            
            var bitmap = FreeImage.LoadBitmap(fileName, FREE_IMAGE_LOAD_FLAGS.DEFAULT, ref format);
            return bitmap;
        }



    }
}
