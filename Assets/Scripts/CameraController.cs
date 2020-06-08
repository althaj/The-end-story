using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private Vector3 offset = new Vector3(0, 0.5f, -10);

    void Start()
    {
        PlayerControler pc = FindObjectOfType<PlayerControler>();
        if(pc != null)
            player = pc.transform;
    }

    void Update()
    {
        if (player != null)
            transform.position = Vector3.Lerp(transform.position, player.position + offset, 0.1f);
    }
}
