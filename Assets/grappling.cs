using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappling : MonoBehaviour
{
    public Camera mainCamnera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    public float force = 5f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        distanceJoint.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = (Vector2)mainCamnera.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(rb.velocity.magnitude);
        //print(mousePos);
        mainCamnera.transform.position=new Vector3(transform.position.x,transform.position.y,-10);
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 pullForce = new Vector2(mousePos.x - transform.position.x, 0);

            transform.position += (Vector3)pullForce.normalized*0.1f;
            if (rb.velocity.magnitude <= 20)
            {
                rb.velocity += pullForce * force;
            }

            

            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, transform.position);

            distanceJoint.connectedAnchor = mousePos;
            distanceJoint.enabled = true;

            lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
        }
        
        if(distanceJoint.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="death")
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
