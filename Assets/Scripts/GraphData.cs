﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;

[XmlRoot("GraphData")]
public class GraphData : MonoBehaviour
{
    [XmlAttribute("Name")]
    private string drugName;

    [XmlAttribute("Vital Name")]
    private string vitalName;

    [XmlArray("Drug Points")]
    private Vector2[] drugData;

    [XmlArray("MinMaxTime")]
    private int[,] timeScale = new int[1, 1];

    [XmlArray("MinMaxTime2")]
    private int[,] vitalScale = new int[1, 1];

    [XmlElement("dosage")]
    private float drugDose;

    /// <summary>
    ///
    /// </summary>
    /// <param name="_drugData">points plotted</param>
    /// <param name="_drugDose">drug dose associated</param>
    /// <param name="_timeScale">Max and min times</param>
    /// <param name="_vitalScale">Max and min vital scale</param>
    /// <param name="_vitalName">name of current vital</param>
    /// <param name="_drugName">drug name</param>
    public GraphData(Vector2[] _drugData, float _drugDose, int[,] _timeScale, int[,] _vitalScale, string _vitalName, string _drugName)
    {
        drugData = _drugData;
        drugDose = _drugDose;
        timeScale = _timeScale;
        vitalScale = _vitalScale;
        vitalName = _vitalName;
        drugName = _drugName;
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}