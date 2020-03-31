using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Seeds : MonoBehaviour
{
    public List<Seed_item> items;

    private static Inventory_Seeds _instance;
    public static Inventory_Seeds Instance { get { return _instance; } }

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
        items = new List<Seed_item>();
    }

    public void GainItem(Seed_type item, int amount)
    {
        foreach (Seed_item i in items)
        {

            if (i.GetSeedType().ToString().Equals(item.ToString()))
            {
                i.AddAmount(amount);
                return;
            }
        }
        //If not found
        items.Add(new Seed_item(item, amount));
        Inventory_seeds_UI.Instance.RefreshImages();
    }

    public void RemoveItem(Seed_type item, int amount)
    {
        foreach (Seed_item i in items)
        {
            if (i.GetSeedType().ToString().Equals(item.ToString()))
            {
                i.RemoveAmount(amount);
                return;
            }
        }
    }

    public int FindAmount(Seed_type item)
    {
        foreach (Seed_item i in items)
        {
            if (i.GetSeedType().ToString().Equals(item.ToString()))
            {
                return i.GetAmount();
            }
        }
        return 0;
    }
}
