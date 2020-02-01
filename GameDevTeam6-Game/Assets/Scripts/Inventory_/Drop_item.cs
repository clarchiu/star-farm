using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_item : MonoBehaviour
{
    [SerializeField]
    private GameObject dropItem;


    private static Drop_item _instance;
    public static Drop_item Instance { get { return _instance; } }

    //Singleton
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

     }

    public void DropItem(Mineral_type type, float x, float y)
    {
        GameObject obj = Instantiate(dropItem);
        obj.transform.position = new Vector2(x, y);
        obj.GetComponent<SpriteRenderer>().sprite = ItemInfo.Instance.GetSprite(type);
    }



}
