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

    public enum Vital
    {
        LUNGS,
        KIDNEY,
        HEART
    }

    public Vital vital;

    [Header("Y Axis")]
    public string vitalName;

    public int smallMarkerNum;
    public int maxVitalRate;

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
    private GameObject yLargerMarker;

    private GameObject graphHolder;
    private GameObject canvas;
    private GameObject textBox;
    private Text xAxisLabel;
    private Text yAxisLabel;

    private int number_spacing = 0;
    private int label_spacing = 0;
    private float largeMarkerPos = 0.0f;
    private float smallMarkerPos = 0.0f;
    private float smallMarkerProgression = 0.0f;

    private bool smallMarkersSet = false;

    public Vector3 origin = Vector3.zero;

    // Use this for initialization
    private void Start()
    {
        //INITIALISATION
        graphHolder = GameObject.Find("Axis");
        xAxis = GameObject.Find("xAxis").GetComponent<LineRenderer>();
        yAxis = GameObject.Find("yAxis").GetComponent<LineRenderer>();
        canvas = GameObject.Find("GraphCanvas");
        xSmallMarker = Resources.Load("XSmallLabel") as GameObject;
        xLargeMarker = Resources.Load("XBigLabel") as GameObject;
        ySmallMarker = Resources.Load("YSmallLabel") as GameObject;
        yLargerMarker = Resources.Load("YBigLabel") as GameObject;
        textBox = Resources.Load("TextBox") as GameObject;
        xAxisLabel = GameObject.Find("XLabel").GetComponent<Text>();
        yAxisLabel = GameObject.Find("YLabel").GetComponent<Text>();
        origin = graphHolder.transform.position;

        //INITIALISATION
        SetTimeScale();
        DrawXMarkerLines();
        DrawYMarkerLines();
        DrawAxis();
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
            DrawAxis();
            SetYScale();
            DrawYMarkerLines();
        }
    }

    private void DrawAxis()
    {
        xAxis.SetPosition(0, origin);
        yAxis.SetPosition(0, origin);
        yAxis.SetPosition(yAxis.positionCount - 1, yEndPos);
        xAxis.SetPosition(xAxis.positionCount - 1, xEndPos);
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
                number_spacing = maxTime / 100;

                break;

            default:
                break;
        }

        xAxis.positionCount = 2;
    }

    private void DrawXMarkerLines()
    {
        int hour = 0;
        largeMarkerPos = 0.0f;
        smallMarkersSet = false;
        smallMarkerProgression = 0.0f;

        for (int a = 0; a <= label_spacing; a += 6) //break in to hour intervals
        {
            smallMarkerPos = largeMarkerPos;
            GameObject curTimeLabel = Instantiate(xLargeMarker, new Vector3(largeMarkerPos, 0.0f, 0.0f), Quaternion.identity);
            GameObject largerMarkerLabel = Instantiate(textBox, new Vector3(largeMarkerPos, -0.7f, 0.0f), Quaternion.identity);
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
                    GameObject curSmallLabel = Instantiate(xSmallMarker, new Vector3(smallMarkerPos, 0.0f, 0.0f), Quaternion.identity);
                    if (b == 3)
                    {
                        GameObject smallMarkerLabel = Instantiate(textBox, new Vector3(smallMarkerPos, -0.5f, 0.0f), Quaternion.identity);
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
        largeMarkerPos = 0.0f;
        smallMarkersSet = false;
        smallMarkerProgression = 0.0f;
        label_spacing = (maxVitalRate * 100) / 10; //WITH the * X

        for (int a = 0; a <= label_spacing; a += 10) //break in to intervals //this number neds to CORRESPOND
        {
            smallMarkerPos = largeMarkerPos;
            GameObject curTimeLabel = Instantiate(yLargerMarker, new Vector3(0.0f, largeMarkerPos, 0.0f), Quaternion.identity);
            GameObject largerMarkerLabel = Instantiate(textBox, new Vector3(-0.6f, largeMarkerPos, 0.0f), Quaternion.identity);
            largerMarkerLabel.GetComponent<Text>().text = yScaleVal.ToString();
            largerMarkerLabel.transform.SetParent(graphHolder.transform); largeMarkerPos += yEndPos.y / maxVitalRate;

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
                    if (b == 3)
                    {
                        GameObject smallMarkerLabel = Instantiate(textBox, new Vector3(-0.5f, smallMarkerPos, 0.0f), Quaternion.identity);
                        Text tempText = smallMarkerLabel.GetComponent<Text>();
                        tempText.fontSize = 12;
                        tempText.text = yScaleVal.ToString() + ".50";
                        smallMarkerLabel.transform.SetParent(graphHolder.transform);
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
        yAxisLabel.text = vitalName;
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