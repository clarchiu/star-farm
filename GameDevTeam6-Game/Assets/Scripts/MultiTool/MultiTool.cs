using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTool : MonoBehaviour
{
    ToolModes mode;
    int index;

    private void Start() {
        mode = ToolModes.defaultMode;
    }

    private void Update() {
        if (Input.GetKeyDown("f")) {
            SwitchTool();
        }
    }

    private void SwitchTool()
    {
        index -= 1;
        if (index == -1) {
            index = 4;
        }
        if (index == 0) { mode = ToolModes.defaultMode; }
        if (index == 1) { mode = ToolModes.seedsMode; }
        if (index == 2) { mode = ToolModes.buildingMode; }
        if (index == 3) { mode = ToolModes.breakMode; }
        if (index == 4) { mode = ToolModes.swordMode; }
    }

    public ToolModes GetMode()
    {
        return mode;
    }
}

public enum ToolModes
{
    defaultMode,
    seedsMode,
    buildingMode,
    breakMode,
    swordMode
}