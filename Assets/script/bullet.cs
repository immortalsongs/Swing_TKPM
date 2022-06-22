using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : Enemies
{
    public float speed=30f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Damage = 30;
        rb.velocity = -transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag!="camera"&& collision.gameObject.tag != "hockAble" && collision.gameObject.tag != "aHit")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
