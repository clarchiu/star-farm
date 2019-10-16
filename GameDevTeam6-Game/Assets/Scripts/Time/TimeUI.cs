using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public Text timeText;
    private TimeSystem system;

    private void Start()
    {
        system = GetComponent<TimeSystem>();
    }

    void Update()
    {
        int hour = system.getHour();
        int minute = system.getMinute();
        float seconds = system.getSeconds();

        string h = hour.ToString();
        string m = minute.ToString();

        if (hour < 10) {
            h = "0" + h.ToString();
        }
        if (minute < 10)
        {
            m = "0" + m.ToString();
        }

        timeText.text = "TIME: " + h + " : " + m;
    }
}
