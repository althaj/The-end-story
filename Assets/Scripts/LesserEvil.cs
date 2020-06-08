using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserEvil : MonoBehaviour
{
    public bool directionRight;
    public float speed;
    public AudioSource deathAudio;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int health = 1;

    private bool alive = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateDirection();
    }

    private void Update()
    {
        if (alive)
        {
            Vector3 movement = new Vector3();
            if (directionRight)
                movement.x = speed * Time.deltaTime;
            else
                movement.x = -speed * Time.deltaTime;
            transform.Translate(movement);
        }
    }

    public void Damage()
    {
        health--;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("Death");
        Destroy(gameObject, 1f);
        alive = false;
        deathAudio.Play();
    }

    public void SwitchDirection()
    {
        directionRight = !directionRight;
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        if (!directionRight)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

}
