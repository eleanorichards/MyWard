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

    public LayerMask acceptMask;

    // Use this for initialization
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        textBox = GameObject.Find("CursorPos"); //for displaying current location
        pointPlotter = GetComponent<PointPlotter>(); //point plotter script
        positionText = textBox.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 origin = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

        Ray ray = cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane));

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, acceptMask);
        Debug.DrawRay(origin, ray.direction, Color.red);
        // RaycastHit hit;

        if (hit)
        {
            transform.localPosition = hit.point;
            textBox.transform.position = new Vector2(hit.point.x, hit.point.y + 0.3f);
            positionText.text = pointPlotter.ClickRecieved(hit.point).ToString();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //pointPlotter.ClickRecieved(hit.point);
                pointPlotter.PlacePoint(hit.point);
            }

            if (hit.transform.CompareTag("point"))
            {
                Debug.Log("removing d");
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Debug.Log("removing point...");
                    pointPlotter.RemovePoint(hit.transform.gameObject);
                }
            }
        }
        //RaycastHit2D pointHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, acceptMask);
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