using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeObject : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public float speed=10f;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    public void closing()
    {
        rb.velocity = Vector2.right * speed * Time.deltaTime;
    }
}
