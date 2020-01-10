using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOnGameStart : MonoBehaviour
{
    public float dialogueStartTimer;

    void Start()
    {
        dialogueStartTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        dialogueStartTimer -= Time.deltaTime;

        if (dialogueStartTimer <= 0)
        {
            Dialogue dialogue = new Dialogue();
            dialogue.name = "Helper bot";
            dialogue.sentences = new string[3];
            dialogue.sentences[0] = "Hello there";
            dialogue.sentences[1] = "Welcome to the game";
            dialogue.sentences[2] = "Goodbye";

            GetComponent<DialogueManager>().StartDialogue(dialogue);

            Destroy(this);
        }
    }
}
