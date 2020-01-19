using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daylight : MonoBehaviour
{
    public Light theLight;
    public TimeSystem timeSystem;



    float brightness;
    // Start is called before the first frame update
    void Start()
    {
   
        brightness = 1f;
        transform.position = new Vector3(29, 29, -80);
        timeSystem = FindObjectOfType<TimeSystem>();
        

    }



    // Update is called once per frame
    void Update()
    {

        int hour = timeSystem.getHour();
        if (hour > 12 && hour < 14)
          {
              brightness -= 0.0001f;
          }
          if (hour > 22 && brightness < 1)
          {
              brightness += 0.00011f;
          }

          theLight.color = new Color(brightness, brightness, brightness, 1f);


    }

}
