using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GenSelector : MonoBehaviour
{

    private static GenSelector _instance;
    public static GenSelector Instance { get { return _instance; } }

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
    public Gen_type chosenSeed;
    bool selected = false;

    public void TaskOnClick(int buttonNum)
    {
        if (buttonNum == -1)
        {
            if (!selected)
            {
                chosenSeed = Inventory_gen.Instance.items[0].GetGenType();
            }
        }
        else if (buttonNum < Inventory_gen.Instance.items.Count)
        {
            chosenSeed = Inventory_gen.Instance.items[buttonNum].GetGenType();
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
