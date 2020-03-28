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

    public Image neededImage1, neededImage2, neededImage3, neededImage4, neededImage5, neededImage6;
    public Text wepLevText, breakLevText, rangLevelText;

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
                    if (wepLev <= 5)
                    {
                        wepLev++;
                        PlayerUpgrades.Instance.UpgradeMeleeDamage();
                        Inventory_mineral.Instance.RemoveItem(neededType1, 4);
                        Inventory_mineral.Instance.RemoveItem(neededType2, 4);
                        refreshInfo();
                    }
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
                    if (breakLev <= 5)
                    {
                        breakLev++;
                        PlayerUpgrades.Instance.UpgradeObstacleDamage();
                        Inventory_mineral.Instance.RemoveItem(neededType3, 4);
                        Inventory_mineral.Instance.RemoveItem(neededType4, 4);
                        refreshInfo();
                    }
                } else
                {
                    ShowShipError.Instance.DisplayError("Not enough resources to upgrade!");
                }

            }
            if (buttonNum == 7)
            {
                if (Inventory_mineral.Instance.FindAmount(neededType5) >= 4 && Inventory_mineral.Instance.FindAmount(neededType6) >= 4)
                {
                    if (rangLev <= 5)
                    {
                        rangLev++;
                        PlayerUpgrades.Instance.UpgradeRangedDamage();
                        Inventory_mineral.Instance.RemoveItem(neededType5, 4);
                        Inventory_mineral.Instance.RemoveItem(neededType6, 4);
                        refreshInfo();
                    }
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
        wepLevText.text    = "Level: " + wepLev;
        breakLevText.text  = "Level: " + breakLev;
        rangLevelText.text = "Level: " + rangLev;

        ///************************************************************************
        ///************************************************************************
        if (wepLev == 1) {
            setNames(Mineral_type.iron, Mineral_type.tin, 1);
        }
        else if (wepLev == 2) {
            setNames(Mineral_type.bronze, Mineral_type.granite, 1);
        }
        else if (wepLev == 3) {
            setNames(Mineral_type.steel, Mineral_type.adamantite, 1);
        }
        else if (wepLev == 4) {
            setNames(Mineral_type.concrete, Mineral_type.silver, 1);
        }
        else if (wepLev == 5) {
            setNames(Mineral_type.orichalum, Mineral_type.chromatic1, 1);
        } else if (wepLev > 5)
        {
            wepText1.GetComponent<Text>().text = "Fully upgraded!";
            wepText2.GetComponent<Text>().text = "Fully upgraded!"; 
        }

        if (wepLev <= 5)
        {
            neededImage1.sprite = ResourceManager.Instance.GetMineralSprite(neededType1);
            neededImage2.sprite = ResourceManager.Instance.GetMineralSprite(neededType2);
        } else
        {
            neededImage1.sprite = null;
            neededImage2.sprite = null;
        }

        ///************************************************************************
        ///************************************************************************

        if (breakLev == 1) {
            setNames(Mineral_type.copper, Mineral_type.bronze, 2);
        } else if(breakLev == 2) {
            setNames(Mineral_type.granite, Mineral_type.steel, 2);
        } else if (breakLev == 3) {
            setNames(Mineral_type.tin, Mineral_type.tungsten, 2);
        } else if (breakLev == 4) {
            setNames(Mineral_type.cobalt, Mineral_type.mithril, 2);
        } else if (breakLev == 5) {
            setNames(Mineral_type.tartarite, Mineral_type.orichalum, 2);
        } else if (breakLev > 5)
        {
            breakText1.GetComponent<Text>().text = "Fully upgraded!";
            breakText2.GetComponent<Text>().text = "Fully upgraded!";
        }

        if (breakLev <= 5)
        {
            neededImage3.sprite = ResourceManager.Instance.GetMineralSprite(neededType3);
            neededImage4.sprite = ResourceManager.Instance.GetMineralSprite(neededType4);
        }
        else
        {
            neededImage3.sprite = null;
            neededImage4.sprite = null;
        }
        ///************************************************************************
        ///************************************************************************
        if (rangLev == 1)
        {
            setNames(Mineral_type.copper, Mineral_type.tin, 3);
        }
        else if (rangLev == 2)
        {
            setNames(Mineral_type.steel, Mineral_type.bronze, 3);
        }
        else if (rangLev == 3)
        {
            setNames(Mineral_type.tungsten, Mineral_type.steel, 3);
        }
        else if (rangLev == 4)
        {
            setNames(Mineral_type.adamantite, Mineral_type.mithril, 3);
        }
        else if (rangLev == 5)
        {
            setNames(Mineral_type.tartarite, Mineral_type.chromatic1, 3);
        }
        else if (rangLev > 5)
        {
            rangText1.GetComponent<Text>().text = "Fully upgraded!";
            rangText2.GetComponent<Text>().text = "Fully upgraded!";
        }

        if (rangLev <= 5)
        {
            neededImage5.sprite = ResourceManager.Instance.GetMineralSprite(neededType5);
            neededImage6.sprite = ResourceManager.Instance.GetMineralSprite(neededType6);
        }
        else
        {
            neededImage5.sprite = null;
            neededImage6.sprite = null;
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
