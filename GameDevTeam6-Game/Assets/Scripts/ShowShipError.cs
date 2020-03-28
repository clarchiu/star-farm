using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowShipError : MonoBehaviour
{
    public Text text;



    private static ShowShipError _instance;
    public static ShowShipError Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ShowShipError>();
            }
            if (_instance == null)
            {
                Debug.Log("resource manager script not found!, Add resource manager prefab to your scene!");
            }
            return _instance;
        }
    }


    public void DisplayError(string errorMsg)
    {
        text.text = errorMsg;
        StartCoroutine(FadeText());
    }

    IEnumerator FadeText()
    {
        yield return new WaitForSeconds(3);
        text.text = "";
    }
}
