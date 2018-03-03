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

    public LineRenderer xAxis;
    public LineRenderer yAxis;

    public int maxTime; //in minutes

    public int maxVitalRate;

    public GameObject smallLabel;
    public GameObject timeLabel;

    public GameObject ysmallLabel;
    public GameObject ytimeLabel;

    public string vitalName;

    private GameObject graphHolder;

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
        graphHolder = GameObject.Find("Axis");
        xAxisLabel = GameObject.Find("XLabel").GetComponent<Text>();
        yAxisLabel = GameObject.Find("YLabel").GetComponent<Text>();
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
                // number_spacing = maxTime;
                label_spacing = (maxTime * 60) / 10;
                break;

            case TimeFormat.MINUTES:
                xAxisLabel.text = "Time (mins)";
                // number_spacing = maxTime;
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
        switch (timeFormat)
        {
            case TimeFormat.HOURS:
                int hour = 0;
                largeMarkerPos = 0.0f;
                smallMarkersSet = false;
                smallMarkerProgression = 0.0f;

                for (int a = 0; a <= label_spacing; a += 6) //break in to hour intervals
                {
                    smallMarkerPos = largeMarkerPos;
                    GameObject curTimeLabel = Instantiate(timeLabel, new Vector3(largeMarkerPos, 0.0f, 0.0f), Quaternion.identity);
                    curTimeLabel.GetComponentInChildren<Text>().text = hour.ToString();
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
                            GameObject curSmallLabel = Instantiate(smallLabel, new Vector3(smallMarkerPos, 0.0f, 0.0f), Quaternion.identity);
                            if (b == 3)
                            {
                                curSmallLabel.GetComponentInChildren<Text>().text = hour.ToString() + ":30";
                            }
                            else
                            {
                                curSmallLabel.GetComponentInChildren<Text>().text = "";
                            }
                            //add marker label
                            smallMarkerPos += smallMarkerProgression;
                            curSmallLabel.transform.SetParent(graphHolder.transform);
                        }
                    }
                    curTimeLabel.transform.SetParent(graphHolder.transform);

                    hour++;
                }
                break;

            case TimeFormat.MINUTES:
                int minute = 0;
                largeMarkerPos = 0.0f;
                smallMarkersSet = false;
                smallMarkerProgression = 0.0f;

                for (int a = 0; a <= label_spacing; a += 6) //break in to hour intervals
                {
                    smallMarkerPos = largeMarkerPos;
                    GameObject curTimeLabel = Instantiate(timeLabel, new Vector3(largeMarkerPos, 0.0f, 0.0f), Quaternion.identity);
                    curTimeLabel.GetComponentInChildren<Text>().text = minute.ToString();
                    largeMarkerPos += xEndPos.x / maxTime;

                    if (!smallMarkersSet) //to ensure the gap starts at 0 each time
                    {
                        smallMarkersSet = true;
                        smallMarkerProgression = largeMarkerPos / 6.0f; //SETTING SMALL X INCREASE (6 per gap)
                    }
                    if (a < label_spacing)
                    {
                        for (int b = 0; b < 6; b++) //break in to 10's of minutes
                        {
                            GameObject curSmallLabel = Instantiate(smallLabel, new Vector3(smallMarkerPos, 0.0f, 0.0f), Quaternion.identity);
                            if (b == 3)
                            {
                                curSmallLabel.GetComponentInChildren<Text>().text = minute.ToString() + ":30";
                            }
                            else
                            {
                                curSmallLabel.GetComponentInChildren<Text>().text = "";
                            }
                            //add marker label
                            smallMarkerPos += smallMarkerProgression;
                            curSmallLabel.transform.SetParent(graphHolder.transform);
                        }
                    }
                    curTimeLabel.transform.SetParent(graphHolder.transform);

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

    private void DrawYMarkerLines()
    {
        //yAxisLabel =
        switch (vital)
        {
            case Vital.LUNGS:
                int breathingRate = 0;
                largeMarkerPos = 0.0f;
                smallMarkersSet = false;
                smallMarkerProgression = 0.0f;
                label_spacing = (maxVitalRate * 100) / 10; //WITH the * X

                for (int a = 0; a <= label_spacing; a += 10) //break in to hour intervals //this number neds to CORRESPOND
                {
                    smallMarkerPos = largeMarkerPos;
                    GameObject curTimeLabel = Instantiate(ytimeLabel, new Vector3(0.0f, largeMarkerPos, 0.0f), Quaternion.identity);
                    curTimeLabel.GetComponentInChildren<Text>().text = breathingRate.ToString();
                    largeMarkerPos += yEndPos.y / maxVitalRate;

                    if (!smallMarkersSet) //to ensure the gap starts at 0 each time
                    {
                        smallMarkersSet = true;
                        smallMarkerProgression = largeMarkerPos / 10.0f; //SETTING SMALL X INCREASE (10 per gap)
                    }
                    if (a < label_spacing)
                    {
                        for (int b = 0; b < 10; b++) //break in to 10's of minutes
                        {
                            GameObject curSmallLabel = Instantiate(ysmallLabel, new Vector3(0.0f, smallMarkerPos, 0.0f), Quaternion.identity);
                            if (b == 3)
                            {
                                curSmallLabel.GetComponentInChildren<Text>().text = breathingRate.ToString() + ":30";
                            }
                            else
                            {
                                curSmallLabel.GetComponentInChildren<Text>().text = "";
                            }
                            //add marker label
                            smallMarkerPos += smallMarkerProgression;
                            curSmallLabel.transform.SetParent(graphHolder.transform);
                        }
                    }
                    curTimeLabel.transform.SetParent(graphHolder.transform);

                    breathingRate++;
                }
                break;

            case Vital.KIDNEY:
                break;

            case Vital.HEART:
                break;

            default:
                break;
        }
    }

    private void SetYScale()
    {
        yAxisLabel.text = vitalName;
        // number_spacing = maxTime;
        label_spacing = (maxTime * 60) / 10;
    }
}