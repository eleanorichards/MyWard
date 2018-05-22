using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

using System.Xml.Serialization;

[System.Serializable]
public class VitalData
{
    [XmlAttribute("name")]
    public string Name;

    public string vitalInfo;
}

//[XmlRoot("VitalCollection")]
//public class VitalContainer
//{
//    [XmlArray("Vitals")]
//    [XmlArrayItem("Vital")]
//    public VitalData[] vitalData;
//}

public class ExportImport : MonoBehaviour
{
    public VitalData[] vitalData;

    private VitalFileManager vitalManager;

    private void Start()
    {
    }

    [MenuItem("Assets/Create/Vital File Manager")]
    public static void CreateMyAsset()
    {
        VitalFileManager asset = ScriptableObject.CreateInstance<VitalFileManager>();
        AssetDatabase.CreateAsset(asset, "Assets/VitalManager.xml");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    //vitalData[0]  //Vital name
    //vitalData[1]  //Vital info
    //vitalData[2]  //Vital min
    //vitalData[3]  //Vital min
    //vitalData[4]  //vital max
    //vitalData[5]  //vital units
    public void SaveVital()
    {
        //vitalManager = Resources.Load("VitalManager.xml") as VitalFileManager;
        //vitalManager = AssetDatabase.LoadAssetAtPath<VitalFileManager>("Assets/VitalManager.asset");
        /*
           TextAsset textAsset = (TextAsset) Resources.Load("MyXMLFile");
           XmlDocument xmldoc = new XmlDocument ();
           xmldoc.LoadXml ( textAsset.text );
        */
        string path = Application.dataPath;
        vitalManager = VitalFileManager.Load(Path.Combine(path, "VitalManager.xml"));
        vitalManager.Vitals = vitalData;
        vitalManager.Save(Path.Combine(path, "VitalManager.xml"));
        print("saved" + path);
    }
}