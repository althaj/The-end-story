using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speed;
    public float jumpStrength;

    public AudioSource footsteps;
    public AudioSource jumpAudio;
    public AudioSource swordAudio;
    public AudioSource deathAudio;

    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private LayerMask groundLayer;
    private LayerMask enemyLayer;

    private float horizontalMovement;
    private bool jump = false;
    private bool isGrounded;
    private Vector3 force;

    private bool alive = true;
    private bool attacking = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        if (!attacking)
        {
            if (alive)
            {
                horizontalMovement = Input.GetAxisRaw("Horizontal");
                jump = Input.GetAxisRaw("Vertical") > 0;

                if (horizontalMovement > 0)
                    spriteRenderer.flipX = false;
                if (horizontalMovement < 0)
                    spriteRenderer.flipX = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                Attack();
        } else
        {
            horizontalMovement = 0;
            jump = false;
        }
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
            isGrounded = raycastHit.collider != null;

            if (isGrounded)
            {
                force.x = horizontalMovement * speed;
                force.y = jump ? jumpStrength : 0;

                if (jump)
                    jumpAudio.Play();
            }
            else
            {
                force.x = horizontalMovement * speed / 2;
                force.y = 0;
            }

            if (force.sqrMagnitude > 0)
                rb2d.AddForce(force);

            animator.SetFloat("Horizontal velocity", Mathf.Abs(rb2d.velocity.x));
            animator.SetFloat("Vertical velocity", rb2d.velocity.y);
            animator.SetBool("Grounded", isGrounded);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Death") || col.gameObject.CompareTag("PatrolEnemy"))
        {
            alive = false;
            animator.SetBool("Dead", true);
            deathAudio.Play();
            FindObjectOfType<UIManager>().Die();
        }
    }

    private void Attack()
    {
        animator.SetBool("Attack", true);
    }

    public void StartAttack()
    {
        swordAudio.pitch = Random.Range(1.2f, 1.6f);
        swordAudio.Play();
        attacking = true;
    }

    public void EndAttack()
    {
        attacking = false;
        animator.SetBool("Attack", false);
    }

    public void PerformAttack()
    {
        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position + Vector3.up * 0.5f, direction, 1.5f, enemyLayer);
        foreach(RaycastHit2D hit in raycastHits)
        {
            if (hit.transform.GetComponent<LesserEvil>() != null)
                hit.transform.GetComponent<LesserEvil>().Damage();
        }
    }

    public void PlayFootstepSound()
    {
        footsteps.Play();
    }

}
