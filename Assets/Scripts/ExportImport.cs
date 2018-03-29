using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

public class ExportImport : MonoBehaviour
{
    private Text scenarioLoadName;
    private Text scenarioName;
    private Text drugName;
    private Text vitalName;
    private Text drugDose;

    private Vector2[] drugData;
    private int[,] timeScale;
    private int[,] vitalScale;

    private void Start()
    {
    }

    public void ExportDrug()
    {
        FileStream file;
        string destination = Application.persistentDataPath + "/";
        string fileName = destination + scenarioName.text;

        if (File.Exists(fileName))
        {
            Debug.Log(fileName + " already exists.");
            return;
        }
        else if (fileName == "")
        {
            Debug.Log("Please enter a file name");
            return;
        }

        file = File.Create(fileName);

        GraphData data = new GraphData(drugData, float.Parse(drugDose.text), timeScale, vitalScale, vitalName.text, drugName.text);

        var serializer = new XmlSerializer(typeof(GraphData));
        var stream = new FileStream(fileName, FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/";
        string fileName = destination + scenarioLoadName.text;

        var serializer = new XmlSerializer(typeof(GraphData));
        var stream = new FileStream(fileName, FileMode.Open);
        var container = serializer.Deserialize(stream) as GraphData;
        stream.Close();
    }
}