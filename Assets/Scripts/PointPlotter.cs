using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPlotter : MonoBehaviour
{
    private AxisGen graphHolder;

    private Vector2 graphDimensions;
    private Vector2 axisScales;
    private Vector2 scaleValue;
    private Vector2 graphSpacePos;

    private GameObject graphPoint;

    private List<GameObject> points = new List<GameObject>();
    private LineRenderer lineRend;

    // Use this for initialization
    private void Start()
    {
        lineRend = GameObject.Find("GraphLine").GetComponent<LineRenderer>();
        graphHolder = GameObject.Find("GraphCanvas").GetComponent<AxisGen>();
        graphPoint = Resources.Load("GraphPoint") as GameObject;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    /// <summary>
    /// translates click in world space to graph space
    /// </summary>
    /// <param name="_hitPoint">cursor hit point in world space</param>
    public Vector2 ClickRecieved(Vector2 _hitPoint)
    {
        //xEndPos / max Time
        //_hitPoint / THATVALUE
        graphDimensions = graphHolder.ReturnDimensions();
        axisScales = graphHolder.ReturnScale();
        scaleValue = new Vector2(axisScales.x / graphDimensions.x, axisScales.y / graphDimensions.y);
        graphSpacePos = new Vector2(_hitPoint.x / scaleValue.x, _hitPoint.y / scaleValue.y);

        return graphSpacePos;
    }

    public void PlacePoint(Vector2 worldPos)
    {
        GameObject curPoint = Instantiate(graphPoint, worldPos, Quaternion.identity);
        curPoint.transform.SetParent(graphHolder.transform);
        points.Add(curPoint);
        LinkPoints();
    }

    public void RemovePoint(GameObject selected)
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i])
            {
                if (points[i] == selected)
                {
                    points.Remove(points[i]);
                    Destroy(selected);
                    // points.Sort();
                }
            }
        }
        LinkPoints();
    }

    private void LinkPoints()
    {
        int pointNum = points.Count;
        if (pointNum > 1)
        {
            lineRend.positionCount = pointNum;
            for (int i = 0; i < pointNum; i++)
            {
                lineRend.SetPosition(i, points[i].transform.position);
            }
            //Debug.DrawLine(points[pointNum].transform.position, points[pointNum - 1].transform.position, Color.green, 10.0f);
        }
        else
        {
        }
    }
}