using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text subtextText;
    public Text dialogueText;

    public Animator animator;

    public Queue<Sentence> sentences;
    private Sentence currentSentence = null;

    // create audiosource named AI_talking_audiosource
    AudioSource AI_talking_audiosource;
   

    void Awake()
    {
        sentences = new Queue<Sentence>();
        // grabs the audiosource component for AI_talking_audiosource
        AI_talking_audiosource = GetComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        bool startDialogue = false;
        if (sentences.Count == 0 && currentSentence == null)
        {
            startDialogue = true;
           
        }


        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        if (startDialogue)
        {
            
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        // plays audio from the audiosource component on the dialogue manager 
        AI_talking_audiosource.Play();
        subtextText.text = "";
        currentSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(Sentence sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        displaySubtext(sentence);
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        currentSentence = null;
    }

    void displaySubtext(Sentence sentence)
    {
        subtextText.text = sentence.subtext;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }
}
