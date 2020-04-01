using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{

    public MultiTool multiTool;

    public GameObject QDfarm;
    public GameObject QDbuild;
    public GameObject QDcombat;
    public GameObject QuickDisplay;
    public GameObject QDwater;

    private ToolModes mode;

    private void Start() 
    {
        QuickDisplay.SetActive(true);
        QDfarm.SetActive(true);
        QDbuild.SetActive(false);
        QDcombat.SetActive(false);
        QDwater.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        mode = multiTool.GetMode();

        if (mode == ToolModes.farmMode)
        {

            QDfarm.SetActive(true);
            QDbuild.SetActive(false);
            QDcombat.SetActive(false);
            QDwater.SetActive(false);
        }

        else if (mode == ToolModes.buildingMode)
        {
            QDfarm.SetActive(false);
            QDbuild.SetActive(true);
            QDcombat.SetActive(false);
            QDwater.SetActive(false);
        }

        else if (mode == ToolModes.combatMode)
        {

            QDfarm.SetActive(false);
            QDbuild.SetActive(false);
            QDcombat.SetActive(true);
            QDwater.SetActive(false);
        }
        else if (mode == ToolModes.wateringMode)
        {
            QDfarm.SetActive(false);
            QDbuild.SetActive(false);
            QDcombat.SetActive(false);
            QDwater.SetActive(true);
        }

        else if (mode == ToolModes.defaultMode)
        {
            QDfarm.SetActive(false);
            QDbuild.SetActive(false);
            QDcombat.SetActive(false);
            QDwater.SetActive(false);
        }

    }

}
