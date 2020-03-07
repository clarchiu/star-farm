﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public Mineral_type type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inventory_mineral.Instance.GainItem(type, 1);
        Destroy(gameObject);
    }
}