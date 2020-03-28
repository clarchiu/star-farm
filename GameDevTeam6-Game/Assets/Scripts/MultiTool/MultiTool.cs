 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiTool : MonoBehaviour
{

    private ToolModes mode;

    private MultiTool multiTool;
    private GameObject wheel;
    private Image topLeft;
    private Image topRight;
    private Image bottomLeft;
    private Image bottomRight;
    private Image currentImage;
    private Image selectedImage;

    private Color32 blue;
    private Color32 darkBlue;
    private Color32 white;
    //creates audiosource
    AudioSource multitool_open;
    public AudioClip multitool_sound;



    private void Start() {
        mode = ToolModes.defaultMode;

        multiTool = FindObjectOfType<MultiTool>();
        wheel = GameObject.FindWithTag("wheel");
        topLeft = wheel.transform.Find("topLeft").GetComponent<Image>();
        topRight = wheel.transform.Find("topRight").GetComponent<Image>();
        bottomLeft = wheel.transform.Find("bottomLeft").GetComponent<Image>();
        bottomRight = wheel.transform.Find("bottomRight").GetComponent<Image>();

        currentImage = null;
        selectedImage = null;

        blue = new Color32(182, 205, 242, 190);
        darkBlue = new Color32(34, 90, 200, 190);
        white = new Color32(255, 255, 255, 190);
        resetColors();

        wheel.SetActive(false);
        //gets component on multitool object
        multitool_open = GetComponent<AudioSource>();

    }

    public ToolModes GetMode()
    {
        return mode;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);  //convert game object position to VievportPoint

            resetColors();
            if (viewportPoint.x > 0.5f && viewportPoint.y > 0.5f)
            {
                currentImage = topRight;
            }
            else if (viewportPoint.x < 0.5f && viewportPoint.y > 0.5f)
            {
                currentImage = topLeft;
            }
            else if (viewportPoint.x > 0.5f && viewportPoint.y < 0.5f)
            {
                currentImage = bottomRight;
            }
            else if (viewportPoint.x < 0.5f && viewportPoint.y < 0.5f)
            {
                currentImage = bottomLeft;
            }
            else
            {
                currentImage = null;
            }

            if (currentImage != null)
            {
                currentImage.color = blue;
            }
            if (selectedImage != null)
            {
                selectedImage.color = darkBlue;
            }
            if (Input.GetMouseButtonDown(0))
            {
                selectedImage = currentImage;
                SoundEffects_.Instance.PlaySoundEffect(SoundEffect.button);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SoundEffects_.Instance.PlaySoundEffect(SoundEffect.button);
            //plays sound when multitool opened
            multitool_open.PlayOneShot(multitool_sound, 0.5f);
            wheel.SetActive(true);
            //mode = ToolModes.defaultMode;
        }
        else if (Input.GetKeyUp("tab"))
        {
            wheel.SetActive(false);
            if (selectedImage == topLeft)
            {
                mode = ToolModes.buildingMode;
            }
            else if (selectedImage == topRight)
            {
                mode = ToolModes.farmMode;
            }
            else if (selectedImage == bottomLeft)
            {
                mode = ToolModes.combatMode;
            }
            else if (selectedImage == bottomRight)
            {
                mode = ToolModes.wateringMode;
            }

        }

        // Cycles through tools with a Q
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SoundEffects_.Instance.PlaySoundEffect(SoundEffect.button);
            mode += 1;
            if (mode == ToolModes.end)
                mode = ToolModes.defaultMode;

            if (mode == ToolModes.farmMode)
            {
                selectedImage = topRight;
                //QDBuildTool.alpha = 1;
            }

            else if (mode == ToolModes.buildingMode)
            {
                selectedImage = topLeft;
            }

            else if (mode == ToolModes.wateringMode)
            {
                selectedImage = bottomRight;
            }
            else if (mode == ToolModes.combatMode)
            {

                selectedImage = bottomLeft;
            }
            else
            {
                selectedImage = null;
            }
        }

        //Cylces backwards with E
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SoundEffects_.Instance.PlaySoundEffect(SoundEffect.button);
            mode -= 1;
            if (mode == ToolModes.start)
                mode = ToolModes.wateringMode;

            if (mode == ToolModes.farmMode)
            {
                selectedImage = topRight;
                //QDBuildTool.alpha = 1;
            }
            else if (mode == ToolModes.buildingMode)
            {
                selectedImage = topLeft;
            }

            else if (mode == ToolModes.wateringMode)
            {
                selectedImage = bottomRight;
            }
            else if (mode == ToolModes.combatMode)
            {

                selectedImage = bottomLeft;
            }
            else
            {
                selectedImage = null;
            }
        }




        if (Input.GetKey(KeyCode.Tab))
        {
            Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);  //convert game object position to VievportPoint
            resetColors();
            if (viewportPoint.x > 0.5f && viewportPoint.y > 0.5f) {
                currentImage = topRight;
            } else if (viewportPoint.x < 0.5f && viewportPoint.y > 0.5f) {
                currentImage = topLeft;
            } else if (viewportPoint.x > 0.5f && viewportPoint.y < 0.5f) {
                currentImage = bottomRight;
            } else if (viewportPoint.x < 0.5f && viewportPoint.y < 0.5f) {
                currentImage = bottomLeft;
            } else {
                currentImage = null;
            }

            if (currentImage != null) {
                currentImage.color = blue;
            }
            if (selectedImage != null)
            {
                selectedImage.color = darkBlue;
            }

            if (Input.GetMouseButtonDown(0))
            {
                selectedImage = currentImage;

            }


        }
    }

    private void resetColors()
    {
        topLeft.color = white;
        topRight.color = white;
        bottomLeft.color = white;
        bottomRight.color = white;
        if (selectedImage != null)
        {
            selectedImage.color = darkBlue;
        }
    }

}

public enum ToolModes
{
    start,
    defaultMode,
    buildingMode,
    farmMode,
    combatMode,
    wateringMode,
    end
}
