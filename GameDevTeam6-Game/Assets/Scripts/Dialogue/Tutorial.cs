using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Dialogue[] dialogues;
    private int numOfDialogues = 10;
    private int currentDialogue = 0;

    private void Start()
    {
        dialogues = new Dialogue[numOfDialogues];
        for (int i = 0; i < numOfDialogues; i++) {
            dialogues[i] = new Dialogue();
        }

        InitializeDialogues();

        TriggerDialogue(0);
        TriggerDialogue(1);
    }

    private void Update()
    {
        switch(currentDialogue)
        {
            case 2: CheckForWASD();
                    break;
        }
    }

    public void TriggerDialogue(int dialogueID)
    {
        if (dialogueID == currentDialogue)
        {
            GetComponent<DialogueManager>().StartDialogue(dialogues[dialogueID]);
        }
        currentDialogue++;
    }

    private void InitializeDialogues()
    {
        dialogues[0].initializeSentences(3);
        dialogues[0].sentences[0].text = "Alert! Alert! ! Detecting dropping Oxygen density…";
        dialogues[0].sentences[1].text = "Diagnosing ship damage…. Activating emergency airlock!";
        dialogues[0].sentences[2].text = "Captain! You are finally awake!";

        dialogues[1].initializeSentences(2);
        dialogues[1].sentences[0].text = "Hi Captain! It appears that you have been in a coma for the past [REDACTED] Earth Days.";
        dialogues[1].sentences[1].text = "Now try to move your arms and legs so I can monitor your health condition!";
        dialogues[1].sentences[1].subtext = "Press WASD to move";

        dialogues[2].initializeSentences(1);
        dialogues[2].sentences[0].text = "Captain! If you don’t remember, this heap of wreck is your ship! My sensors shows the overall damage is 84%! I am the only part working right now!";
        dialogues[2].sentences[0].subtext = "This is your ship, left click to interact.";

        dialogues[3].initializeSentences(1);
        dialogues[3].sentences[0].text = "Captain! This is your universal multi-tool. Model MIDA-X872A made from the most durable alloys ever. It is suitable for any hazardous environment in this universe!";
        dialogues[3].sentences[0].subtext = "Your multi-tool will be all thing you need for the game, there are many modes. Hold tab to select mode";

        dialogues[4].initializeSentences(1);
        dialogues[4].sentences[0].text = "Sensor detecting familiar metal object. Captain! Can you please use your multi-tool to obtain a sample so I can analyze its component.";
        dialogues[4].sentences[0].subtext = "Select build mode on your multi-tool and try to break the first obstacle using right mouse button";

        dialogues[5].initializeSentences(3);
        dialogues[5].sentences[0].text = "Analyzing unknown structure.. It appears that this unknown object has similar organic structure as the native fauna.";
        dialogues[5].sentences[1].text = "Captain!Could this unknown object be a seed of the native plants over there?";
        dialogues[5].sentences[2].text = "Your multi-tool is specifically equipped to interact with alien environment, why not try it out?";
        dialogues[5].sentences[2].subtext = "Press tab to open the multi-tool wheel, select farming mode and plant the seed on the processed ground";

        dialogues[6].initializeSentences(1);
        dialogues[6].sentences[0].text = "Captain!As the alien object lay growing, you should explore the surroundings and find some more similar space rocks! I always wanted to leave this ship and see a new planet!";
        dialogues[6].sentences[0].subtext = "As you wait for the seed to grow, you can break more with your multi-tool use the mode wheel to switch back to building mode";

    }

    private void CheckForWASD() {
        string[] keys = { "w", "a", "s", "d" };
        foreach (string k in keys) {
            if (Input.GetKeyDown(k)) {
                TriggerDialogue(2);
                TriggerDialogue(3);
                break;
            }
        }
    }
}