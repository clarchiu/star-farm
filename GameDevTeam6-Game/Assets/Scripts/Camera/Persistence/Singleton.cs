using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    [SerializeField] private GameObject objectClass = null;

    void Awake()
    {
        Object[] objects = FindObjectsOfType(objectClass.GetType());
        if (objects.Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
