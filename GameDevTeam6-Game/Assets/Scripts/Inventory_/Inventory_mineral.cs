using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_mineral : MonoBehaviour
{
    List<Mineral_item> items;

    public void gainItem(Mineral_item item)
    {
        foreach (Mineral_item i in items)
        {
            if (i.type == item.type)
            {
                i.amount += item.amount;
                return;
            }
        }
        //If not found
        items.Add(item);
    }
}
