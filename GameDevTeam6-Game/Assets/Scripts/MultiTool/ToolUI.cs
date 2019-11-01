using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{
    public Text toolText;
    private MultiTool multiTool;

    private void Start()
    {
        multiTool = FindObjectOfType<MultiTool>();
    }

    void Update()
    {
        toolText.text = multiTool.GetMode().ToString();
    }
}
