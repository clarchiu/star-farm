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

    private void OnMouseDown()
    {
        toggleInfoPanel();
    }

    private void toggleInfoPanel()
    {
        item1In = false;
        item2In = false;
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
            Debug.Log("Item1In is NOW equal to = " + item1In);

        }
        else if(item2In == false)
        {
            item2.GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(Inventory_mineral.Instance.items[buttonNum].GetMineralType());
            item2In = true;
            Debug.Log("Item2In is Now equal to = " + item1In);
        }
    }
}