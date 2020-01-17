using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Sentence[] sentences;

    public void initializeSentences(int numSentences)
    {
        sentences = new Sentence[numSentences];
        for (int i = 0; i < numSentences; i++)
        {
            sentences[i] = new Sentence();
        }
    }
}
