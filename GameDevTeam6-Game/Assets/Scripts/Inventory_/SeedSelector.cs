using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class SeedSelector : MonoBehaviour
{

    private static SeedSelector _instance;
    public static SeedSelector Instance { get { return _instance; } }

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


    private void Update()
    { 
       // Debug.Log(chosenSeed);
    }
    RectTransform selectRect;
    RectTransform imgRect;
    public Seed_type chosenSeed;
    bool selected = false;

    public void TaskOnClick(int buttonNum)
    {
        if (buttonNum == -1)
        {
            if (!selected)
            {
                chosenSeed = Inventory_Seeds.Instance.items[0].GetSeedType();
                selected = true;
            }
        }
        else if (buttonNum < Inventory_Seeds.Instance.items.Count)
        {
            chosenSeed = Inventory_Seeds.Instance.items[buttonNum].GetSeedType();
        }
        selected = true;
    }
}
/*
       if (buttonNum<Inventory_Seeds.Instance.items.Count)
        {
            chosenSeed = Inventory_Seeds.Instance.items[buttonNum].GetSeedType();
Debug.Log("You h clicked the button!");
            Debug.Log(Inventory_Seeds.Instance.items[buttonNum].GetSeedType().ToString());
            selectRect = selector.GetComponent<RectTransform>();
            imgRect = chosenImg.GetComponent<RectTransform>();
            selectRect.anchoredPosition = new Vector2(imgRect.anchoredPosition.x, imgRect.anchoredPosition.y);
        }*/