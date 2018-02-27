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

    private List<GameObject> smallLabels = new List<GameObject>();
    private List<GameObject> timeLabels = new List<GameObject>();

    public GameObject smallLabel;
    public GameObject timeLabel;

    public int maxTime; //in minutes

    private int number_spacing = 0;
    private int label_spacing = 0;

    private Vector3 origin = Vector3.zero;
    public Vector3 xEndPos;
    public Vector3 yEndPos;

    // Use this for initialization
    private void Start()
    {
        SetTimeScale();
        DrawMarkerLine();
        DrawAxis();
    }

    private void SetTimeScale()
    {
        switch (timeFormat)
        {
            case TimeFormat.DAYS:
                number_spacing = maxTime / 24;
                break;

            case TimeFormat.HOURS:
                number_spacing = maxTime;
                label_spacing = (number_spacing * 60) / 10;
                break;

            case TimeFormat.MINUTES:
                number_spacing = maxTime / 60;

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

    private void DrawMarkerLine()
    {
        switch (timeFormat)
        {
            case TimeFormat.DAYS:
                break;

            case TimeFormat.HOURS:
                int x = 0;
                float xProgression = 0.0f;

                for (int i = 0; i <= label_spacing; i += 6) //break in to 10 min intervals
                {
                    //xAxis.SetPosition(i, new Vector3(xProgression, 0, 0));
                    GameObject curTimeLabel = Instantiate(timeLabel, new Vector3(xProgression, 0.0f, 0.0f), Quaternion.identity);
                    curTimeLabel.GetComponentInChildren<Text>().text = x.ToString();
                    xProgression += xEndPos.x / maxTime;
                    x++;
                }
                break;

            case TimeFormat.MINUTES:
                break;

            case TimeFormat.SECONDS:
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