using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1Controller : MonoBehaviour
{
    [SerializeField]
    public float runSpeed = 4;
    [SerializeField]
    public float jumpSpeed = 12;
    public float slideSpeed = 6;
    private float shootWaitTime = 0.2f; // seconds

    [SerializeField]
    public Transform groundCheck;
    public Animator animator { get; set; }
    public Rigidbody2D  rb { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }
    public IEnumerator routineLeash;
    public BoxCollider2D boxCollider;

    private bool isShooting = false;
    private bool isGrounded = false;
    private bool isSliding = false;
    private bool isHurt = false;
    private bool isRunning = false;
    private string directionFacing = "left";

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        
    }

    void FixedUpdate()
    {
        
        /*
        jump
        jump shoot

        run
        run shoot

        stand
        stand shoot



        */
        // if(Input.GetButtonDown("Fire1"))

        if(Input.GetKey("down") && Input.GetKey("/"))
        {
            isSliding = true;
            if(directionFacing == "right")
            {
                rb.velocity = new Vector2(slideSpeed, rb.velocity.y);
            }else 
            {
                rb.velocity = new Vector2(-slideSpeed, rb.velocity.y);
            }
        }
        else if(Input.GetKey("right"))
        {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            if(directionFacing != "right")flip();
            directionFacing = "right";
            isRunning = true;
            isSliding = false;
        } 
        else if(Input.GetKey("left"))
        {
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            if(directionFacing != "left") flip();
            directionFacing = "left";
            isRunning = true;
            isSliding = false;
        } 
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            isRunning = false;
            isSliding = false;
        }

         if(Input.GetKey("."))
        {
            if(routineLeash != null)StopCoroutine(routineLeash);
            routineLeash = ShootTimer();
            StartCoroutine(routineLeash);
            isShooting = true;
        }

        if(Input.GetKey("/") && !isSliding)
        {
            if(isGrounded) rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        
        // if(Input.GetKey("q"))







        if(isHurt)
        {
            //hurt animation
        }else
        {
            if(isGrounded)
            {
                if(isSliding)
                {
                    animator.Play("slideMegaman");
                    boxCollider.size = new Vector2(boxCollider.size.x, 0.75f);
                    rb.position = new Vector2(rb.position.x, rb.position.y - 0.375f);
                    //slideAnimaiton
                }else
                {
                    boxCollider.size = new Vector2(boxCollider.size.x, 1.5f);
                    // rb.position = new Vector2(rb.position.x, rb.position.y + 0.75f);
                    if(isShooting)
                    {
                        if(isRunning)
                        {
                            animator.Play("walkShootMegaman"); 
                            //running shoot animation
                        }else
                        {
                            animator.Play("shootMegaman");
                            // idle shoot animation
                        }
                    }else
                    {
                        if(isRunning)
                        {
                            animator.Play("walkMegaman");
                            //running animation
                        }else
                        {
                            animator.Play("idleMegaman");
                            //idle animation

                        }
                    }
                }
            } else
            {
                if(isShooting)
                {
                    animator.Play("jumpShootMegaman");
                    //jump shoot animation
                }else
                {
                    animator.Play("jumpMegaman");
                    //jump animation
                }
            }
        }
        
        
        
        
        
        

    }
    private void flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }
    private IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(shootWaitTime);
        isShooting = false;
        
    }
    public GameObject deathEffect;
    private int health = 100;
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if(health <= 0) Die();
    }
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collidedObject)
    {
        Debug.Log(collidedObject.gameObject.name);
        if(collidedObject.gameObject.layer == 3) {
        isGrounded = true;
        FindObjectOfType<audioManager>().play("Landing");
        }
    }

    void OnCollisionExit2D(Collision2D collidedObject)
    {
        if(collidedObject.gameObject.layer == 3) isGrounded = false;
    }
}

