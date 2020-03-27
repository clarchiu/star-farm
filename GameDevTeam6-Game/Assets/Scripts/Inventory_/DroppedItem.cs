using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [HideInInspector]
    public Mineral_type type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inventory_mineral.Instance.GainItem(type, 1);
        //SoundEffects_.Instance.PlaySoundEffect(SoundEffect.collect);
        Destroy(gameObject);
    }
}
