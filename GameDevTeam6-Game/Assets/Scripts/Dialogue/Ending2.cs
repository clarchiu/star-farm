using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending2 : MonoBehaviour
{

    string[] dialogues;
    int currentDialogue;
    public Text text;

    public GameObject[] backgrounds;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = new string[3];
        dialogues[0] = "With all the Chromatic Metal used up. The ship simply can not be repaired. The incident had no survivor, not a single soul knows what " +
            "happened after the ship’s communication went dark. The escaped Specimen was labeled as a low level threat and the loss of life was simply disregarded, for the great space exploration there will always be someone to fill in.";
        dialogues[1] = "The Specimen was finally defeated. but the Captain also used up the last of his strength. He gazes upon the cosmos above him, knowing fully " +
            "well that his crews are avenged and his mission completed. Without resources and power the Captain slowly succumbs to his wounds. The AI however," +
            " still lies dormant being left on the planet silently recording everything.";
        dialogues[2] = "The End.";

        currentDialogue = 0;
        text.text = dialogues[0];

        foreach (GameObject bg in backgrounds)
        {
            bg.SetActive(false);
        }
        try
        {
            backgrounds[currentDialogue].SetActive(true);
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