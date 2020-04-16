using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private GameObject shipInfo;
    private MultiTool multiTool;

    private Dialogue[] dialogues;
    private int numOfDialogues = 27;
    private int currentDialogue = 0;
    private TimeSystem timeSystem;

    [SerializeField] private GameObject tutorialPrompt;

    private static Tutorial _instance;

    public static Tutorial Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Tutorial>();
            }
            if (_instance == null)
            {
                Debug.Log("Tutorial script not found!, Add resource manager prefab to your scene!");
            }
            return _instance;
        }
    }

    //Singleton
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
    }

    public void DisablePrompt()
    {
        tutorialPrompt.SetActive(false);
    }

    public void StartDialogue()
    {

        TriggerDialogue(0);
        TriggerDialogue(1);
    }

    private void Update()
    {
        switch(currentDialogue)
        {
            case 2: CheckForWASD();
                break;
            case 4: CheckForTab();
                break;
            case 7: WaitForNight();
                break;
            case 8: WaitForCombatMode();
                break;
            case 9: WaitForDay();
                break;
        }
    }

    public void TriggerDialogue(int dialogueID)
    {
        if (dialogueID == currentDialogue)
        {
            GetComponent<DialogueManager>().StartDialogue(dialogues[dialogueID]);
            currentDialogue++;
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
        dialogues[2].sentences[0].subtext = "This is your ship, [Left click] on ship to interact. Here you can upgrade your abilities or fix the ship, combine minerals and make new items.";

        dialogues[3].initializeSentences(1);
        dialogues[3].sentences[0].text = "Captain! I have equipped you with your universal multi-tool. Model MIDA-X872A made from the most durable alloys ever. It is suitable for any hazardous environment in this universe!";
        dialogues[3].sentences[0].subtext = "Your multi-tool will be all thing you need for the game. Hold [Tab] to select mode.";

        dialogues[4].initializeSentences(1);
        dialogues[4].sentences[0].text = "Sensor detecting familiar metal object. Captain! Can you please use your multi-tool to obtain a sample so I can analyze its component.";
        dialogues[4].sentences[0].subtext = "Select build mode (top left) on your multi-tool and try to break the first obstacle using right mouse button. You can also cycle through modes by pressing [Space key]";

        dialogues[5].initializeSentences(3);
        dialogues[5].sentences[0].text = "Analyzing unknown structure.. It appears that this unknown object has similar organic structure as the native fauna.";
        dialogues[5].sentences[1].text = "Captain! Could this unknown object be a seed of the native plants over there?";
        dialogues[5].sentences[2].text = "Your multi-tool is specifically equipped to interact with alien environment, why not try it out?";
        dialogues[5].sentences[2].subtext = "Press [E] to open your inventory, once you have selected the seed you want to plant, use your multi tool to select farming mode. [Left click] to process ground and plant seeds";

        dialogues[6].initializeSentences(1);
        dialogues[6].sentences[0].text = "Captain! As the alien object lay growing, you should explore the surroundings and find some more similar space rocks! I always wanted to leave this ship and see a new planet!";
        dialogues[6].sentences[0].subtext = "As you wait for the seed to grow, you can break more with your multi-tool use the mode wheel to switch back to building mode";

        dialogues[7].initializeSentences(2);
        dialogues[7].sentences[0].text = "Alert! Alert! Sensor detecting abnormal organic activity! Captain! It appears to be celestial organism that is native to this planet!";
        dialogues[7].sentences[1].text = "Oh no! It appears these creatures are hostile! Captain! There is built-in MIDA-LS close combat unit in the multi-tool, time to fight some bad guys!";
        dialogues[7].sentences[1].subtext = "Use the mode wheel to switch to combat mode Left click for melee, right click for ranged attack";

        dialogues[8].initializeSentences(1);
        dialogues[8].sentences[0].text = "Sensor detecting more hostile movements! Captain it appears that these creatures are going after the seed!";
        dialogues[8].sentences[0].subtext = "Clear more enemies until the day comes";

        dialogues[9].initializeSentences(2);
        dialogues[9].sentences[0].text = "Looks like the hostile creatures have all disappeared!";
        dialogues[9].sentences[1].text = "Look like the seed you planted is maturing! Try to harvest them with your multi-tool when they fully mature";
        dialogues[9].sentences[1].subtext = "Once the plants are fully matured, use farming mode and [Right click] to harvest resources from the plant ";

        dialogues[10].initializeSentences(1);
        dialogues[10].sentences[0].text = "Captain! As the resources starting to pour in, it’s better to upgrade our ship";
        dialogues[10].sentences[0].subtext = "By clicking on the ship, you can upgrade it with resources you collect.";

        dialogues[11].initializeSentences(1);
        dialogues[11].sentences[0].text = "You will need to continue upgrading the ship to return back to Earth"; ;
        dialogues[11].sentences[0].subtext = "You will need to continue upgrading the ship to return back to Earth";

        dialogues[12].initializeSentences(2);
        dialogues[12].sentences[0].text = "Looks like these natives are relentless, I wonder what disturbed them.";
        dialogues[12].sentences[1].text = "Grunt: Must….Consume…..";

        dialogues[13].initializeSentences(2);
        dialogues[13].sentences[0].text = "Grunt: The sorcerer has conjured up a Golem…";
        dialogues[13].sentences[1].text = "Alert! Alert! Sensor detecting powerful energy clusters";

        dialogues[14].initializeSentences(1);
        dialogues[14].sentences[0].text = "Captain! You need to defeat the Golem!";
        dialogues[14].sentences[0].subtext = "Defeat the golem";

        dialogues[15].initializeSentences(1);
        dialogues[15].sentences[0].text = "Captain! Now the smelter system has been restored! The smelter makes you mix metals into more advanced materials!";

        dialogues[16].initializeSentences(1);
        dialogues[16].sentences[0].text = "With the advanced materials that just acquired, we can make some defenses!";

        dialogues[17].initializeSentences(1);
        dialogues[17].sentences[0].text = "With the advanced materials that just acquired, we can make some defenses!";
        dialogues[17].sentences[0].subtext = "Now we have material to build walls and turrets. Cement can be used to build walls while other metals can be made into defensive turrets";

        dialogues[18].initializeSentences(1);
        dialogues[18].sentences[0].text = "These materials radiate cosmic energy, we could make more powerful tools with this. We can improve your multi-tool to be even more powerful";

        dialogues[19].initializeSentences(1);
        dialogues[19].sentences[0].text = "Sensor detects spikes of energy signal, the natives seem to be gathering for a big attack!";

        dialogues[20].initializeSentences(1);
        dialogues[20].sentences[0].text = "You brought it here… We must survive";

        dialogues[21].initializeSentences(1);
        dialogues[21].sentences[0].text = "It seems that the Natives are not only here for our resources, why are they becomes so aggressive towards us?";

        dialogues[22].initializeSentences(1);
        dialogues[22].sentences[0].text = "You.. despicable..outsider";

        dialogues[23].initializeSentences(1);
        dialogues[23].sentences[0].text = "Captain! Sensor detects these twirling pools of materials contain powerful energy! This is nothing we have encountered before.";

        dialogues[24].initializeSentences(1);
        dialogues[24].sentences[0].text = "The Natives are getting desperate. Their attacks have become increasingly ferocious. We need to repair the ship as soon as possible.";

        dialogues[25].initializeSentences(1);
        dialogues[25].sentences[0].text = "Captain! It looks like the quantity of the Chromatic Metal is very limited. We do not have enough to upgrade our tools and fixing the ship altogether!";
        dialogues[25].sentences[0].subtext = "You can choose to upgrade your ship to the final tier and finish the game or choosing to upgrade your multi-tool weapon in order to defeat the escaped Specimen.";

        //If choose to upgrade ship, following won't play

        dialogues[26].initializeSentences(1);
        dialogues[26].sentences[0].text = "But… we need these materials to repair the ship. The ship is our only chance of getting out of here! Captain! We…";

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

    private void CheckForTab()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TriggerDialogue(4);
        }
    }

    private void WaitForNight()
    {
        if (!timeSystem.isDay())
        {
            TriggerDialogue(7);
        }
    }

    private void WaitForDay()
    {
        if (timeSystem.isDay())
        {
            TriggerDialogue(9);
        }
    }

    private void WaitForCombatMode()
    {
        if (multiTool.GetMode() == ToolModes.combatMode)
        {
            TriggerDialogue(8);
        }
    }
}