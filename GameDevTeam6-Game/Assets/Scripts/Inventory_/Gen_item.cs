using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen_item
{
    // Start is called before the first frame update
    private Gen_type type;
    private int amount;

    public Gen_item(Gen_type t, int amount)
    {
        type = t;
        this.amount = amount;
    }

    public Gen_type GetGenType()
    {
        return type;
    }

    public void AddAmount(int i)
    {
        amount += i;
        Inventory_general_UI.Instance.RefreshImages();
    }

    public void RemoveAmount(int i)
    {
        amount -= i;
        Inventory_general_UI.Instance.RefreshImages();
    }

    public int GetAmount()
    {
        return amount;
    }
}

public enum Gen_type
{
   wall,
   turret,
}

