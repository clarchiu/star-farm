using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_gen : MonoBehaviour
{
    public List<Gen_item> items;

    private static Inventory_gen _instance;
    public static Inventory_gen Instance { get { return _instance; } }

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

    }

    private void Start()
    {
        items = new List<Gen_item>();
    }

    public void GainItem(Gen_type item, int amount)
    {
        foreach (Gen_item i in items)
        {

            if (i.GetGenType().ToString().Equals(item.ToString()))
            {
                i.AddAmount(amount);
                return;
            }
        }
        //If not found
        items.Add(new Gen_item(item, amount));
        Inventory_general_UI.Instance.RefreshImages();
    }

    public void RemoveItem(Gen_type item, int amount)
    {
        foreach (Gen_item i in items)
        {
            if (i.GetGenType().ToString().Equals(item.ToString()))
            {
                i.RemoveAmount(amount);
                return;
            }
        }
    }

    public int FindAmount(Gen_type item)
    {
        foreach (Gen_item i in items)
        {
            if (i.GetGenType().ToString().Equals(item.ToString()))
            {
                return i.GetAmount();
            }
        }
        return 0;
    }
}
