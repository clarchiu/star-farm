using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_mineral : MonoBehaviour
{
    public Mineral_type Mtype;
    private float offset = 0.3f;
    public Seed_type Stype;
    int typeType;

    public void DropItem(float x, float y)
    {
        GameObject obj = Instantiate(ResourceManager.Instance.dropItem);
        obj.transform.position = new Vector2(x + Random.Range(-offset, offset), y + Random.Range(-offset, offset));

        int drop = Random.Range(1,3);
      // Debug.Log(drop);
        if (drop == 1)
        {
            obj.GetComponent<DroppedItem>().Mtype = Mtype;
            obj.GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.GetMineralSprite(Mtype);
            obj.GetComponent<DroppedItem>().typeType = 0;
        }
        else if(drop == 2)
        {
            obj.GetComponent<DroppedItem>().Stype = Stype;
            obj.GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.GetSeedSprite(Stype);
            obj.GetComponent<DroppedItem>().typeType = 1;
        }
    }
}
