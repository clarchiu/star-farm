using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShipInfo : MonoBehaviour
{
    private GameObject infoPanel;

    private void Start()
    {
        infoPanel = GameObject.Find("Ship info");
        infoPanel.SetActive(false);
    }

    private void OnMouseDown() {
        toggleInfoPanel();
    }

    private void toggleInfoPanel()
    {
        if (infoPanel.activeSelf)
        {
            infoPanel.SetActive(false);
        } else
        {
            infoPanel.SetActive(true);
        }
    }
}
