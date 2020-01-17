using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject shipInfo;
    private MultiTool multiTool;

    private Dialogue[] dialogues;
    private int numOfDialogues = 11;
    private int currentDialogue = 0;
    private TimeSystem timeSystem;

    private bool timeRunOnce = true;

    private void Awake()
    {
        multiTool = FindObjectOfType<MultiTool>();
        timeSystem = FindObjectOfType<TimeSystem>();
    }
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
            case 3: WaitForShipInteraction();
                break;
            case 4: CheckForTab();
                break;
            case 7: SetTime();
                break;
        }
    }

    public void TriggerDialogue(int dialogueID)
    {
        if (dialogueID == currentDialogue)
        {
            GetComponent<DialogueManager>().StartDialogue(dialogues[dialogueID]);
            currentDialogue++;
            Debug.Log(currentDialogue);
        }
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
        dialogues[3].sentences[0].text = "Captain! I have equipped you with your universal multi-tool. Model MIDA-X872A made from the most durable alloys ever. It is suitable for any hazardous environment in this universe!";
        dialogues[3].sentences[0].subtext = "Your multi-tool will be all thing you need for the game. Hold tab to select mode.";

        dialogues[4].initializeSentences(1);
        dialogues[4].sentences[0].text = "Sensor detecting familiar metal object. Captain! Can you please use your multi-tool to obtain a sample so I can analyze its component.";
        dialogues[4].sentences[0].subtext = "Select build mode on your multi-tool and try to break the first obstacle using right mouse button.";

        dialogues[5].initializeSentences(3);
        dialogues[5].sentences[0].text = "Analyzing unknown structure.. It appears that this unknown object has similar organic structure as the native fauna.";
        dialogues[5].sentences[1].text = "Captain! Could this unknown object be a seed of the native plants over there?";
        dialogues[5].sentences[2].text = "Your multi-tool is specifically equipped to interact with alien environment, why not try it out?";
        dialogues[5].sentences[2].subtext = "Press tab to open the multi-tool wheel and select farming mode. Right mouse click to process ground and Left mouse click to plant seed";

        dialogues[6].initializeSentences(1);
        dialogues[6].sentences[0].text = "Captain! As the alien object lay growing, you should explore the surroundings and find some more similar space rocks! I always wanted to leave this ship and see a new planet!";
        dialogues[6].sentences[0].subtext = "As you wait for the seed to grow, you can break more with your multi-tool use the mode wheel to switch back to building mode";

        dialogues[7].initializeSentences(2);
        dialogues[7].sentences[0].text = "Alert! Alert! Sensor detecting abnormal organic activity! Captain! It appears to be celestial organism that is native to this planet!";
        dialogues[7].sentences[1].text = "Oh no! It appears these creatures are hostile! Captain! There is built-in MIDA-LS close combat unit in the multi-tool, time to fight some bad guys!";
        dialogues[7].sentences[1].subtext = "Use the mode wheel to switch to combat mode";

        dialogues[8].initializeSentences(1);
        dialogues[8].sentences[0].text = "Sensor detecting more hostile movements! Captain it appears that these creatures are going after the seed!";
        dialogues[8].sentences[0].subtext = "Clear more enemies until the day comes";

        dialogues[9].initializeSentences(2);
        dialogues[9].sentences[0].text = "AI: Looks like the hostile creatures have turned into rust! We need to find out why in the future.";
        dialogues[9].sentences[1].text = "Look like the seeds you planted have matured!Try to harvest them with your multi-tool";
        dialogues[9].sentences[1].subtext = "Use farming mode to harvest resources";

        dialogues[10].initializeSentences(1);
        dialogues[10].sentences[0].text = "Captain! As the resources starting to pour in, it’s better to upgrade our storage";
        dialogues[10].sentences[0].subtext = "You can craft utilities in your ship, as the ship gets upgraded you will have access to more buildings";

    }

    private void CheckForWASD() {
        string[] keys = { "w", "a", "s", "d" };
        foreach (string k in keys) {
            if (Input.GetKeyDown(k)) {
                TriggerDialogue(2);
                break;
            }
        }
    }
    private void WaitForShipInteraction()
    {
        if (shipInfo.active)
        {
            TriggerDialogue(3);
        }
    }

    private void CheckForTab()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TriggerDialogue(4);
        }
    }

    private void SetTime()
    {
        if (timeRunOnce)
        {
            timeSystem.setHour(13);
            timeSystem.setMinute(0);
            timeRunOnce = false;
        }
        if (timeSystem.getHour() == 14)
        {
            TriggerDialogue(7);
        }

    }
}