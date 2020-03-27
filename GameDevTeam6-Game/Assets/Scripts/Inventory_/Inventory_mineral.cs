using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_mineral : MonoBehaviour
{
    public List<Mineral_item> items;

    private static Inventory_mineral _instance;
    public static Inventory_mineral Instance
    {
        get
        {
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Inventory_mineral>();
                }
                if (_instance == null)
                {
                    Debug.Log("Tutorial script not found!, Add resource manager prefab to your scene!");
                }
                return _instance;
            }
        }
    }


    private void Start()
    {
        items = new List<Mineral_item>();
    }

    public void GainItem(Mineral_type item, int amount)
    {
        foreach (Mineral_item i in items)
        {
            
            if (i.GetMineralType().ToString().Equals(item.ToString()))
            {
                i.AddAmount(amount);
                return;
            }
        }
        //If not found
        items.Add(new Mineral_item(item, 1));
        Inventory_mineral_UI.Instance.RefreshImages();
    }

    public void RemoveItem(Mineral_type item, int amount)
    {
        foreach (Mineral_item i in items)
        {
            if (i.GetMineralType().ToString().Equals(item.ToString()))
            {
                i.RemoveAmount(amount);
                return;
            }
        }
    }

    public int FindAmount(Mineral_type item)
    {
        foreach (Mineral_item i in items)
        {
            if (i.GetMineralType().ToString().Equals(item.ToString()))
            {
                return i.GetAmount();
            }
        }
        return 0;
    }
}
