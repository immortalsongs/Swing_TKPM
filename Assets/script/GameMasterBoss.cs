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
    public float speed = 100f;
    void Start()
    {
        Hp = 1000;
        Damage = 0;
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity += Vector2.down * speed * Time.deltaTime;
        yield return new WaitForSeconds(2f);
        rb.velocity += Vector2.up * speed * 4 * Time.deltaTime;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.right * speed * Time.deltaTime;
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
        Debug.Log(Hp);
    }

    public void Finish()
    {
        rb.velocity = Vector2.zero;
        StartCoroutine(finish());
    }

    IEnumerator finish()
    {
        Instantiate(finish_bullet, two.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(finish_bullet, two.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(finish_bullet, two.transform.position, Quaternion.identity);
    }
}