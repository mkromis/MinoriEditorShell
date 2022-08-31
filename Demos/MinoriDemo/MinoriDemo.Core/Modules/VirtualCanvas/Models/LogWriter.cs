using System;
using System.IO;
using System.Xml;

namespace MinoriDemo.Core.Modules.VirtualCanvas.Models
{
    public class LogWriter : IDisposable
    {
        private XmlWriter _xw;
        private int _indent;

        public LogWriter(TextWriter w)
        {
            XmlWriterSettings s = new XmlWriterSettings
            {
                Indent = true
            };
            _xw = XmlWriter.Create(w, s);
        }

        public int MaxDepth { get; private set; }

        public void Open(string label)
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

        public void WriteAttribute(string name, string value) => _xw.WriteAttributeString(name, value);

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
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

        #endregion IDisposable Members
    }
}