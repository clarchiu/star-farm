using System;
using UnityEngine;
public class AStarDebugger: MonoBehaviour
{
    private static AStarDebugger instance;

    public static AStarDebugger MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AStarDebugger>();
            }

            return instance;
        }
    }
}
