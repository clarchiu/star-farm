using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral_item
{

    private Mineral_type type;
    private int amount;


    public Mineral_item(Mineral_type t, int amount)
    {
        type = t;
        this.amount = amount;
    }

    public Mineral_type GetMineralType()
    {
        return type;
    }

    public void AddAmount(int i)
    {
        Debug.Log("gained item");
        amount += i;
        Inventory_mineral_UI.Instance.RefreshImages();
    }

    public void RemoveAmount(int i)
    {
        amount -= i;
        Inventory_mineral_UI.Instance.RefreshImages();
    }

    public int GetAmount()
    {
        return amount;
    }
}

public enum Mineral_type
{
    copper,
    iron,
    tin
}