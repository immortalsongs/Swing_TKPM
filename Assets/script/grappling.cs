using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class grappling : MonoBehaviour
{
    public Camera mainCamnera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    public AudioSource playerSource;
    public Animator Animator;

    public CharacterController2D controller;
    public float runspeed = 40f;

    public float force = 5f;
    public Rigidbody2D rb;

    float horizontalMove = 0;
    bool jump = false;
    public bool isFacing = true;

    bool isDashed=false;

    public Transform shootPoint;
    public GameObject bullet;

    Vector3 Checkpoint;
    bool isDead;

    int Hp = 100;
    int damage = 100;
    int NumBullet = 10;
    bool OutOfBu;
    bool isAttacking=false;

    public Text countDown;
    float countdown;

    CinemachineCameraOffset cinemachine;

    CinemachineVirtualCamera vcam;

    public Slider HPslider, Bulletslider;

    int jumpCount = 0;

    BoxCollider2D boxCol;

    int windCount = 0;

    Scene scene;


    // Start is called before the first frame update
    void Start()
    {
        OutOfBu = false;
        countdown = 0;
        distanceJoint.enabled = false;
        Checkpoint= new Vector3(-140.7f, -125.9f, 0);
        isDead = false;

        playerSource = GetComponent<AudioSource>();

        var camera = Camera.main;
        var brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
        vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;

        boxCol = gameObject.GetComponent<BoxCollider2D>();

        scene = SceneManager.GetActiveScene();
        //SoundManager.soundManager.PlaySound("start");
        //audioS.PlayOneShot(_start);
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
            Vector3 mousePos3 = (Vector2)mainCamnera.ScreenToWorldPoint(Input.mousePosition);
            //print(mousePos);
            mainCamnera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (NumBullet > 0)
                {

                    int hockMask = LayerMask.GetMask("hockAble");
                    bool check = Physics2D.Raycast(mousePos, Vector2.down, 0.5f, hockMask);


                    if (true)
                    {
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
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                distanceJoint.enabled = false;
                lineRenderer.enabled = false;
                //transform.rotation = Quaternion.Euler(Vector3.up);
            }
            else
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;
                //Debug.Log(horizontalMove);
                if (horizontalMove != 0)
                {
                    Animator.SetBool("isMoving", true);
                }
                else Animator.SetBool("isMoving", false);

                if (Input.GetButtonDown("Jump") && jumpCount<2)
                {
                    SoundManager.soundManager.PlayerSound("jump2");
                    Animator.SetBool("jump", true);
                    jump = true;
                    controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
                    jumpCount++;
                }
            }

            if (distanceJoint.enabled)
            {
                lineRenderer.SetPosition(1, transform.position);
            }

            //if (NumBullet > 0 && !OutOfBu)
            //{
            //    if (Input.GetKeyDown(KeyCode.Mouse1))
            //    {
            //        Shoot();
            //        NumBullet--;
            //        if(NumBullet==0)
            //        {
            //            OutOfBu = true;
            //            StartCoroutine(Reload());
            //        }
            //    }
            //}
            
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



        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(!isDashed)
                Dash();
        }

        if (countdown > 0)
        {
            countDown.gameObject.SetActive(true);
            
            countdown -= Time.fixedDeltaTime;
            countdown = Mathf.FloorToInt(countdown);
            int dis = (int)(countdown / 100);

            countDown.text = dis.ToString();
        }
        else countDown.gameObject.SetActive(false);

        if (rb.velocity.magnitude >= 25 && windCount==0)
        {
            playerSource.clip = SoundManager.soundManager.SoundDic["swing"];
            SoundManager.soundManager.PlayerSound("swing");
            Animator.SetBool("isAttack", true);
            isAttacking = true;
            windCount++;
        }

        if (rb.velocity.magnitude < 10)
        {
            if (playerSource.clip == SoundManager.soundManager.SoundDic["swing"] && windCount == 1)
            {
                windCount = 0;
                playerSource.Stop();
                SoundManager.soundManager.PlayerSound("endswing");


            }
        }
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime,false,false);
        jump = false;
        if (Hp <= 0)
        {
            SoundManager.soundManager.PlaySound("playerdie");
            isDead = true;
            Animator.SetBool("isDead", true);
        }
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(horizontalMove >= 0)
            transform.rotation = Quaternion.Euler(0,transform.rotation.y,0);
        else if(horizontalMove < 0)
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
        //Debug.Log(1);
        jumpCount = 0;

        SoundManager.soundManager.PlayerSound("impact");

        Animator.SetBool("isAttack", false);

        Animator.SetBool("jump", false);
        isDashed = false;

        if (collision.collider.tag == "aHit")
        {
            SoundManager.soundManager.PlayerSound("ahit");
            Enemies enemy = collision.gameObject.GetComponent<Enemies>();
            if (!isAttacking)
            { 
                //Debug.Log(enemy.Damage);
                Hp -= enemy.Damage;
                //Debug.Log(Hp);
                StartCoroutine(GetHit());
            }else
            {
                Debug.Log(enemy.Hp);
                enemy.TakeDamage(damage);
            }
        }
        if (collision.gameObject.tag == "death")
        {
            if (!isAttacking)
            {
                Hp -= 50;
                StartCoroutine(GetHit());
            }
        }
        if(collision.gameObject.tag=="Finish")
        {
            Animator.SetBool("goToDoor", true);
        }
        isAttacking = false;
    }

    public void Dead()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        Animator.SetBool("isDead", false);
        if(scene.name=="Boss")
        {
            transform.position = new Vector3(transform.position.x, 3.8f, 0);
            Hp = 100;
            isDead = false;
            return;
        }
        transform.position = Checkpoint;
        Hp = 100;
        isDead = false;
    }

    //IEnumerator Reload()
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        yield return new WaitForSeconds(1);
    //        NumBullet++;
    //    }
    //    OutOfBu = false;
    //}
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
            Enemies e = collision.GetComponent<Enemies>();
            Hp -= e.Damage;
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
        if (collision.gameObject.name == "gateControl")
        {

            countdown = 0;
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }
    void Dash()
    {
        SoundManager.soundManager.PlayerSound("hit");
        boxCol.enabled = false;
        Animator.SetBool("isDash", true);
        rb.gravityScale = 0;
        rb.AddForce(Vector2.right*horizontalMove*5f);
        isDashed = true;
    }
    public void endDash()
    {
        Animator.SetBool("isDash", false);
        boxCol.enabled = true;
        rb.gravityScale = 2;
    }

    public void LoadNextLevel()
    {
        GameManager.gm.LoadNextLevel();
        Animator.SetBool("goToDoor", false);
    }

}
