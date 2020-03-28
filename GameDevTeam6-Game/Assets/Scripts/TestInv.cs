using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInv : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        Inventory_mineral.Instance.GainItem(Mineral_type.coal, 10);
        Inventory_mineral.Instance.GainItem(Mineral_type.tin, 10);
        Inventory_mineral.Instance.GainItem(Mineral_type.cobalt, 10);
        Inventory_mineral.Instance.GainItem(Mineral_type.chromatic1, 10);
        Inventory_mineral.Instance.GainItem(Mineral_type.mithril, 10);
        Inventory_mineral.Instance.GainItem(Mineral_type.iron, 10);
        Inventory_mineral.Instance.GainItem(Mineral_type.steel, 10);
        Inventory_mineral.Instance.GainItem(Mineral_type.tartarite, 10);
        Inventory_mineral.Instance.GainItem(Mineral_type.copper, 1000);
        Inventory_mineral.Instance.GainItem(Mineral_type.tin, 100);
        Inventory_mineral.Instance.GainItem(Mineral_type.iron, 100);
        Inventory_mineral.Instance.GainItem(Mineral_type.bronze, 100);
    }
}
