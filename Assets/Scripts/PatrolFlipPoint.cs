using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFlipPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PatrolEnemy"))
        {
            col.GetComponent<LesserEvil>().SwitchDirection();
        }
    }
}
