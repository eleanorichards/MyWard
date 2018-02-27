using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisGen : MonoBehaviour
{
    public enum TimeFormat
    {
        DAYS,
        HOURS,
        MINUTES,
        SECONDS
    }

    public TimeFormat timeFormat;

    public LineRenderer xAxis;
    public LineRenderer yAxis;

    public int maxTime; //in minutes

    public GameObject smallLabel;
    public GameObject timeLabel;

    private List<GameObject> smallLabels = new List<GameObject>();
    private List<GameObject> timeLabels = new List<GameObject>();
    private Text xAxisLabel;
    private Text yAxisLabel;
    private int number_spacing = 0;
    private int label_spacing = 0;
    private float largeMarkerPos = 0.0f;
    private float smallMarkerPos = 0.0f;
    private float smallMarkerProgression = 0.0f;

    private bool smallMarkersSet = false;

    private Vector3 origin = Vector3.zero;

    public Vector3 xEndPos;
    public Vector3 yEndPos;

    // Use this for initialization
    private void Start()
    {
        xAxisLabel = GameObject.Find("XLabel").GetComponent<Text>();
        yAxisLabel = GameObject.Find("YLabel").GetComponent<Text>();
        SetTimeScale();
        DrawMarkerLines();
        DrawAxis();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            foreach (GameObject marker in timeLabels)
            {
                timeLabels.Remove(marker);
                Destroy(marker);
            }
            SetTimeScale();
            DrawMarkerLines();
            DrawAxis();
        }
    }

    private void SetTimeScale()
    {
        switch (timeFormat)
        {
            case TimeFormat.DAYS:
                number_spacing = maxTime / 24;
                break;

            case TimeFormat.HOURS:
                xAxisLabel.text = "Time (hrs)";
                number_spacing = maxTime;
                label_spacing = (number_spacing * 60) / 10;
                break;

            case TimeFormat.MINUTES:
                xAxisLabel.text = "Time (mins)";
                number_spacing = maxTime / 60;
                label_spacing = (number_spacing * 60) / 10;
                break;

            case TimeFormat.SECONDS:
                number_spacing = maxTime / 60;

                break;

            default:
                break;
        }

        xAxis.positionCount = 2;
    }

    private void DrawAxis()
    {
        xAxis.SetPosition(0, origin);
        yAxis.SetPosition(0, origin);
        yAxis.SetPosition(yAxis.positionCount - 1, yEndPos);
        xAxis.SetPosition(xAxis.positionCount - 1, xEndPos);
    }

    private void DrawMarkerLines()
    {
        switch (timeFormat)
        {
            case TimeFormat.DAYS:
                break;

            case TimeFormat.HOURS:
                int hour = 0;
                largeMarkerPos = 0.0f;
                smallMarkersSet = false;
                smallMarkerProgression = 0.0f;

                for (int a = 0; a <= label_spacing; a += 6) //break in to hour intervals
                {
                    smallMarkerPos = largeMarkerPos;
                    GameObject curTimeLabel = Instantiate(timeLabel, new Vector3(largeMarkerPos, 0.0f, 0.0f), Quaternion.identity);
                    timeLabels.Add(curTimeLabel);
                    curTimeLabel.GetComponentInChildren<Text>().text = hour.ToString();
                    largeMarkerPos += xEndPos.x / maxTime;

                    if (!smallMarkersSet)
                    {
                        smallMarkersSet = true;
                        smallMarkerProgression = largeMarkerPos / 6.0f; //SETTING SMALL X INCREASE
                    }

                    for (int b = 0; b < 6; b++) //break in to 10's of minutes
                    {
                        GameObject curSmallLabel = Instantiate(smallLabel, new Vector3(smallMarkerPos, 0.0f, 0.0f), Quaternion.identity);
                        smallLabels.Add(curSmallLabel);
                        //add marker label
                        smallMarkerPos += smallMarkerProgression;
                    }
                    hour++;
                }
                break;

            case TimeFormat.MINUTES:
                int minute = 0;
                largeMarkerPos = 0.0f;

                for (int a = 0; a <= label_spacing; a += 6) //break in to hour intervals
                {
                    for (int b = 0; b < 6; b++) //break in to 10's of minutes
                    {
                        //add marker label
                    }
                    //xAxis.SetPosition(i, new Vector3(xProgression, 0, 0));
                    GameObject curTimeLabel = Instantiate(timeLabel, new Vector3(largeMarkerPos, 0.0f, 0.0f), Quaternion.identity);
                    timeLabels.Add(curTimeLabel);
                    curTimeLabel.GetComponentInChildren<Text>().text = minute.ToString();
                    largeMarkerPos += xEndPos.x / maxTime;
                    minute++;
                }
                break;

            case TimeFormat.SECONDS:
                int seconds = 0;
                break;

            default:
                break;
        }
    }

    private void DrawNumberLine()
    {
        int x = 0;
        for (int i = 0; i <= label_spacing; i += label_spacing)
        {
            xAxis.SetPosition(i, new Vector3(x, 0, 0));
            Instantiate(timeLabel, xAxis.GetPosition(i), Quaternion.identity);
            // smallLabel.Add();
            x++;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}