﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Vital Info
    [Header("Vital Info")]
    public InputField vitalName;

    public InputField vitalInfo;

    public InputField vitalMinStatus;
    public InputField vitalMaxStatus;
    public InputField vitalUnits;

    [Header("Drug Info")]
    public InputField drugName;

    public InputField drugInfo;
    public InputField drugMinDose;
    public InputField drugMaxDose;
    public InputField drugUnits;

    private XMLManager xmlManager;

    public GameObject[] tabPages;
    public GameObject[] tabs;
    public Sprite[] tabGraphics;

    private Dropdown drugDropDown;
    private Dropdown vitalDropDown;
    public Dropdown xAxisDropDown;
    public Dropdown yAxisDropdown;

    // Use this for initialization
    private void Start()
    {
        xmlManager = GetComponent<XMLManager>();
        xmlManager.PopulateDrugDD();
        xmlManager.PopulateVitalDD();
        tabPages[0].SetActive(true);
        tabPages[1].SetActive(false);
        tabPages[2].SetActive(false);
    }

    // Update is called once per frame
    private void Update()
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

    public void UpdateVitalInputFields(VitalContainer _vitalData)
    {
        List<Dropdown.OptionData> menuOptions = vitalDropDown.GetComponent<Dropdown>().options;

        foreach (VitalContainer.VitalData vitalData in _vitalData._vitalDat)
        {
            if (menuOptions[vitalDropDown.value].text == vitalData.name.Trim())
            {
                vitalName.text = vitalData.name.Trim();
                vitalInfo.text = vitalData.info.Trim();
                vitalMinStatus.text = vitalData.minStatus.Trim();
                vitalMaxStatus.text = vitalData.maxStatus.Trim();
                vitalUnits.text = vitalData.units.Trim();
            }
        }
    }

    public void UpdateDrugInputFields(DrugContainer _drugData)
    {
        List<Dropdown.OptionData> menuOptions = drugDropDown.GetComponent<Dropdown>().options;

        foreach (DrugContainer.DrugData drugData in _drugData._drugDat)
        {
            if (menuOptions[drugDropDown.value].text == drugData.name.Trim())
            {
                print(drugData.info.Trim());
                drugName.text = drugData.name.Trim();
                drugInfo.text = drugData.info.Trim();
                vitalMinStatus.text = drugData.minDose.Trim();
                vitalMaxStatus.text = drugData.maxDose.Trim();
                vitalUnits.text = drugData.units.Trim();
            }
        }
    }

    public void DeleteVitalField()
    {
        xmlManager.DeleteVitalField(vitalName.text);
    }

    public void DeleteDrugField()
    {
        xmlManager.DeleteDrugField(drugName.text);
    }

    public void UpdateVitalDD(VitalContainer _vitalData)
    {
        vitalDropDown = GameObject.Find("VitalDD").GetComponent<Dropdown>();
        vitalDropDown.ClearOptions();
        List<string> vitalNames = new List<string>();
        //LOADXML
        foreach (VitalContainer.VitalData vitalData in _vitalData._vitalDat)
        {
            vitalNames.Add(vitalData.name.Trim());
        }
        vitalDropDown.AddOptions(vitalNames);
    }

    public void UpdateDrugDD(DrugContainer _drugData)
    {
        drugDropDown = GameObject.Find("DrugDD").GetComponent<Dropdown>();
        drugDropDown.ClearOptions();
        List<string> drugNames = new List<string>();

        foreach (DrugContainer.DrugData drugData in _drugData._drugDat)
        {
            drugNames.Add(drugData.name.Trim());
        }
        drugDropDown.AddOptions(drugNames);
    }

    public void UpdateXAxisDD(DrugContainer _drugData)
    {
        xAxisDropDown = GameObject.Find("xAxisDD").GetComponent<Dropdown>();
        xAxisDropDown.ClearOptions();
        List<string> drugNames = new List<string>();

        foreach (DrugContainer.DrugData drugData in _drugData._drugDat)
        {
            drugNames.Add(drugData.name.Trim());
        }
        xAxisDropDown.AddOptions(drugNames);
    }

    public void UpdateYAxisDD(VitalContainer _vitalData)
    {
        yAxisDropdown = GameObject.Find("yAxisDD").GetComponent<Dropdown>();
        yAxisDropdown.ClearOptions();
        List<string> vitalNames = new List<string>();
        foreach (VitalContainer.VitalData vitalData in _vitalData._vitalDat)
        {
            vitalNames.Add(vitalData.name.Trim());
        }
        yAxisDropdown.AddOptions(vitalNames);
    }
}