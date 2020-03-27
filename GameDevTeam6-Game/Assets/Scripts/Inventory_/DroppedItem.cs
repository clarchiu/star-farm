using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [HideInInspector]
    public Mineral_type Mtype;
    public Seed_type Stype;
    public int typeType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (typeType == 0)
        {
            Inventory_mineral.Instance.GainItem(Mtype, 1);
        }
        if(typeType == 1)
        {
            Inventory_Seeds.Instance.GainItem(Stype, 1);
            Debug.Log("Sent seeds to Inventory_Seeds.instance...");
        }
        
        Destroy(gameObject);
    }
}
