using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed_item
{

    private Seed_type type;
    private int amount;


    public Seed_item(Seed_type t, int amount)
    {
        type = t;
        this.amount = amount;
    }

    public Seed_type GetSeedType()
    {
        return type;
    }

    public void AddAmount(int i)
    {
        amount += i;
        Inventory_seeds_UI.Instance.RefreshImages();
        Debug.Log("Where is this coming from?");
    }

    public void RemoveAmount(int i)
    {
        amount -= i;
        Inventory_seeds_UI.Instance.RefreshImages();
    }

    public int GetAmount()
    {
        return amount;
    }
}

public enum Seed_type
{
    copper,
    iron,
    tin,
    adamantite,
    coal,
    cobalt,
    concrete,
    granite,
    mithril,
    orichalum,
    silver,
    tungsten
}