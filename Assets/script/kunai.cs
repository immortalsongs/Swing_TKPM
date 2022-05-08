using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kunai : Enemies
{
    // Start is called before the first frame update
    public int speed = 50;
    public Rigidbody2D rb;
    public GameObject pl;
    void Start()
    {
        Damage = 15;
        pl= GameObject.Find("player");
        rb = this.GetComponent<Rigidbody2D>();
        Vector3 force = -transform.position + pl.transform.position;
        //Debug.Log(force);
        rb.velocity = (force).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
