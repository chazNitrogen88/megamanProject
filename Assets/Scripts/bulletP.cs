using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletP : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 5;
    void Start()
    {
        rb.velocity = transform.right * speed;
        // StartCoroutine(delayedDestruction());
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        // Debug.Log(hitInfo.ToString());
        // var target = hitInfo.GetComponent<player1Controller>();
        // hitInfo.takeDamage(1);
        // target.takeDamage(1);
        player1Controller megaMan = hitInfo.GetComponent<player1Controller>();
        if (megaMan != null)
        {
            megaMan.TakeDamage(damage);
        }
        Destroy(gameObject);
        // StartCoroutine(delayedDestruction());
    }
    // private IEnumerator delayedDestruction()
    // {
        // yield return new WaitForSeconds(1f);
    // }
}
