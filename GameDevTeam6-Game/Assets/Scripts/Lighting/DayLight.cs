using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLight : MonoBehaviour
{
    public Light theLight;
    public TimeSystem timeSystem;

    // Start is called before the first frame update
    void Start()
    {
        timeSystem = FindObjectOfType<TimeSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        int hour = timeSystem.getHour();
    }
}
