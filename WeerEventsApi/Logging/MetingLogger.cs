using System.Text.Json;
using System.Xml.Linq;
using WeerEventsApi.Metingen;

namespace WeerEventsApi.Logging;


public class JsonMetingLoggerDecorator : IMetingLogger
{
    private readonly IMetingLogger _inner;

    public JsonMetingLoggerDecorator(IMetingLogger inner)
    {
        _inner = inner;
    }

    public void Log(Meting meting)
    {
        var opties = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string jsonPad = Path.Combine(AppContext.BaseDirectory, "log.json");
        string json = JsonSerializer.Serialize(meting, opties);
        File.WriteAllText(jsonPad, json);
    }
}

public class XmlMetingLoggerDecorator : IMetingLogger
{
    private readonly IMetingLogger _inner;

    public XmlMetingLoggerDecorator(IMetingLogger inner)
    {
        _inner = inner;
    }

    public void Log(Meting meting)
    {
        XElement xml = new XElement("Meting",
            new XElement("Moment", meting.Moment.ToString("d/M/yyyy HH:mm:ss")),
            new XElement("Waarde", meting.Waarde),
            new XElement("Eenheid", meting.Eenheid)
        );
        string xmlPad = Path.Combine(AppContext.BaseDirectory, "log.xml");
        xml.Save(xmlPad);
    }
}
public class MetingLogger : IMetingLogger
{
    public void Log(Meting meting)
    {
        Console.WriteLine("default metingLogger, meting: " + meting.ToString());
    }
}