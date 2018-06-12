using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGen : MonoBehaviour {

    //public float graphWidth;
    //public float graphHeight;

    private LineRenderer xLine;
    private LineRenderer yLine;

    public GameObject graphOrigin;

    public GameObject lineHolder;

    private XMLManager xmlManager;

    private float xNum = 0;
    private float yNum = 0;

    private Canvas graphCanvas;

	// Use this for initialization
	void Start () {
        xmlManager = GameObject.Find("MenuManager").GetComponent<XMLManager>();
        lineHolder = GameObject.Find("LinePrefab");
        graphCanvas = GameObject.Find("GraphCanvas2").GetComponent<Canvas>();
        InitGraphLines();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitGraphLines()
    {
        //xNum = float.Parse(xmlManager.drugMaxDose.text) - float.Parse(xmlManager.drugMinDose.text);
        //yNum = float.Parse(xmlManager.vitalMaxStatus.text) - float.Parse(xmlManager.vitalMinStatus.text);
        xNum = 10;
        yNum = 5;
        Debug.Log(xNum + yNum);

        for(float i = graphCanvas.pixelRect.xMin; i < graphCanvas.pixelRect.xMax; i += (graphCanvas.pixelRect.width/xNum))
        {
            xLine = Instantiate(lineHolder).GetComponent<LineRenderer>();
            xLine.positionCount = 2;
            xLine.SetPosition(0, new Vector3(i, graphCanvas.pixelRect.yMin));
            xLine.SetPosition(0, new Vector3(i, graphCanvas.pixelRect.yMax));
            xLine.startWidth = 0.5f;
            xLine.endWidth = 0.5f;

        }
    }
}
