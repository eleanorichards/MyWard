using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text drugName;
    public Text drugDosage;
    public Text drugUnits;
    public Text minTime;
    public Text maxTime;
    public Text minVital;
    public Text maxVital;

    private GraphData _GD;

    public GameObject[] tabPages;
    public GameObject[] tabs;
    public Sprite[] tabGraphics;

    //Vector2[] _drugData, float _drugDose, int[,] _timeScale, int[,] _vitalScale, string _vitalName, string _drugName;

    // Use this for initialization
    void Start()
    {
        //tabs = new GameObject[3];
        tabPages[0].SetActive(true);
        tabPages[1].SetActive(false);
        tabPages[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void tabChanged(int tabIndex)
    {
        switch (tabIndex)
        {
            case 0:
                //View main graph, DD for vital and drug
                tabPages[0].SetActive(true);
                tabs[0].GetComponentInChildren<Image>().sprite = tabGraphics[0];
                tabPages[1].SetActive(false);
                tabs[1].GetComponentInChildren<Image>().sprite = tabGraphics[1];
                tabPages[2].SetActive(false);
                tabs[2].GetComponentInChildren<Image>().sprite = tabGraphics[1];
                break;

            case 1:
                //Drug edit
                tabPages[0].SetActive(false);
                tabs[0].GetComponentInChildren<Image>().sprite = tabGraphics[1];

                tabPages[1].SetActive(true);
                tabs[1].GetComponentInChildren<Image>().sprite = tabGraphics[0];

                tabPages[2].SetActive(false);
                tabs[2].GetComponentInChildren<Image>().sprite = tabGraphics[1];

                break;

            case 2:
                //Vital edit
                tabPages[0].SetActive(false);
                tabs[0].GetComponentInChildren<Image>().sprite = tabGraphics[1];

                tabPages[1].SetActive(false);
                tabs[1].GetComponentInChildren<Image>().sprite = tabGraphics[1];

                tabPages[2].SetActive(true);
                tabs[2].GetComponentInChildren<Image>().sprite = tabGraphics[0];

                break;

            default:
                break;
        }
    }
}