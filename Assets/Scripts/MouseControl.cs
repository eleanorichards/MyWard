using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    private Camera cam;
    private PointPlotter pointPlotter;
    private bool editMode = false;
    private GameObject textBox;
    private Text positionText;

    // Use this for initialization
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        pointPlotter = GetComponent<PointPlotter>();
        textBox = GameObject.Find("CursorPos");
        positionText = textBox.GetComponent<Text>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 origin = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

        Ray ray = cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("Graph"));
        Debug.DrawRay(origin, ray.direction, Color.red);
        // RaycastHit hit;

        if (hit)
        {
            transform.localPosition = hit.point;
            textBox.transform.position = new Vector2(hit.point.x, hit.point.y + 0.3f);
            positionText.text = pointPlotter.ClickRecieved(hit.point).ToString();
            if (Input.GetKey(KeyCode.Mouse0))
            {
                pointPlotter.ClickRecieved(hit.point);
            }
        }
    }

    public void EditModeSwitch()
    {
        editMode = !editMode;
        if (editMode)
        {
            cam.transform.SetParent(null);
            //tileHoverIcon.SetActive(true);
        }
    }
}