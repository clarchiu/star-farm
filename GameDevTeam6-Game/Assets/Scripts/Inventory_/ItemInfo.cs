using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo: MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites;
    private static Dictionary<string, Sprite> map;

    private static ItemInfo _instance;
    public static ItemInfo Instance { get { return _instance; } }

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

        SetupMap();
    }

    private void SetupMap()
    {
        map = new Dictionary<string, Sprite>();

        string[] PieceTypeNames = System.Enum.GetNames(typeof(Mineral_type));
        for (int i = 0; i < PieceTypeNames.Length; i++)
        {
            if (i < sprites.Count)
            {
                map.Add(PieceTypeNames[i], sprites[i]);
            } else
            {
                throw new System.Exception("Not all sprites are set for each mineral type!! Check ItemInfo in Inventory controller!");
            }
        }
    }

    public Sprite GetSprite(Mineral_type type)
    {
        Sprite sprite = map[type.ToString()];
        if (sprite == null)
        {
            throw new System.Exception("No sprite corresponding to this type");
        }
        return sprite;
    }




}
