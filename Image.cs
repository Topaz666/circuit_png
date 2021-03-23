using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace circuit_png
{
    public class Pixel
    {
        public int Red { get; set; }
        public int Blue { get; set; }
        public int Yellow { get; set; }
        public int Opacity { get; set; }
    }

    /// <summary>
    /// Define an exchangable image data
    /// </summary>
    public class Image
    {
        private bool _disposed = false;
        #region Properties
        public int Width { get; set; }
        public int Height { get; set; }
        #endregion

        public Image CreateImage(string filename)
        {
            Bitmap myBitmap = new Bitmap(filename);
            throw new NotImplementedException();

        }

        ~Image() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }
    }

}