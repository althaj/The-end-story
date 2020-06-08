using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject uiPanel;

    private void Start()
    {
        uiPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            uiPanel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            uiPanel.SetActive(false);
        }
    }
}
