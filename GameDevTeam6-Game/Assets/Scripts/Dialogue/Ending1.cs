using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending1 : MonoBehaviour
{

    string[] dialogues;
    int currentDialogue;
    public Text text;

    public GameObject[] backgrounds;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = new string[4];
        dialogues[0] = "Thrusters engaged at 100% level, navigation system has located the coordinates of the nearest starport. Captain! We are finally going home!";
        dialogues[1] = "Captain survived and escaped. Being the sole survivor of the containment ship he was celebrated as a hero. The escaped Specimen however, has been labeled as a low level threat and completely forgotten in the chronicles of space exploration.";
        dialogues[2] = "The Captain escaped, so goes the last hope of the planet. Unbeknownst to Captain, the Specimen is indeed of a malicious nature. Over the years the tribal civilization of the Natives has completely faded away, the once prosperous Planet is now a lifeless husk devoid of life. Now stronger and more intelligent, the Specimen sets its eyes onto the universe.";
        dialogues[3] = "The End.";

        currentDialogue = 0;
        text.text = dialogues[0];

        foreach (GameObject bg in backgrounds)
        {
            bg.active = false;
        }
        try
        {
            backgrounds[currentDialogue].active = true;
        }
        catch (System.Exception e)
        {
            Debug.Log("No background was found for " + currentDialogue);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentDialogue += 1;
            if (currentDialogue == dialogues.Length)
            {
                currentDialogue -= 1;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(TypeSentence(dialogues[currentDialogue]));
            }

            foreach (GameObject bg in backgrounds)
            {
                bg.active = false;
            }
            try
            {
                backgrounds[currentDialogue].active = true;
            }
            catch (System.Exception e)
            {
                Debug.Log("No background was found for " + currentDialogue);
            }
           


        }
    }

    IEnumerator TypeSentence(string str)
    {
        text.text = "";
        foreach (char letter in str)
        {
            text.text += letter;
            yield return null;
        }
    }
}