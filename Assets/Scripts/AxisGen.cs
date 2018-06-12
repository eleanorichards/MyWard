using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisGen : MonoBehaviour
{
    public enum TimeFormat
    {
        HOURS,
        MINUTES,
        SECONDS
    }

    public TimeFormat timeFormat;

    public float XShift;
    public float YShift;

    [Header("Y Axis")]
    public string vitalName;

    public int smallMarkerNum;
    public float maxVitalRate;

    [Header("X Axis")]
    public int maxTime; //in minutes

    [Header("Graph Size")]
    public Vector3 xEndPos;

    public Vector3 yEndPos;

    private LineRenderer xAxis;
    private LineRenderer yAxis;

    private GameObject xSmallMarker;
    private GameObject xLargeMarker;

    private GameObject ySmallMarker;
    private GameObject yLargeMarker;

    private GameObject graphHolder;
    private GameObject canvas;
    private GameObject textBox;
    private Text xAxisLabel;
    //private Text yAxisLabel;

    private float label_spacing = 0;
    [Tooltip("difference between large lines")]
    private float largeMarkerPos = 0.0f;
    [Tooltip("difference between small lines")]
    private float smallMarkerPos = 0.0f;
    private float smallMarkerProgression = 0.0f;

    private bool smallMarkersSet = false;

    public Vector3 origin = Vector3.zero;

    // Use this for initialization
    private void Start()
    {
        //INITIALISATION
        graphHolder = GameObject.Find("Axis");
        canvas = GameObject.Find("GraphCanvas");
        xSmallMarker = Resources.Load("XSmallLabel") as GameObject;
        xLargeMarker = Resources.Load("XBigLabel") as GameObject;
        ySmallMarker = Resources.Load("YSmallLabel") as GameObject;
        yLargeMarker = Resources.Load("YBigLabel") as GameObject;
        textBox = Resources.Load("TextBox") as GameObject;
        xAxisLabel = GameObject.Find("XLabel").GetComponent<Text>();
        // yAxisLabel = GameObject.Find("YLabel").GetComponent<Text>();
        origin = graphHolder.transform.position;

        //INITIALISATION
        SetTimeScale();
        DrawXMarkerLines();
        DrawYMarkerLines();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            foreach (Transform child in graphHolder.transform)
            {
                Destroy(child.gameObject);
            }
            SetTimeScale();
            DrawXMarkerLines();
            //SetYScale();
            DrawYMarkerLines();
        }
    }

    private void SetTimeScale()
    {
        switch (timeFormat)
        {
            case TimeFormat.HOURS:
                xAxisLabel.text = "Time (hrs)";
                label_spacing = (maxTime * 60) / 10;
                break;

            case TimeFormat.MINUTES:
                xAxisLabel.text = "Time (mins)";
                label_spacing = (maxTime * 60) / 10;
                break;

            case TimeFormat.SECONDS:
                xAxisLabel.text = "Time (secs)";
                label_spacing = maxTime / 100;

                break;

            default:
                break;
        }
    }

    private void DrawXMarkerLines()
    {
        int hour = 0;
        largeMarkerPos = XShift;
        smallMarkersSet = false;
        smallMarkerProgression = 0.0f;

        for (int a = 0; a <= label_spacing; a += 6) //break in to hour intervals
        {
            smallMarkerPos = largeMarkerPos;

            GameObject curTimeLabel = Instantiate(xLargeMarker, new Vector3(largeMarkerPos, XShift, 0.0f), Quaternion.identity/*, graphHolder.transform*/);
            GameObject largerMarkerLabel = Instantiate(textBox, new Vector3(largeMarkerPos, XShift - 0.7f, 0.0f), Quaternion.identity/*, graphHolder.transform*/);

            largerMarkerLabel.GetComponent<Text>().text = hour.ToString();
            largerMarkerLabel.transform.SetParent(graphHolder.transform);


            largeMarkerPos += xEndPos.x / maxTime;

            if (!smallMarkersSet) //to ensure the gap starts at 0 each time
            {
                smallMarkersSet = true;
                smallMarkerProgression = largeMarkerPos / 6.0f; //SETTING SMALL X INCREASE
            }
            if (a < label_spacing)
            {
                for (int b = 0; b < 6; b++) //break in to 10's of minutes
                {
                    GameObject curSmallLabel = Instantiate(xSmallMarker, new Vector3(smallMarkerPos, XShift, 0.0f), Quaternion.identity/*, graphHolder.transform*/);
                    if (b == 3)
                    {
                        GameObject smallMarkerLabel = Instantiate(textBox, new Vector3(smallMarkerPos, XShift - 0.5f, 0.0f), Quaternion.identity/*, graphHolder.transform*/);
                        Text tempText = smallMarkerLabel.GetComponent<Text>();
                        tempText.fontSize = 12;
                        tempText.text = hour.ToString() + ":30";
                        smallMarkerLabel.transform.SetParent(graphHolder.transform);
                    }
                    //add marker label
                    smallMarkerPos += smallMarkerProgression;
                    curSmallLabel.transform.SetParent(graphHolder.transform);
                }
            }
            curTimeLabel.transform.SetParent(graphHolder.transform);

            hour++;
        }
    }

    private void DrawYMarkerLines()
    {
        int yScaleVal = 0;
        largeMarkerPos = YShift;
        smallMarkersSet = false;
        smallMarkerProgression = 0.0f;
        label_spacing = (maxVitalRate * 100) / 10; //WITH the * X

        for (int a = 0; a <= label_spacing; a += 10) //break in to intervals //this number neds to CORRESPOND
        {
            smallMarkerPos = largeMarkerPos;
            GameObject curTimeLabel = Instantiate(yLargeMarker, new Vector3(XShift, largeMarkerPos, 0.0f), Quaternion.identity);
            GameObject largerMarkerLabel = Instantiate(textBox, new Vector3(XShift - 0.6f, largeMarkerPos, 0.0f), Quaternion.identity);
            largerMarkerLabel.GetComponent<Text>().text = yScaleVal.ToString();
            largerMarkerLabel.transform.SetParent(graphHolder.transform);

            largeMarkerPos += yEndPos.y / maxVitalRate;

            if (!smallMarkersSet) //to ensure the gap starts at 0 each time
            {
                smallMarkersSet = true;
                smallMarkerProgression = largeMarkerPos / smallMarkerNum; //SETTING SMALL X INCREASE (10 per gap)
            }
            if (a < label_spacing)
            {
                for (int b = 0; b < smallMarkerNum; b++) //break in to 10's of minutes
                {
                    GameObject curSmallLabel = Instantiate(ySmallMarker, new Vector3(0.0f, smallMarkerPos, 0.0f), Quaternion.identity);
                    if (smallMarkerNum % 2 == 1)
                    {
                        if (b == (smallMarkerNum / 2) + 1)
                        {
                            GameObject smallMarkerLabel = Instantiate(textBox, new Vector3(-0.6f, smallMarkerPos - (smallMarkerProgression / 2), 0.0f), Quaternion.identity);
                            Text tempText = smallMarkerLabel.GetComponent<Text>();
                            tempText.fontSize = 12;
                            tempText.text = yScaleVal.ToString() + ".50";
                            smallMarkerLabel.transform.SetParent(graphHolder.transform);
                        }
                    }
                    else
                    {
                        if (b == smallMarkerNum / 2)
                        {
                            GameObject smallMarkerLabel = Instantiate(textBox, new Vector3(-0.6f, smallMarkerPos, 0.0f), Quaternion.identity);
                            Text tempText = smallMarkerLabel.GetComponent<Text>();
                            tempText.fontSize = 12;
                            tempText.text = yScaleVal.ToString() + ".50";
                            smallMarkerLabel.transform.SetParent(graphHolder.transform);
                        }
                    }
                    smallMarkerPos += smallMarkerProgression;
                    curSmallLabel.transform.SetParent(graphHolder.transform);
                }
            }
            curTimeLabel.transform.SetParent(graphHolder.transform);

            yScaleVal++;
        }
    }

    private void SetYScale()
    {
        //yAxisLabel.text = vitalName;
    }

    public Vector2 ReturnDimensions()
    {
        return (new Vector2(maxTime, maxVitalRate));
    }

    public Vector2 ReturnScale()
    {
        return new Vector2(xEndPos.x, yEndPos.y);
    }
}