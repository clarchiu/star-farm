using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_mineral_UI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> images;
    [SerializeField]
    private GameObject panel;

    private static Inventory_mineral_UI _instance;
    public static Inventory_mineral_UI Instance
    {
        get
        {
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Inventory_mineral_UI>();
                }
                if (_instance == null)
                {
                    Debug.Log("Inventory_mineral_UI script not found!, Add InventoryController prefab to your scene!");
                }
                return _instance;
            }
        }
    }

    bool invON;
    bool buttonPress;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    //Singleton
    private void Awake()
    {
        panel.SetActive(false);
        invON = true;
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            buttonPress = true;
            ToggleInventory();
            ToggleButtons();
        }
    }
    public void ToggleInventory()
    {
        if (!panel.activeSelf && invON == true)
        {
            panel.SetActive(true);
            RefreshImages();
            buttonPress = false;
        } else if(buttonPress == true)
        {
            buttonPress = false;
            panel.SetActive(false);
        }
    }
    public void ToggleButtons()
    {
        if (!button1.activeSelf)
        {
            button1.SetActive(true);
            button2.SetActive(true);
            button3.SetActive(true);
        }
        else
        {
            button1.SetActive(false);
            button2.SetActive(false);
            button3.SetActive(false);
        }
    }

    public void RefreshImages()
    {
        for (int i = 0; i < Inventory_mineral.Instance.items.Count; i++)
        {
            if (i < images.Count)
            {
                images[i].GetComponent<Image>().sprite = ResourceManager.Instance.GetMineralSprite(Inventory_mineral.Instance.items[i].GetMineralType());
                images[i].GetComponentInChildren<Text>().text = Inventory_mineral.Instance.items[i].GetAmount().ToString();
            }
        }
    }
    public void OnTaskClick(int buttonNum)
    {
        if(buttonNum == 0)
        {
            invON = true;
            ToggleInventory();
        }
        else
        {
            invON = false;
            buttonPress = true;
            ToggleInventory();
        }
    }
}
