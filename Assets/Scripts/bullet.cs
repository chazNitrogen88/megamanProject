using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.right * speed;
        // StartCoroutine(delayedDestruction());
        
    }

    void OnTriggerEnter2D() {
        // Debug.Log(hitInfo.ToString());
        // var target = hitInfo.GetComponent<player1Controller>();
        
        // hitInfo.takeDamage(1);
        // target.takeDamage(1);
        Destroy(gameObject);
        // StartCoroutine(delayedDestruction());
    }
    // private IEnumerator delayedDestruction()
    // {
        // yield return new WaitForSeconds(1f);
    // }
}
