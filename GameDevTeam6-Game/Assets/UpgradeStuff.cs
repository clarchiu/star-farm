using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStuff : MonoBehaviour
{
    public GameObject infoPanel;
    int shipLevel;
    public GameObject displayLevel;
    Text levelNum;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject wepText1;
    public GameObject wepText2;
    public GameObject breakText1;
    public GameObject breakText2;
    public GameObject rangText1;
    public GameObject rangText2;
    int wepLev;
    int breakLev;
    int rangLev;
    Mineral_type neededType1;
    Mineral_type neededType2;
    Mineral_type neededType3;
    Mineral_type neededType4;
    Mineral_type neededType5;
    Mineral_type neededType6;
    public GameObject Ship;

    private void Start()
    {
        infoPanel.SetActive(false);
        refreshInfo();
        wepLev = 1;
        breakLev = 1;
        rangLev = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAll();
        }
    }

    public void TaskOnClick(int buttonNum)
    {
        if (buttonNum == 2)
        {
            infoPanel.SetActive(true);
            refreshInfo();
        }
        if (buttonNum == 1 || buttonNum == 3)
        {
            infoPanel.SetActive(false);
        }
        if (infoPanel.activeSelf)
        {
            if (buttonNum == 4)
            {
                if (Inventory_mineral.Instance.FindAmount(neededType1) >= 4 && Inventory_mineral.Instance.FindAmount(neededType2) >= 4)
                {
                    PlayerUpgrades.Instance.UpgradeMeleeDamage();
                    Inventory_mineral.Instance.RemoveItem(neededType1, 4);
                    Inventory_mineral.Instance.RemoveItem(neededType2, 4);
                    refreshInfo();
                } else
                {
                    ShowShipError.Instance.DisplayError("Not enough resources to upgrade!");
                }
            }

            if (buttonNum == 5)
            {
                CloseAll();
            }
            if (buttonNum == 6)
            {
                if (Inventory_mineral.Instance.FindAmount(neededType3) >= 4 && Inventory_mineral.Instance.FindAmount(neededType4) >= 4)
                {
                    PlayerUpgrades.Instance.UpgradeObstacleDamage();
                    Inventory_mineral.Instance.RemoveItem(neededType3, 4);
                    Inventory_mineral.Instance.RemoveItem(neededType4, 4);
                    refreshInfo();
                } else
                {
                    ShowShipError.Instance.DisplayError("Not enough resources to upgrade!");
                }

            }
            if (buttonNum == 7)
            {
                if (Inventory_mineral.Instance.FindAmount(neededType5) >= 4 && Inventory_mineral.Instance.FindAmount(neededType6) >= 4)
                {
                    PlayerUpgrades.Instance.UpgradeRangedDamage();
                    Inventory_mineral.Instance.RemoveItem(neededType5, 4);
                    Inventory_mineral.Instance.RemoveItem(neededType6, 4);
                    refreshInfo();
                } else
                {
                    ShowShipError.Instance.DisplayError("Not enough resources to upgrade!");
                }

            }

        }     
        


    }

    void refreshInfo()
    {
    shipLevel = Ship.GetComponent<ShowShipInfo>().shipLevel;
        if (wepLev == 1)
        {
            setNames(Mineral_type.iron, Mineral_type.tin, 1);
        }
        if (wepLev == 2)
        {
            setNames(Mineral_type.bronze, Mineral_type.tungsten, 1);
        }
        if (rangLev == 1)
        {
            setNames(Mineral_type.copper, Mineral_type.iron, 3);
        }
        if(rangLev == 2)
        {
            setNames(Mineral_type.steel, Mineral_type.silver, 3);
        }
        if(breakLev == 1)
        {
            setNames(Mineral_type.copper, Mineral_type.bronze, 2);
        }
        if(breakLev == 2)
        {
            setNames(Mineral_type.cobalt, Mineral_type.tungsten, 2);
        }
    }

    void setNames(Mineral_type mineral1, Mineral_type mineral2, int num)
    {
        if (num == 1)
        {
            neededType1 = mineral1;
            neededType2 = mineral2;
            wepText1.GetComponent<Text>().text = char.ToUpper(mineral1.ToString()[0]) + mineral1.ToString().Substring(1) + " X 4";
            wepText2.GetComponent<Text>().text = char.ToUpper(mineral2.ToString()[0]) + mineral2.ToString().Substring(1) + " X 4";
        }
        if (num == 2)
        {
            neededType3 = mineral1;
            neededType4 = mineral2;
            breakText1.GetComponent<Text>().text = char.ToUpper(mineral1.ToString()[0]) + mineral1.ToString().Substring(1) + " X 4";
            breakText2.GetComponent<Text>().text = char.ToUpper(mineral2.ToString()[0]) + mineral2.ToString().Substring(1) + " X 4";
        }
        if (num == 3)
        {
            neededType5 = mineral1;
            neededType6 = mineral2;
            rangText1.GetComponent<Text>().text = char.ToUpper(mineral1.ToString()[0]) + mineral1.ToString().Substring(1) + " X 4";
            rangText2.GetComponent<Text>().text = char.ToUpper(mineral2.ToString()[0]) + mineral2.ToString().Substring(1) + " X 4";
        }
    }

    private void CloseAll()
    {
        infoPanel.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
    }
}
