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
   
        brightness = 0.8f;
        transform.position = new Vector3(29, 29, -80);
        timeSystem = FindObjectOfType<TimeSystem>();
        

    }


    
    // Update is called once per frame
    //TODO Change this to a function that gets called in timeSystem
    void Update()
    {

        if (!timeSystem.isDay() && brightness > 0.2f)
        {
            brightness -= 0.05f * Time.deltaTime;
        }
        else if (timeSystem.isDay() && brightness < 0.8)
        {
            brightness += 0.05f * Time.deltaTime;
        }

        theLight.color = new Color(brightness, brightness, brightness, 1f);


    }

}
