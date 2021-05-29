using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2Controller : MonoBehaviour
{
    [SerializeField]
    public float runSpeed = 4;
    [SerializeField]
    public float jumpSpeed = 12;
    private float shootWaitTime = 0.2f; // seconds

    [SerializeField]
    public Transform groundCheck1;
    public Animator animator { get; set; }
    public Rigidbody2D  rb { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }
    public IEnumerator routineLeash;

    private bool isShooting = false;
    private bool isGrounded = false;
    private bool isSliding = false;
    private bool isHurt = false;
    private bool isRunning = false;
    private string directionFacing = "right";

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
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
        if(Input.GetKey("q"))
        {
            if(routineLeash != null)StopCoroutine(routineLeash);
            routineLeash = ShootTimer();
            StartCoroutine(routineLeash);
            isShooting = true;
        }

        if(Input.GetKey("w"))
        {
            if(isGrounded) rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        if(Input.GetKey("f"))
        {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            if(directionFacing != "right")flip();
            directionFacing = "right";
            isRunning = true;
        } 
        else if(Input.GetKey("s"))
        {
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            if(directionFacing != "left") flip();
            directionFacing = "left";
            isRunning = true;
        } 
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            isRunning = false;
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
                    //slideAnimaiton
                }else
                {
                    if(isShooting)
                    {
                        if(isRunning)
                        {
                            animator.Play("walkShootProtoman"); 
                            //running shoot animation
                        }else
                        {
                            animator.Play("shootProtoman");
                            // idle shoot animation
                        }
                    }else
                    {
                        if(isRunning)
                        {
                            animator.Play("walkProtoman");
                            //running animation
                        }else
                        {
                            animator.Play("idleProtoman");
                            //idle animation

                        }
                    }
                }
            } else
            {
                if(isShooting)
                {
                    animator.Play("jumpShootProtoman");
                    //jump shoot animation
                }else
                {
                    animator.Play("jumpProtoman");
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

