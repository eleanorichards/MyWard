using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using UnityEngine.UI;

/// <summary>
/// Taken and adaptedfrom the Unify Community Wiki
/// http://wiki.unity3d.com/index.php/Save_and_Load_from_XML
/// </summary>
public class XMLManager : MonoBehaviour
{
    // An example where the encoding can be found is at
    // http://www.eggheadcafe.com/articles/system.xml.xmlserialization.asp
    // We will just use the KISS method and cheat a little and use
    // the examples from the web page since they are fully described

    // This is our local private members
    private Rect _Save, _Load, _SaveMSG, _LoadMSG;

    private bool _ShouldSave, _ShouldLoad, _SwitchSave, _SwitchLoad;
    private string _FileLocation, _FileName;

    //public GameObject _Player;
    private UserData myData;

    private string _data;

    //Vital Info
    [Header("Vital Info")]
    public Text vitalName;

    public Text vitalInfo;

    public Text vitalMinStatus;
    public Text vitalMaxStatus;
    public Text vitalUnits;

    [Header("Drug Info")]
    public Text drugName;

    public Text drugInfo;
    public Text drugMinDose;
    public Text drugMaxDose;
    public Text drugUnits;

    private void Start()
    {
        // Where we want to save and load to and from - persistant data path at Assets/...
        _FileLocation = Application.dataPath;
        _FileName = "SaveData.xml";

        // we need soemthing to store the information into
        myData = new UserData();
    }

    public void SaveVitals()
    {
        //Get vital Info from GUI

        myData._vitalDat.name = vitalName.text;
        myData._vitalDat.info = vitalInfo.text;
        myData._vitalDat.minStatus = vitalMinStatus.text;
        myData._vitalDat.maxStatus = vitalMaxStatus.text;
        myData._vitalDat.units = vitalUnits.text;

        // serialize UserData here, to empty string
        _data = SerializeObject(myData);
        // This is the final resulting XML from the serialization process
        CreateXML();
        Debug.Log(_data);
    }

    public void SaveDrug()
    {
        //Get vital Info from GUI

        myData._drugDat.name = drugName.text;
        myData._drugDat.info = drugInfo.text;
        myData._drugDat.minDose = drugMinDose.text;
        myData._drugDat.maxDose = drugMaxDose.text;
        myData._drugDat.units = drugUnits.text;
        // serialize UserData here, to empty string
        _data = SerializeObject(myData);
        // This is the final resulting XML from the serialization process
        CreateXML();
        Debug.Log(_data);
    }

    public void LoadVitals()
    {
        LoadXML();
        if (_data.ToString() != "")
        {
            // notice how I use a reference UserData here, deserialize from saved string
            myData = (UserData)DeserializeObject(_data);

            Debug.Log(myData._vitalDat.name);
        }
    }

    //private void OnGUI()
    //{
    //    ***************************************************
    //     Loading The Info...
    //     **************************************************
    //    if (GUI.Button(_Load, "Load"))
    //    {
    //        GUI.Label(_LoadMSG, "Loading from: " + _FileLocation);
    //        // Load our UserData into myData
    //        LoadXML();
    //        if (_data.ToString() != "")
    //        {
    //            // notice how I use a reference UserData here, deserialize from saved string
    //            myData = (UserData)DeserializeObject(_data);

    //            Debug.Log(myData._vitalDat.name);
    //        }
    //    }

    //    ***************************************************
    //     Saving The Info...
    //     **************************************************
    //    if (GUI.Button(_Save, "Save"))
    //    {
    //        GUI.Label(_SaveMSG, "Saving to: " + _FileLocation);
    //        //Get vital Info from GUI
    //        myData._vitalDat.name = vitalName.text;
    //        myData._vitalDat.info = vitalInfo.text;

    //        // serialize UserData here, to empty string
    //        _data = SerializeObject(myData);
    //        // This is the final resulting XML from the serialization process
    //        CreateXML();
    //        Debug.Log(_data);
    //    }
    //}

    /* The following methods came from the referenced URL
     * without these you get: "... encoding whitespace line 0 space 1 etc... error */

    private string UTF8ByteArrayToString(byte[] characters)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }

    private byte[] StringToUTF8ByteArray(string pXmlString)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }

    // Here we serialize our UserData object of myData
    private string SerializeObject(object pObject)
    {
        string XmlizedString = null;
        MemoryStream memoryStream = new MemoryStream();
        XmlSerializer xs = new XmlSerializer(typeof(UserData));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        xs.Serialize(xmlTextWriter, pObject);
        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        return XmlizedString;
    }

    // Here we deserialize it back into its original form
    private object DeserializeObject(string pXmlizedString)
    {
        XmlSerializer xs = new XmlSerializer(typeof(UserData));
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return xs.Deserialize(memoryStream);
    }

    // Finally our save and load methods for the file itself
    private void CreateXML()
    {
        //need to load current file & append
        StreamWriter writer;
        FileInfo t = new FileInfo(_FileLocation + "\\" + _FileName);
        if (!t.Exists)
        {
            writer = t.CreateText();
        }
        else
        {
            //t.Delete();
            writer = t.CreateText();
        }
        writer.Write(_data);
        writer.Close();
        Debug.Log("File written.");
    }

    private void LoadXML()
    {
        StreamReader r = File.OpenText(_FileLocation + "\\" + _FileName);
        string _info = r.ReadToEnd();
        r.Close();
        _data = _info;
        Debug.Log("File Read");
    }
}

// UserData is our custom class that holds our defined objects we want to store in XML format
public class UserData
{
    // We have to define a default instance of the structure
    public VitalData _vitalDat;

    public DrugData _drugDat;

    // Default constructor doesn't really do anything at the moment
    public UserData() { }

    // Anything we want to store in the XML file, we define it here
    public struct VitalData
    {
        public string name;
        public string info;
        public string minStatus;
        public string maxStatus;
        public string units;
    }

    public struct DrugData
    {
        public string name;
        public string info;
        public string minDose;
        public string maxDose;
        public string vitalAffected;
        public string units;
    }
}