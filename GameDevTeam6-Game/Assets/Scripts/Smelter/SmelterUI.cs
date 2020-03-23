using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmelterUI : MonoBehaviour
{
    public GameObject smelterPanel;

    public GameObject item1;


    public GameObject item2;
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

    private void Start()
    {
        Debug.Log(this.gameObject.name);
       // item1 = gameObject.transform.GetChild(0).gameObject;
       //item2 = gameObject.transform.GetChild(1).gameObject;
    }
    private void Awake()
    {
        smelterPanel.SetActive(false);
    }

    private void Update()
    {
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
    }

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
    }

        private void OnMouseDown()
    {
        toggleInfoPanel();
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
            item1Amount--;
            item2Amount--;
        }
        if (smelterPanel.activeSelf)
        {
            smelterPanel.SetActive(false);
        }
        else
        {
            smelterPanel.SetActive(true);
        }

    }
    public void setItems(int buttonNum)
    {
        Debug.Log("Item1In is originally equal to = " + item1In);
        Debug.Log("Item2In is originally equal to = " + item1In);
        if (item1In == false)
        {
            item1.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(Inventory_mineral.Instance.items[buttonNum].GetMineralType());
            item1In = true;
            itemType1 = Inventory_mineral.Instance.items[buttonNum].GetMineralType();
            Debug.Log("Item1In is NOW equal to = " + item1In);

        }
        else if(item2In == false)
        {
            item2.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(Inventory_mineral.Instance.items[buttonNum].GetMineralType());
            item2In = true;
            itemType2 = Inventory_mineral.Instance.items[buttonNum].GetMineralType();
            Debug.Log("Item2In is Now equal to = " + item1In);
        }
        else if(item1In == true && item2In == true && swap ==  false)
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