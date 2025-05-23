namespace WeerEventsApi.Logging.Factories;

public static class MetingLoggerFactory
{
    public static IMetingLogger Create(bool decorateWithJson = false, bool decorateWithXml = false)
    {
        //TODO Alle combinaties moeten mogelijk zijn (false,false | true,false | false,true | true,true)
        IMetingLogger logger = new MetingLogger();

        if (decorateWithJson)
        {
            logger = new JsonMetingLoggerDecorator(logger);
        }

        if (decorateWithXml)
        {
            logger = new XmlMetingLoggerDecorator(logger);
        }

        return logger;
        //return new MetingLogger();
    }
}