using System;
using System.IO;
using System.Xml.Serialization;

public static class XmlDeserializer
{
    public static T Deserialize<T>(string xml) where T : new()
    {
        T obj = new T();
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(xml);
        obj = (T)serializer.Deserialize(reader);
        return obj;
    }
}

