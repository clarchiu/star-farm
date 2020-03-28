using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowShipInfo : MonoBehaviour
{
    public GameObject infoPanel;
    public int shipLevel;
    public GameObject displayLevel;
    Text levelNum;
    public GameObject metal1;
    public GameObject metal2;
    public GameObject fixNum1;
    public GameObject fixNum2;
    Text numText1;
    Text numText2;
    Mineral_type neededType1;
    Mineral_type neededType2;
    int reqAmount1;
    int reqAmount2;
    public GameObject textBox;
    Text flavorText;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject smelterPanel;
    public GameObject upgradePanel;
    public GameObject ship;

    private void Start()
    {
        //infoPanel = GameObject.Find("Ship info");
        infoPanel.SetActive(false);
        shipLevel = 1;
        levelNum = displayLevel.GetComponent<Text>();
        numText1 = fixNum1.GetComponent<Text>();
        numText2 = fixNum2.GetComponent<Text>();
        flavorText = textBox.GetComponent<Text>();
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        refreshInfo();
    }

    private void OnMouseDown() {
        toggleInfoPanel();
    }

    private void toggleInfoPanel()
    {
        if (!infoPanel.activeSelf && !upgradePanel.activeSelf && !smelterPanel.activeSelf)
        {
            button1.SetActive(true);
            button2.SetActive(true);
            if (shipLevel > 1)
            {
                button3.SetActive(true);
            }
            infoPanel.SetActive(true);
        } else
        {
            refreshInfo();
        }


    }
    public void TaskOnClick(int buttonNum)
    {
        if (buttonNum == 1)
        {
            infoPanel.SetActive(true);
        }
        if (buttonNum == 2 || buttonNum == 3)
        {
            infoPanel.SetActive(false);
        }
        if (infoPanel.activeSelf)
        {
            if (buttonNum == 4)
            {
                if (Inventory_mineral.Instance.FindAmount(neededType1) >= reqAmount1 && Inventory_mineral.Instance.FindAmount(neededType2) >= reqAmount2)
                {
                  //  Debug.Log("UPGRAIDING!");
                    shipLevel++;
                    Inventory_mineral.Instance.RemoveItem(neededType1, reqAmount1);
                    Inventory_mineral.Instance.RemoveItem(neededType2, reqAmount2);
                    refreshInfo();
                    button3.SetActive(true);
                }

            }
            if(buttonNum == 5)
            {
                infoPanel.SetActive(false);
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
            }
        } 
      
    }

    void refreshInfo()
    {
        if (shipLevel == 1)
        {
            SetImages(Mineral_type.copper, Mineral_type.tin);
            reqAmount1 = 5;
            reqAmount2 = 5;
            flavorText.text = "Find some basic materials and fix the basic functions of the ship! With these improvements, we should be able to forge new things!";

        }
        if(shipLevel == 2)
        {
            SetImages(Mineral_type.bronze, Mineral_type.steel);
        }
        levelNum.text = shipLevel.ToString();
        numText1.text = reqAmount1.ToString();
        numText2.text = reqAmount2.ToString();
        flavorText.text = "Now we can craft new metals and items using the forge! The forge uses up coal to turn your metals into something new! Use it to upgrade the ship again";
    }

    void SetImages(Mineral_type mineral1, Mineral_type mineral2)
    {
        metal1.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(mineral1);
        metal2.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(mineral2);
        neededType1 = mineral1;
        neededType2 = mineral2;
    }
}
