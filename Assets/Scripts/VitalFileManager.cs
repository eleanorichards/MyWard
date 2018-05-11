using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
public class VitalFileManager : ScriptableObject
{
    [XmlArray("Vitals"), XmlArrayItem("Vital")]
    public VitalData[] Vitals;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(VitalFileManager));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static VitalFileManager Load(string path)
    {
        var serializer = new XmlSerializer(typeof(VitalFileManager));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as VitalFileManager;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static VitalFileManager LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(VitalFileManager));
        return serializer.Deserialize(new StringReader(text)) as VitalFileManager;
    }
}