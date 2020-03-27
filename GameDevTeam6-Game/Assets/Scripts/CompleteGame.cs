using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteGame : MonoBehaviour
{  
    public void Ending1()
    {
        SceneManager.LoadScene("Ending1");
    }

    public void Ending2()
    {
        SceneManager.LoadScene("Ending2");
    }
}
