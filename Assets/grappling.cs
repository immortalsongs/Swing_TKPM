using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappling : MonoBehaviour
{
    public Camera mainCamnera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    public Animator Animator;

    public CharacterController2D controller;
    public float runspeed = 40f;

    public float force = 5f;
    public Rigidbody2D rb;

    float horizontalMove = 0;
    bool jump = false;
    

    // Start is called before the first frame update
    void Start()
    {
        distanceJoint.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //horizontalMove = 0;
        Vector2 mousePos = (Vector2)mainCamnera.ScreenToWorldPoint(Input.mousePosition);
        
        //print(mousePos);
        mainCamnera.transform.position=new Vector3(transform.position.x,transform.position.y,-10);
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(8, 8, 1);
            }
            else if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-8, 8, 1);
            }

            Vector3 pullForce = new Vector3(mousePos.x - transform.position.x, 0);

            transform.position += (Vector3)pullForce.normalized*0.1f;
            if (rb.velocity.magnitude <= 20)
            {
                rb.velocity += (Vector2)pullForce * force;
            }
            //Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, mousePos.x - transform.position.x));
            //transform.rotation = rotation;

            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, transform.position);

            distanceJoint.connectedAnchor = mousePos;
            distanceJoint.enabled = true;

            lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
            transform.rotation = Quaternion.Euler(Vector3.up);
        }
        else
        {
            horizontalMove = Input.GetAxisRaw("Horizontal")*runspeed;
            if (horizontalMove != 0)
            {
                Animator.SetBool("isMoving", true);
            }
            else Animator.SetBool("isMoving", false);

            if (horizontalMove > 0)
            {
                transform.localScale = new Vector3(8, 8, 1);
            }
            else if (horizontalMove < 0)
            {
                transform.localScale = new Vector3(-8, 8, 1);
            }
            if (Input.GetButtonDown("Jump"))
            {
                Animator.SetBool("jump", true);
                jump = true;
            }
        }
        
        if(distanceJoint.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
        
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime,false,jump);
        jump = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.rotation = Quaternion.Euler(0,0,0);
        Animator.SetBool("jump", false);
        if (collision.gameObject.tag=="death")
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
