using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

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
    public bool isFacing = true;

    public Transform shootPoint;
    public GameObject bullet;

    Vector3 Checkpoint;
    bool isDead;

    int Hp = 100;
    int NumBullet = 4;
    bool OutOfBu;

    public Text countDown;
    float countdown;

    CinemachineCameraOffset cinemachine;

    CinemachineVirtualCamera vcam;

    public Slider HPslider, Bulletslider;

    // Start is called before the first frame update
    void Start()
    {
        OutOfBu = false;
        countdown = 0;
        distanceJoint.enabled = false;
        Checkpoint= new Vector3(-140.7f, -125.9f, 0);
        isDead = false;

        var camera = Camera.main;
        var brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
        vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;

    }

    // Update is called once per frame
    void Update()
    {
        HPslider.value = Hp;
        Bulletslider.value = NumBullet;
        if (!isDead)
        {
            //horizontalMove = 0;
            Vector2 mousePos = (Vector2)mainCamnera.ScreenToWorldPoint(Input.mousePosition);

            //print(mousePos);
            mainCamnera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //if (rb.velocity.x > 0)
                //{
                //    transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                //}
                //else if (rb.velocity.x < 0)
                //{
                //    transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                //}

                Vector3 pullForce = new Vector3(mousePos.x - transform.position.x, 0);

                transform.position += (Vector3)pullForce.normalized * 0.1f;
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
                //transform.rotation = Quaternion.Euler(Vector3.up);
            }
            else
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;
                if (horizontalMove != 0)
                {
                    Animator.SetBool("isMoving", true);
                }
                else Animator.SetBool("isMoving", false);

                if (Input.GetButtonDown("Jump"))
                {
                    Animator.SetBool("jump", true);
                    jump = true;
                }
            }

            if (distanceJoint.enabled)
            {
                lineRenderer.SetPosition(1, transform.position);
            }

            if (NumBullet > 0 && !OutOfBu)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Shoot();
                    NumBullet--;
                    if(NumBullet==0)
                    {
                        OutOfBu = true;
                        StartCoroutine(Reload());
                    }
                }
            }
            
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position= new Vector3(-140.7f, -125.9f, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = new Vector3(185.0585f, 109.5f, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = new Vector3(163, 206, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.position = new Vector3(322, 320, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            transform.position = new Vector3(485, 109.6f, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            transform.position = new Vector3(603, 224, 0);
        }


        if (countdown > 0)
        {
            countDown.gameObject.SetActive(true);
            
            countdown -= Time.fixedDeltaTime;
            countdown = Mathf.FloorToInt(countdown);
            int dis = (int)(countdown / 100);
            int mili= (int)(countdown % 100);
            countDown.text = string.Format("{0:00}:{1:00}", dis, mili);
        }
        else countDown.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime,false,jump);
        jump = false;
        if (Hp <= 0)
        {
            isDead = true;
            StartCoroutine(Dead());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(horizontalMove >= 0)
            transform.rotation = Quaternion.Euler(0,transform.rotation.y,0);
        else if(horizontalMove < 0)
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);

        Animator.SetBool("jump", false);
        if (collision.gameObject.tag == "aHit")
        {
            Enemies enemy = collision.gameObject.GetComponent<Enemies>();
            //Debug.Log(enemy.Damage);
            Hp -= enemy.Damage;
            //Debug.Log(Hp);
            StartCoroutine(GetHit());
        }
        if (collision.gameObject.tag == "death")
        {
            Hp = 0;
        }

    }

    IEnumerator Dead()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        Animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        Animator.SetBool("isDead", false);
        transform.position = Checkpoint;
        Hp = 100;
        isDead = false;
    }

    IEnumerator Reload()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(1);
            NumBullet++;
        }
        OutOfBu = false;
    }
    IEnumerator GetHit()
    {
        //Debug.Log("hit!");
        Animator.SetBool("isHitted", true);
        yield return new WaitForSeconds(0.5f);

        Animator.SetBool("isHitted", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Animator.SetBool("isHitted", true);

        if (collision.gameObject.tag == "aHit")
        {

            Hp -= 20;
            StartCoroutine(GetHit());
            
        }

        if (collision.gameObject.tag == "checkpoint")
        {
            Checkpoint = collision.transform.position;
        }

        if (collision.gameObject.tag == "death")
        {
            Hp = 0;
        }

        if(collision.gameObject.tag=="zoom out")
        {
            while(vcam.m_Lens.OrthographicSize < 30)
                vcam.m_Lens.OrthographicSize += Time.deltaTime;
        }
        if (collision.gameObject.tag == "zoom in")
        {
            while (vcam.m_Lens.OrthographicSize > 12)
                vcam.m_Lens.OrthographicSize -= Time.deltaTime;
        }
        if (collision.gameObject.name=="button1")
        {
            
            countdown = 3100;
        }

    }

    public void Shoot()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }

}
