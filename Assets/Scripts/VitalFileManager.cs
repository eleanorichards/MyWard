using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

//[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
[XmlRoot("VitalObjectContainer")]
public class VitalFileManager : ScriptableObject
{
    [XmlElement("Vital")]
    public VitalData[] Vitals;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(VitalFileManager));
        //System.IO.StringReader stringReader = new System.IO.StringReader(textAsset.text);
        //stringReader.Read(); // skip BOM
        //System.Xml.XmlReader reader = System.Xml.XmlReader.Create(stringReader);
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
            using (StreamReader reader = new StreamReader(path, new System.Text.UTF8Encoding(false)))
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