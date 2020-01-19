using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    string[] dialogues;
    int currentDialogue;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = new string[10];
        dialogues[0] = "It is the 23rd century, and humans on the earth have reached the capacity of interstellar travel.";
        dialogues[1] = "You are the Captain of the Arcadia-class containment ship thats tasked to capture and retrieve an alien lifeform sample from the unknown galaxy";
        dialogues[2] = "...";
        dialogues[3] = "Crew I: Come in control, this is designation 1172. We have successfully captured lifeform X-1, it is carnivorous and highly aggressive but we have managed to contain it.";
        dialogues[4] = "Control: Affirmative 1172, sending you the coordination of the nearest cosmic port.";
        dialogues[5] = "Crew I: Coordinates received, just sit tight and we will deliver the creature in no time.";
        dialogues[6] = "...";
        dialogues[7] = "Crew I: Its breaking out!";
        dialogues[8] = "Crew II: KILL IT!";
        dialogues[9] = "Crew III: The ship is losing power";

        currentDialogue = 0;
        text.text = dialogues[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

            currentDialogue += 1;
            if (currentDialogue == 10)
            {
                SceneManager.LoadScene("RandyScene");
            }
            else
            {
                StartCoroutine(TypeSentence(dialogues[currentDialogue]));
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
