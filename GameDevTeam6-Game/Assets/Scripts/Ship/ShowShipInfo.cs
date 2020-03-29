using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAll();
        }
    }
    private void OnMouseDown() {
        toggleInfoPanel();
    }

    private void toggleInfoPanel()
    {
        if (!infoPanel.activeSelf && !upgradePanel.activeSelf && !smelterPanel.activeSelf)
        {
            Tutorial.Instance.TriggerDialogue(3);
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
        SoundEffects_.Instance.PlaySoundEffect(SoundEffect.button);

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
                    if (shipLevel == 9)
                    {
                        SceneManager.LoadScene("Ending_1");
                    }

                    Tutorial.Instance.TriggerDialogue(11);
                } else
                {
                    ShowShipError.Instance.DisplayError("Not enough resources to upgrade!");
                }

            }
            if(buttonNum == 5)
            {
                CloseAll();
            }
        } 
      
    }

    void refreshInfo()
    {
        if (shipLevel == 1)
        {
            SetImages(Mineral_type.copper, Mineral_type.tin);
            reqAmount1 = 10;
            reqAmount2 = 10;
            flavorText.text = "Find some basic materials and fix the basic functions of the ship! With these improvements, we should be able to forge new things!";

        }
        if(shipLevel == 2)
        {
            SetImages(Mineral_type.iron, Mineral_type.tin);
            reqAmount1 = 10;
            reqAmount2 = 10;
            flavorText.text = "Now we can craft new metals and items using the forge! The forge uses up coal to turn your metals into something new! Use it to upgrade the ship again";
        }
        if (shipLevel == 3)
        {
            SetImages(Mineral_type.bronze, Mineral_type.steel);
            reqAmount1 = 20;
            reqAmount2 = 20;
            flavorText.text = "Keep upgrading the ship so we can go home!";
        }
        if (shipLevel == 4)
        {
            SetImages(Mineral_type.granite, Mineral_type.tungsten);
            reqAmount1 = 40;
            reqAmount2 = 40;
            flavorText.text = "Keep upgrading the ship so we can go home!";
        }
        if (shipLevel == 5)
        {
            SetImages(Mineral_type.adamantite, Mineral_type.cobalt);
            reqAmount1 = 60;
            reqAmount2 = 60;
            flavorText.text = "Keep upgrading the ship so we can go home!";
        }
        if (shipLevel == 6)
        {
            SetImages(Mineral_type.mithril, Mineral_type.silver);
            reqAmount1 = 120;
            reqAmount2 = 120;
            flavorText.text = "Keep upgrading the ship so we can go home!";
        }
        if (shipLevel == 7)
        {
            SetImages(Mineral_type.tartarite, Mineral_type.concrete);
            reqAmount1 = 240;
            reqAmount2 = 240;
            flavorText.text = "Keep upgrading the ship so we can go home!";
        }
        if (shipLevel == 8)
        {
            SetImages(Mineral_type.orichalum, Mineral_type.chromatic1);
            reqAmount1 = 300;
            reqAmount2 = 10;
            flavorText.text = "The ship will be completely fixed after this final upgrade! You can choose to upgrade your ship to the final tier and finish the game or choosing to upgrade your multi-tool weapon in order to defeat the escaped Specimen.";
        }
        levelNum.text = shipLevel.ToString();
        numText1.text = reqAmount1.ToString();
        numText2.text = reqAmount2.ToString();
    }

    void SetImages(Mineral_type mineral1, Mineral_type mineral2)
    {
        metal1.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(mineral1);
        metal2.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(mineral2);
        neededType1 = mineral1;
        neededType2 = mineral2;
    }

    void CloseAll()
    {
        infoPanel.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
    }
}
