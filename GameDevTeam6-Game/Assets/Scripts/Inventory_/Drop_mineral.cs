using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_mineral : MonoBehaviour
{
    public Mineral_type type;
    private float offset = 0.3f;

    public void DropItem(float x, float y)
    {
        GameObject obj = Instantiate(ResourceManager.Instance.dropItem);
        obj.transform.position = new Vector2(x + Random.Range(-offset, offset), y + Random.Range(-offset, offset));
        obj.GetComponent<DroppedItem>().type = type;
        obj.GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.GetMineralSprite(type);
    }
}
