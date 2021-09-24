using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace CkApp.Web.Models.Utilities
{
    public class Upload : MBase
    {

        public string SaveMappath;

        public string Filename;
        public string FileExtension;
        public string FolderUrl;

        public HttpPostedFileBase File;
        

        public Upload(HttpPostedFileBase file)
        {
            this.File = file;
        }

        public void Init(HttpPostedFileBase _file = null)
        {

            if(_file != null)
                this.File = _file;

            if (File != null)
            {

                _newFileName();

                _getExtension();

                this.FolderUrl = String.Format("/assets/uploads/{0}{1}", this.Filename, this.FileExtension);

            }

        }

        public void Do()
        {
            using (var img = System.Drawing.Image.FromStream(this.File.InputStream))
            {
                SaveToFolder(img, Filename, FileExtension, new Size(800, 700), FolderUrl);
            }
        }

        void _getExtension()
        {
            this.FileExtension = System.IO.Path.GetExtension(File.FileName).ToLower();
        }

        void _newFileName()
        {
            this.Filename = Guid.NewGuid().ToString();
        }

        private void SaveToFolder(Image img, string fileName, string extension, Size newSize, string pathToSave)
        {
            Size imgSize = NewImageSize(img.Size, newSize);
            using (System.Drawing.Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                newImg.Save(SaveMappath, img.RawFormat);
            }
        }

        public Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize;

            return finalSize;
        }

        public void DeleteFile(string file)
        {
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);
        }

    }
}