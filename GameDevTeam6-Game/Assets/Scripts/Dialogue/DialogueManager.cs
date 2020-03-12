using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Text subtextText;
    private Text dialogueText;

    private Animator animator;

    public Queue<Sentence> sentences;
    private Sentence currentSentence = null;

    void Awake()
    {
        sentences = new Queue<Sentence>();
        animator = GameObject.Find("DialogueBox").GetComponent<Animator>();
        subtextText = GameObject.Find("subtext").GetComponent<Text>();
        dialogueText = GameObject.Find("Dialogue text").GetComponent<Text>();
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
