using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninja_frog : Enemies
{
    public GameObject player;
    public GameObject kunai;
    public GameObject shootPoint;
    SpriteRenderer sp;

    public int count = 4;
    // Start is called before the first frame update
    void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        sp.enabled = false;
        Hp = 50;
        Damage = 0;
        count = 4;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("player");
        //Debug.Log((player.transform.position-transform.position).magnitude);
        if (player.transform.position.x < transform.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else transform.rotation = Quaternion.Euler(0, 0, 0);
        //if (isDead())
        //{
        //    Destroy(gameObject);
        //}
    }



    IEnumerator shoot(GameObject player)
    {
        //Debug.Log(count);
        if (count < 0)
        {
            yield break;
        }
        Vector3 objectPos = transform.position;
        Vector3 targ = player.transform.position;
        targ.z = 0f;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        Instantiate(kunai, shootPoint.transform.position, Quaternion.identity);
        count--;
        //Debug.Log(count);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(shoot(player));
    }

    public void Shoot()
    {

        sp.enabled = true;

        StartCoroutine(shoot(player));
    }

}
