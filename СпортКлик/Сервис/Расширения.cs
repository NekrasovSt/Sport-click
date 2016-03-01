using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace СпортКлик.Сервис
{
    public static class РасширенияМодели
    {
        // Изменение размеров картинки
        public static Image Resize(this Image img, int iMaxHeight, int iMaxWidth)
        {
            int iDestHeight = 0;
            int iDestWidth = 0;

            // Определяем новые размеры картинки.
            // Если она меньше максимального размера, то оставляем её без изменения.
            // Если хотя бы по одному измерению больше максимального размера,
            // то уменьшаем её пропорционально до максимального размера.
            if ((iMaxHeight == 0 || iMaxHeight >= img.Height) && (iMaxWidth == 0 || iMaxWidth >= img.Width)) return img;
            else
            {
                if (iMaxHeight == 0 && iMaxWidth > 0)
                {
                    iDestWidth = iMaxWidth;
                    iDestHeight = img.Height * iMaxWidth / img.Width;
                }

                if (iMaxHeight > 0 && iMaxWidth == 0)
                {
                    iDestHeight = iMaxHeight;
                    iDestWidth = img.Width * iMaxHeight / img.Height;
                }

                if (iMaxHeight > 0 && iMaxWidth > 0)
                {
                    iDestWidth = iMaxWidth;
                    iDestHeight = img.Height * iMaxWidth / img.Width;

                    if (iDestHeight > iMaxHeight)
                    {
                        iDestHeight = iMaxHeight;
                        iDestWidth = img.Width * iMaxHeight / img.Height;
                    }
                }

                return new Bitmap(img, new Size(iDestWidth, iDestHeight));
            }
        }

        // Сохранение картинки на диск одновременно с изменением размеров
        public static void ResizeAndSave(this HttpPostedFileBase imagefile, int iMaxHeight, int iMaxWidth, string strSavePath)
        {
            if (imagefile != null)
            {
                ImageFormat format = ImageFormat.Bmp;
                string strExtension = Path.GetExtension(strSavePath);
                switch (strExtension.ToLower())
                {
                    case ".gif":
                        format = ImageFormat.Gif;
                        break;

                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;

                    case ".jpeg":
                        format = ImageFormat.Jpeg;
                        break;

                    case ".png":
                        format = ImageFormat.Png;
                        break;
                }
                Image.FromStream(imagefile.InputStream).Resize(iMaxHeight, iMaxWidth).Save(strSavePath, format);
            }
        }
    }
    public static class РасширенияВывода
    {
        public static MvcHtmlString ВывестиКакРазметку(this string разметка)
        {
            return new MvcHtmlString(разметка);
        }
    }
}