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

    public Text vitalInfo;
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
    private Text scenarioLoadName;
    public VitalData[] vitalData;

    private Text scenarioName;
    private Text drugName;
    private Text drugDose;

    private Vector2[] drugData;
    private int[,] timeScale;
    public int[,] vitalScale;

    private VitalFileManager vitalManager;

    private void Start()
    {
    }

    [MenuItem("Assets/Create/Vital File Manager")]
    public static void CreateMyAsset()
    {
        VitalFileManager asset = ScriptableObject.CreateInstance<VitalFileManager>();
        AssetDatabase.CreateAsset(asset, "Assets/VitalManager.asset");
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
        vitalManager = AssetDatabase.LoadAssetAtPath<VitalFileManager>("Assets/VitalManager.asset");
        string path = Application.dataPath;
        // vitalManager = Resources.Load("VitalFileManager") as UnityScript;
        vitalManager.Vitals = vitalData;
        vitalManager.Save(path);
        print("saved" + path);
    }
}