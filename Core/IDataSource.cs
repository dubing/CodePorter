using System.Xml;

namespace CodePorter.Core
{
    public interface IDataSource
    {
        XmlDocument GetSchema(string baseName);
    }
}

