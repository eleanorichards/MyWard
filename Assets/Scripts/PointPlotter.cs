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

    // Use this for initialization
    private void Start()
    {
        graphHolder = GameObject.Find("GraphCanvas").GetComponent<AxisGen>();
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
}