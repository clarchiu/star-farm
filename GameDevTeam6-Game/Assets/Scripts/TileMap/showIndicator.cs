using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showIndicator : MonoBehaviour
{
    private MultiTool tool;
    private GameObject indicator;

    private void Awake()
    {
        tool = FindObjectOfType<MultiTool>();
        indicator = GameObject.FindWithTag("indicator");
    }

    // Update is called once per frame
    void Update()
    {
        if (tool.GetMode() == ToolModes.buildingMode || tool.GetMode() == ToolModes.farmMode)
        {
            indicator.SetActive(true);
        } else
        {
            indicator.SetActive(false);
        }
    }
}
