using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<UIManager>().GoToNextLevel();
        }
    }
}
