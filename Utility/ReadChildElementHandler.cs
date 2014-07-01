using System.Xml;

namespace CodePorter.Utility
{
    public delegate void ReadChildElementHandler(XmlReader reader, params object[] args);
}

