using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_seeds_UI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> images;
    [SerializeField]
    private GameObject panel;
    bool invON;
    private static Inventory_seeds_UI _instance;
    public static Inventory_seeds_UI Instance { get { return _instance; } }
    bool buttonPress;


    //Singleton
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        panel.SetActive(false);
        invON = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            buttonPress = true;
            ToggleInventory();
        }
    }
    public void ToggleInventory()
    {
        if (!panel.activeSelf && invON == true)
        {
            panel.SetActive(true);
            RefreshImages();
            buttonPress = false;
        }
        else if (buttonPress == true)
        {
            panel.SetActive(false);
        }
    }

    public void RefreshImages()
    {
        for (int i = 0; i < Inventory_Seeds.Instance.items.Count; i++)
        {
            if (i < images.Count)
            {
                images[i].GetComponent<Image>().sprite = ResourceManager.Instance.GetSeedSprite(Inventory_Seeds.Instance.items[i].GetSeedType());
                images[i].GetComponentInChildren<Text>().text = Inventory_Seeds.Instance.items[i].GetAmount().ToString();
            }
        }
    }
    public void OnTaskClick(int buttonNum)
    {
        if (buttonNum == 1)
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
