using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShipInfo : MonoBehaviour
{
    public GameObject infoPanel;

    private void Awake()
    {
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
