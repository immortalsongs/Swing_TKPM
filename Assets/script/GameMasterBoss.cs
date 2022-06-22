using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterBoss : Enemies
{
    // Start is called before the first frame update

    public GameObject bullet;
    public GameObject one, two, three;
    public GameObject finish_bullet;
    Rigidbody2D rb;
    public float speed = 101f;
    public GameObject Explosion;
    public GameObject Blast;
    public Animator ani;
    int count = 0;
    public GameObject hub;

    void Start()
    {
        Explosion.SetActive(false);
        Blast.SetActive(false);
        Hp = 2000;
        Damage = 10;
        rb = GetComponent<Rigidbody2D>();
        count = 0;
    }
    private void Update()
    {
        if(isDead()&& count==0)
        {
            hub.SetActive(false);
            rb.velocity = Vector2.zero;
            StopAllCoroutines();
            Explosion.SetActive(true);
            StartCoroutine(dead());
            count++;
            //Dead();
        }
    }
    public void ShootAtOne()
    {
        StartCoroutine(shot(one));
    }
    public void ShootAtTwo()
    {
        StartCoroutine(shot(two));
    }
    public void ShootAtThree()
    {
        StartCoroutine(shot(three));
    }

    IEnumerator shot(GameObject point)
    {
        Instantiate(bullet, point.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        Instantiate(bullet, point.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        Instantiate(bullet, point.transform.position, Quaternion.identity);
    }
    IEnumerator move()
    {
        //rb.velocity += Vector2.down * speed * Time.deltaTime;
        yield return new WaitForSeconds(1f);
        //rb.velocity += Vector2.up * speed * 4 * Time.deltaTime;
        //yield return new WaitForSeconds(0.5f);
        //rb.velocity = Vector2.right * speed * Time.deltaTime;
    }
    public void closing()
    {
        rb.velocity = Vector2.right * speed * Time.deltaTime;
    }
    public void moving()
    {
        StartCoroutine(move());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void Finish()
    {
        rb.velocity = Vector2.zero;
        StartCoroutine(finish());
    }

    IEnumerator finish()
    {
        Instantiate(finish_bullet, two.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.8f);
        Instantiate(finish_bullet, two.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.8f);
        Instantiate(finish_bullet, two.transform.position, Quaternion.identity);
    }
    IEnumerator dead()
    {

        yield return new WaitForSeconds(1f);
        Blast.SetActive(true);
        ani.SetBool("appear", true);
        yield return new WaitForSeconds(2f);
        GameManager.gm.LoadCredit();
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(1);
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log(2);
    //        ani.SetBool("isHit", true);
    //    }
    //}
    public void endHit()
    {
        ani.SetBool("isHit", false);
    }
}