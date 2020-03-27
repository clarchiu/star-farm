using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Selector : MonoBehaviour
{
    public GameObject selector;
    RectTransform selectPos;
    RectTransform boxPos;
    public int inventoryMode;

    public void TaskOnClick(int buttonNum)
    {
        if (buttonNum < Inventory_Seeds.Instance.items.Count&&inventoryMode == 0)
        {

            // Start is called before the first frame update

                Debug.Log("ON");
                selectPos = selector.GetComponent<RectTransform>();
                boxPos = GetComponent<RectTransform>();
                selectPos.anchoredPosition = new Vector2(boxPos.anchoredPosition.x, boxPos.anchoredPosition.y);
            

        }
        if (buttonNum < Inventory_Seeds.Instance.items.Count && inventoryMode == 1)
        {

            // Start is called before the first frame update

            Debug.Log("ON");
            selectPos = selector.GetComponent<RectTransform>();
            boxPos = GetComponent<RectTransform>();
            selectPos.anchoredPosition = new Vector2(boxPos.anchoredPosition.x, boxPos.anchoredPosition.y);


        }
    }
}
