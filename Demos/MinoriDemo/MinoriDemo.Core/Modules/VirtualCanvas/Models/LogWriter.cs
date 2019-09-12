using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MinoriDemo.Core.Modules.VirtualCanvas.Models
{
    public class LogWriter : IDisposable
    {
        XmlWriter _xw;
        Int32 _indent;

        public LogWriter(TextWriter w)
        {
            XmlWriterSettings s = new XmlWriterSettings
            {
                Indent = true
            };
            _xw = XmlWriter.Create(w, s);
        }

        public Int32 MaxDepth { get; private set; }

        public void Open(String label)
        {
            _xw.WriteStartElement(label);
            _indent++;
            if (_indent > MaxDepth) { MaxDepth = _indent; }

        }
        public void Close()
        {
            _indent--;
            _xw.WriteEndElement();
        }
        public void WriteAttribute(String name, String value) => _xw.WriteAttributeString(name, value);

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing && _xw != null)
            {
                using (_xw)
                {
                    _xw.Flush();
                }
                _xw = null;
            }
        }
        #endregion
    }
}
