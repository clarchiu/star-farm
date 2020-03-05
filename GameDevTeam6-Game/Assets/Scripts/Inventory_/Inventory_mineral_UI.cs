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
    public static Inventory_mineral_UI Instance { get { return _instance; } }

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
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            ToggleInventory();
        }
    }
    public void ToggleInventory()
    {
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
            RefreshImages();
        } else
        {
            panel.SetActive(false);
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
}
