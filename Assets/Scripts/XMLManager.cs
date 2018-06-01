using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using UnityEngine.UI;
using System.Collections.Generic;

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

    private string _FileLocation;

    //public GameObject _Player;
    private VitalContainer vitalContainerData;

    private DrugContainer drugContainerData;

    private string _vData;
    private string _dData;

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

    private UIManager uiManager;

    private void Start()
    {
        // Where we want to save and load to and from - persistant data path at Assets/...
        _FileLocation = Application.dataPath;
        //_FileName = "SaveData.xml";

        // we need soemthing to store the information into
        vitalContainerData = new VitalContainer();
        drugContainerData = new DrugContainer();
        uiManager = GetComponent<UIManager>();
    }

    public void SaveVitals()
    {
        LoadVitals();
        //Get vital Info from GUI
        VitalContainer.VitalData _tempDat = new VitalContainer.VitalData();

        _tempDat.name = "\n" + vitalName.text;
        _tempDat.info = vitalInfo.text;
        _tempDat.minStatus = vitalMinStatus.text;
        _tempDat.maxStatus = vitalMaxStatus.text;
        _tempDat.units = vitalUnits.text + "\n";

        vitalContainerData._vitalDat.Add(_tempDat);

        // serialize UserData here, to empty string
        _vData = SerializeObject(vitalContainerData, "VitalData.xml");
        // This is the final resulting XML from the serialization process
        CreateXML("VitalData.xml", _vData);
        PopulateVitalDD();
        Debug.Log(_vData);
    }

    public void PopulateVitalDD()
    {
        if (!uiManager)
            uiManager = GetComponent<UIManager>();
        LoadVitals();
        uiManager.UpdateVitalSV(vitalContainerData);
    }

    public void PopulateVitalFields()
    {
        if (!uiManager)
            uiManager = GetComponent<UIManager>();
        LoadVitals();
        uiManager.UpdateVitalInputFields(vitalContainerData);
    }

    public void PopulateDrugDD()
    {
        if (!uiManager)
            uiManager = GetComponent<UIManager>();
        LoadDrugs();
        uiManager.UpdateDrugSV(drugContainerData);
    }

    public void PopulateDrugFields()
    {
        if (!uiManager)
            uiManager = GetComponent<UIManager>();
        LoadDrugs();
        uiManager.UpdateDrugInputFields(drugContainerData);
    }

    public void SaveDrug()
    {
        LoadDrugs();
        DrugContainer.DrugData _tempDat = new DrugContainer.DrugData();

        _tempDat.name = "\n" + drugName.text;
        _tempDat.info = drugInfo.text;
        _tempDat.minDose = drugMinDose.text;
        _tempDat.maxDose = drugMaxDose.text;
        _tempDat.units = drugUnits.text + "\n";

        drugContainerData._drugDat.Add(_tempDat);

        // serialize UserData here, to empty string
        _dData = SerializeObject(drugContainerData, "DrugData.xml");
        // This is the final resulting XML from the serialization process
        CreateXML("DrugData.xml", _dData);
        PopulateDrugDD();
        Debug.Log(_dData);
    }

    public void LoadVitals()
    {
        LoadXML("VitalData.xml", _vData);
        if (_vData.ToString() != "")
        {
            //use a referenced UserData here, deserialize from saved string
            vitalContainerData = (VitalContainer)DeserializeObject(_vData, "VitalData.xml");
        }
    }

    public void LoadDrugs()
    {
        LoadXML("DrugData.xml", _dData);
        if (_dData.ToString() != "")
        {
            //use a referenced UserData here, deserialize from saved string
            drugContainerData = (DrugContainer)DeserializeObject(_dData, "DrugData.xml");

            Debug.Log(drugContainerData._drugDat.Count);
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
    private string SerializeObject(object pObject, string filename)
    {
        string XmlizedString = null;
        XmlSerializer xs = new XmlSerializer(typeof(VitalContainer)); //SET TO DEFAULT
        MemoryStream memoryStream = new MemoryStream();
        if (filename == "DrugData.xml")
        {
            xs = new XmlSerializer(typeof(DrugContainer));
        }
        else if (filename == "VitalData.xml")
        {
            xs = new XmlSerializer(typeof(VitalContainer));
        }

        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        xs.Serialize(xmlTextWriter, pObject);
        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());

        return XmlizedString;
    }

    // Here we deserialize it back into its original form
    private object DeserializeObject(string pXmlizedString, string filename)
    {
        XmlSerializer xs = new XmlSerializer(typeof(VitalContainer)); //SET TO DEFAULT
        if (filename == "DrugData.xml")
        {
            xs = new XmlSerializer(typeof(DrugContainer));
        }
        else if (filename == "VitalData.xml")
        {
            xs = new XmlSerializer(typeof(VitalContainer));
        }
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return xs.Deserialize(memoryStream);
    }

    // Finally our save and load methods for the file itself
    private void CreateXML(string _fileName, string _data)
    {
        //need to load current file & append
        StreamWriter writer;
        FileInfo t = new FileInfo(_FileLocation + "\\" + _fileName);
        if (!t.Exists)
        {
            writer = t.CreateText();
        }
        else
        {
            t.Delete();
            writer = t.CreateText();
        }
        writer.Write(_data);
        writer.Close();
        Debug.Log("File written.");
    }

    private void LoadXML(string _fileName, string _data)
    {
        if (_FileLocation == null)
            _FileLocation = Application.dataPath;
        StreamReader r = File.OpenText(_FileLocation + "\\" + _fileName);
        // string info = r.ReadBlock('a', 2, 3);
        string _info = r.ReadToEnd();
        r.Close();
        //needs to be vData or dData
        _data = _info;
        SetData(_fileName, _data);
        Debug.Log("File Read");
    }

    private void SetData(string filename, string data)
    {
        switch (filename)
        {
            case "DrugData.xml":
                _dData = data;
                break;

            case "VitalData.xml":
                _vData = data;
                break;

            default:
                break;
        }
    }
}

// UserData is our custom class that holds our defined objects we want to store in XML format
public class VitalContainer
{
    // We have to define a default instance of the structure
    public List<VitalData> _vitalDat = new List<VitalData>();

    // Default constructor doesn't really do anything at the moment
    public VitalContainer() { }

    // Anything we want to store in the XML file, we define it here
    [XmlRoot("VitalData")]
    public struct VitalData
    {
        public string name;
        public string info;
        public string minStatus;
        public string maxStatus;
        public string units;
    }
}

public class DrugContainer
{
    public List<DrugData> _drugDat = new List<DrugData>();

    // Default constructor doesn't really do anything at the moment
    public DrugContainer() { }

    // Anything we want to store in the XML file, we define it here

    [XmlRoot("DrugData")]
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