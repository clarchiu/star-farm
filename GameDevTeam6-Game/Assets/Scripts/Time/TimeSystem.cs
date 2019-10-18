using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public float speed;
    private int hour;
    private int minute;
    private float seconds;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        minute = 0;
        hour = 0;
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime * speed;

        if (seconds >= 60)
        {
            minute += 1;
            seconds -= 60;
        }
        if (minute >= 60)
        {
            hour += 1;
            minute -= 60;
        }
        if (hour >= 24)
        {
            hour -= 24;
        }
    }

    public int getHour()
    {
        return hour;
    }

    public int getMinute()
    {
        return minute;
    }

    public float getSeconds()
    {
        return seconds % 60;
    }
}
