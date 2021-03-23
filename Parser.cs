using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace circuit_png
{
    public class GernericCoordinate<T> where T : IConvertible
    { 
        private T x1, y1, x2, y2;
        private string val;

        public String getVal()
        {
            return val;
        }
        public T getX1()
        {
            return x1;
        }
        public T getX2()
        {
            return x2;
        }
        public T getY1()
        {
            return y1;
        }
        public T getY2()
        {
            return y2;
        }
        public void ParseCoordiante(string[] text)
        {
            foreach (string subtext in text)
            {

                string[] subs = subtext.Split('=');
                switch (subs[0])
                {
                    case "X1":
                        x1 = (T)Convert.ChangeType(subs[1], typeof(T));
                        break;
                    case "X2":
                        x2 = (T)Convert.ChangeType(subs[1], typeof(T));
                        break;
                    case "Y1":
                        y1 = (T)Convert.ChangeType(subs[1], typeof(T));
                        break;
                    case "Y2":
                        y2 = (T)Convert.ChangeType(subs[1], typeof(T));
                        break;
                    case "R":
                        val = subs[1];
                        break;
                    case "V":
                        val = subs[1];
                        break;
                    case "L":
                        val = subs[1];
                        break;
                    case "F":
                        val = subs[1];
                        break;
                    case "Z":
                        val = subs[1];
                        break;
                    default:
                        break;
                }
            }
            return;
        }

    }
    public class Parser
    {
        #region Properties

        #endregion
        private bool _disposed = false;
        #region Inputs
        /// <summary>
        /// Parse a text
        /// </summary>
        /// <param name="text"></param>
        public string[] ParseLine(string text)
        {
            string[] subs = text.Split(' ');
            return subs;
        }
        /// <summary>
        /// Parse a text file
        /// </summary>
        /// <param name="filePath"></param>
        public string[] ParseFile(string filePath)
        {
            filePath = BaseDomain() + filePath;
            if (System.IO.File.Exists(filePath))
            {
                string[] lines = System.IO.File.ReadAllLines(filePath);
                return lines;
            }
            return null;
        }
        #endregion
        
        #region Outputs

        public string BaseDomain()
        {
            var domaininfo = new AppDomainSetup();
            domaininfo.ApplicationBase = System.Environment.CurrentDirectory;
            AppDomain domain = AppDomain.CreateDomain("Domain2", null, domaininfo);
            return Path.GetFullPath(Path.Combine(domain.BaseDirectory, @"..\..\"));
        }
        /// <summary>
        /// Save image data to a designated file
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveAsFile(string filePath)
        {
            throw new NotImplementedException();
        }
        #endregion
        ~Parser() => Dispose(false);

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
