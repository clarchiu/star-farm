using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private Text timeText;
    private TimeSystem system;
    private Text showDayText;
    private int previousHour;

    private void Start()
    {
        system = GetComponent<TimeSystem>();
        timeText =  GameObject.Find("Time").GetComponent<Text>();
        showDayText = GameObject.Find("DayNumber").GetComponent<Text>();

        showDayText.enabled = false;
        previousHour = 0;
    }

    void Update()
    {
        int day = system.day;
        int hour = system.hour;
        int minute = system.minute;

        string h = hour.ToString();
        string m = minute.ToString();

        if (hour < 10) {
            h = "0" + h.ToString();
        }
        if (minute < 10)
        {
            m = "0" + m.ToString();
        }

        timeText.text = "day " + day + " " + h + "h " + m + "m";

        if (hour != previousHour && hour == 13)
        {
            ShowNight(system.day);
        }
        if (hour != previousHour && hour == 0 && day != 0)
        {
            ShowDay(system.day);
        }

        previousHour = hour;
    }

    void ShowDay(int day)
    {
        showDayText.enabled = true;
        showDayText.text = "Day " + day;
        showDayText.color = Color.white;
        StartCoroutine(HideDayText());
    }

    void ShowNight(int night)
    {
        Debug.Log("aa");
        showDayText.enabled = true;
        showDayText.text = "Night " + night;
        showDayText.color = Color.white;
        StartCoroutine(HideDayText());
    }

    IEnumerator HideDayText()
    {
        yield return new WaitForSeconds(7);
        showDayText.enabled = false;
    }
}
