using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterUI : MonoBehaviour
{
    public GameObject smelterPanel;

    private GameObject item1;
    private GameObject item2;
    private void Awake()
    {
        Debug.Log(this.gameObject.name);
        smelterPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        toggleInfoPanel();
    }

    private void toggleInfoPanel()
    {
        if (smelterPanel.activeSelf)
        {
            smelterPanel.SetActive(false);
        }
        else
        {
            smelterPanel.SetActive(true);
        }
        item1 = this.transform.GetChild(0);
        item2 = this.transform.GetChild(1);

    }
}