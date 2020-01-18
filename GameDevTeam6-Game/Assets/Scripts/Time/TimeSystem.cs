using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public float speed;
    public int hour;
    public int minute;
    public float seconds;

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

    public void setHour(int h)
    {
        hour = h;
    }
    public void setMinute(int m)
    {
        minute = m;
    }
    public void setSeconds(int s)
    {
        seconds = s;
    }

}
