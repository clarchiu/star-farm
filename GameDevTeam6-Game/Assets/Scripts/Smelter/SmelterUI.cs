using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmelterUI : MonoBehaviour
{
    public GameObject smelterPanel;
    public GameObject button1, button2, button3;

    public GameObject item1;


    public GameObject item2;
    public GameObject resultImg;


    public InventorySelector inventory;
    private bool item1In;
    private bool item2In;
    private bool swap;
    private Mineral_type itemType1;
    private Mineral_type itemType2;
    public Sprite blank;
    public GameObject itemNum1;
    public GameObject itemNum2;
    int item1Amount;
    int item2Amount;
    Text item1Text;
    Text item2Text;
    bool smelterRunning;
    Mineral_type resultType;
    Gen_type genResultType;
    
    public GameObject resultNum;
    public GameObject result;
    public GameObject progressBar;
    float progressTime;
    float timeRemaining;
    Text resultNume;
    Text resulte;
    bool smelterDone;
    public GameObject fuelBar;
    float fuelAmount;

    private void Start()
    {
       // Debug.Log(this.gameObject.name);
       // item1 = gameObject.transform.GetChild(0).gameObject;
       //item2 = gameObject.transform.GetChild(1).gameObject;
    }
    private void Awake()
    {
        smelterPanel.SetActive(false);
        resultNume = resultNum.GetComponent<Text>();
        resulte = result.GetComponent<Text>();
        fuelBar.transform.localScale = new Vector3(0,1,1);
        progressBar.transform.localScale = new Vector3(0, 1, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAll();
        }

        if (item1In == true)
        {
            itemNum1.SetActive(true);
            item1Text = itemNum1.GetComponent<Text>();
            item1Text.text = item1Amount.ToString();
        }
        else
        {
            itemNum1.SetActive(false);
        }
        if (item2In == true)
        {
            itemNum2.SetActive(true);
            item2Text = itemNum2.GetComponent<Text>();
            item2Text.text = item2Amount.ToString() ;
        }
        else
        {
            itemNum2.SetActive(false);
        }
        if (item1In == true && item2In == true && smelterRunning == false)
        {
            if (CheckSmeltRecipe() == true)
            {
                resultImg.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(resultType);
                resultNume.text = (item1Amount+item2Amount).ToString();
                resulte.text = char.ToUpper(resultType.ToString()[0]) + resultType.ToString().Substring(1);
            }
            else if (CheckItemRecipe())
            {
                resultImg.GetComponent<Image>().sprite = ResourceManager.Instance.GetGenSprite(genResultType);
                resultNume.text = "1";
                resulte.text = char.ToUpper(genResultType.ToString()[0]) + genResultType.ToString().Substring(1);
            }
            else 
            {
                resultNume.text = "";
                resulte.text = "";
                resultImg.GetComponent<Image>().sprite = blank;
            }
        }
        if(smelterRunning == true)
        {
            //Debug.Log(progressTime);
            if (timeRemaining >= 0)
            {
                // Debug.Log(timeRemaining);
                if (fuelAmount > 0)
                {
                    Debug.Log(timeRemaining);
                    timeRemaining = timeRemaining - 100 * Time.deltaTime;
                    progressBar.transform.localScale = new Vector2(1 - timeRemaining / progressTime, 1);
                    fuelAmount -= Time.deltaTime/10;
                    fuelBar.transform.localScale = new Vector2(fuelAmount / 100f, 1);
                }
               
            }
            else
            {
                smelterDone = true;
            }
        }
    }

    string resultTypeString;

    public void TaskOnClick(int buttonNum)
    {
        if(buttonNum == 1)
        {
            item1Amount++;
            item2Amount++;
        }
        if(buttonNum == 2)
        {
            if (item2Amount > 0 && item1Amount > 0)
            {
                item1Amount--;
                item2Amount--;
            }
        }
        if(buttonNum == 3)
        {
            if (CheckSmeltRecipe() == true && Inventory_mineral.Instance.FindAmount(itemType1) >= item1Amount && Inventory_mineral.Instance.FindAmount(itemType2) >= item2Amount && item1Amount != 0 && item2Amount != 0 && smelterRunning == false && smelterDone == false) 
            {
                resultTypeString = "mineral";
               // Debug.Log("SmelterRecipe Correct");
                Inventory_mineral.Instance.RemoveItem(itemType1, item1Amount);
                Inventory_mineral.Instance.RemoveItem(itemType2, item2Amount);
                smelterRunning = true;
            }
            else if (CheckItemRecipe() && Inventory_mineral.Instance.FindAmount(itemType1) >= item1Amount && Inventory_mineral.Instance.FindAmount(itemType2) >= item2Amount && item1Amount != 0 && item2Amount != 0 && smelterRunning == false && smelterDone == false)
            {
                resultTypeString = "item";
                Inventory_mineral.Instance.RemoveItem(itemType1, item1Amount);
                Inventory_mineral.Instance.RemoveItem(itemType2, item2Amount);
                smelterRunning = true;
            }
        }
        if(buttonNum == 4)
        {
            if(smelterDone == true)
            {
                if (resultTypeString == "mineral")
                {
                    Inventory_mineral.Instance.GainItem(resultType, item1Amount + item2Amount);
                } else
                {
                    Inventory_gen.Instance.GainItem(genResultType, item1Amount + item2Amount);
                }
                smelterDone = false;
                smelterRunning = false;
                item1In = false;
                item2In = false;
                item1Amount = 0;
                item2Amount = 0;
                swap = false;
                item1.GetComponent<Image>().sprite = blank;
                item2.GetComponent<Image>().sprite = blank;
                smelterDone = false;
                progressBar.transform.localScale = new Vector2(0, 1);
                resultNume.text = "";
                resulte.text = "";
                resultImg.GetComponent<Image>().sprite = blank;
            }
        }
        if(buttonNum == 5)
        {
            if(Inventory_mineral.Instance.FindAmount(Mineral_type.coal) > 0 && fuelAmount <= 90)
            {
                Inventory_mineral.Instance.RemoveItem(Mineral_type.coal, 1);
                fuelAmount += 10f;
                if(fuelAmount > 100)
                {
                    fuelAmount = 100;
                }
                fuelBar.transform.localScale = new Vector2(fuelAmount/100f, 1);
            }
        }
        if (buttonNum == 6)
        {
            smelterPanel.SetActive(false);

        }
        if(buttonNum == 7)
        {
            smelterPanel.SetActive(true);
        }

    }

    private bool CheckSmeltRecipe()
    {

        if ((itemType1 == Mineral_type.copper && itemType2 == Mineral_type.tin) || (itemType2 == Mineral_type.copper && itemType1 == Mineral_type.tin))
        {
            resultType = Mineral_type.bronze;
            progressTime = 5100;
            timeRemaining = 5100;
            return true;
        }
        else if ((itemType1 == Mineral_type.coal && itemType2 == Mineral_type.iron) || (itemType2 == Mineral_type.coal && itemType1 == Mineral_type.iron))
        {
            resultType = Mineral_type.steel;
            progressTime = 6500;
            timeRemaining = 6500;
            return true;
        }
        else if ((itemType1 == Mineral_type.granite && itemType2 == Mineral_type.tungsten) || (itemType2 == Mineral_type.granite && itemType1 == Mineral_type.tungsten))
        {
            resultType = Mineral_type.adamantite;
            progressTime = 6500;
            timeRemaining = 6500;
            return true;
        }
        if ((itemType1 == Mineral_type.steel && itemType2 == Mineral_type.cobalt) || (itemType2 == Mineral_type.steel && itemType1 == Mineral_type.cobalt))
        {
            resultType = Mineral_type.mithril;
            progressTime = 5100;
            timeRemaining = 5100;
            return true;
        }
        if ((itemType1 == Mineral_type.silver && itemType2 == Mineral_type.mithril) || (itemType2 == Mineral_type.silver && itemType1 == Mineral_type.mithril))
        {
            resultType = Mineral_type.tartarite;
            progressTime = 5100;
            timeRemaining = 5100;
            return true;
        }
        if ((itemType1 == Mineral_type.adamantite && itemType2 == Mineral_type.tartarite) || (itemType2 == Mineral_type.adamantite && itemType1 == Mineral_type.tartarite))
        {
            resultType = Mineral_type.orichalum;
            progressTime = 5100;
            timeRemaining = 5100;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckItemRecipe()
    {
        if ((itemType1 == Mineral_type.adamantite && itemType2 == Mineral_type.concrete) || (itemType2 == Mineral_type.adamantite && itemType1 == Mineral_type.concrete))
        {
            genResultType = Gen_type.turret;
            progressTime = 5100;
            timeRemaining = 5100;
            return true;
        }
        else if ((itemType1 == Mineral_type.steel && itemType2 == Mineral_type.iron) || (itemType2 == Mineral_type.steel && itemType1 == Mineral_type.iron))
        {
            genResultType = Gen_type.wall;
            progressTime = 5100;
            timeRemaining = 5100;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void toggleInfoPanel()
    {
        if (smelterRunning == false)
        {
            item1In = false;
            item2In = false;
            swap = false;
            item1.GetComponent<Image>().sprite = blank;
            item2.GetComponent<Image>().sprite = blank;
            item1Amount = 0;
            item2Amount = 0;
            // progressBar.transform.localScale = new Vector2( 0, 1);
            resultNume.text = "";
            resulte.text = "";
            resultImg.GetComponent<Image>().sprite = blank;
            item1Amount = 0;
            item2Amount = 0;
        }
        //        if (smelterPanel.activeSelf)
        //      {
        //        smelterPanel.SetActive(false);
        //  }
        //else
        // {
        CloseAll();
       // }

    }
    public void setItems(int buttonNum)
    {
        if (smelterRunning == false)
        {
            if (item1In == false)
            {
                item1.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(Inventory_mineral.Instance.items[buttonNum].GetMineralType());
                item1In = true;
                itemType1 = Inventory_mineral.Instance.items[buttonNum].GetMineralType();
               // Debug.Log("Item1In is NOW equal to = " + item1In);

            }
            else if (item2In == false)
            {
                item2.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(Inventory_mineral.Instance.items[buttonNum].GetMineralType());
                item2In = true;
                itemType2 = Inventory_mineral.Instance.items[buttonNum].GetMineralType();
//Debug.Log("Item2In is Now equal to = " + item1In);
            }
            else if (item1In == true && item2In == true && swap == false)
            {

                item1.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(Inventory_mineral.Instance.items[buttonNum].GetMineralType());
                itemType1 = Inventory_mineral.Instance.items[buttonNum].GetMineralType();
                swap = true;
            }
            else if (item1In == true && item2In == true && swap == true)
            {
                item2.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(Inventory_mineral.Instance.items[buttonNum].GetMineralType());
                itemType2 = Inventory_mineral.Instance.items[buttonNum].GetMineralType();
                swap = false;
            }
        }
    }

    private void CloseAll()
    {
        smelterPanel.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
    }
}