using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class InventorySelector: MonoBehaviour
{
    public SmelterUI smelterUI;

    // Start is called before the first frame update
    void Start()
    {

       // Button btn = GameOjbect.GetComponent<Button>();
       // btn.onClick().AddListener(TaskOnClick);


    }

    public void TaskOnClick(int buttonNum)
    {
        if (buttonNum < Inventory_mineral.Instance.items.Count)
        {
            Inventory_mineral.Instance.items[buttonNum].GetMineralType();
            Debug.Log("You h clicked the button!");
            Debug.Log(Inventory_mineral.Instance.items[buttonNum].GetMineralType().ToString());



            if (smelterUI.smelterPanel.activeSelf == true)
            {
                smelterUI.setItems(buttonNum);
            }

        }
    }
}

// Update is called once per frame
