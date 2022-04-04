using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maskdude : Enemies
{
    public Transform[] patrol;
    public float speed=20f;
    public Transform des;
    public int i;
    // Start is called before the first frame update
    void Start()
    {
        Hp = 50;
        Damage = 30;
        des = patrol[0];
        i = 0;
    }
    // Update is called once per frame
    void Update()
    {

        transform.position += (des.position - transform.position).normalized*speed*Time.deltaTime;


        if (Mathf.Abs(transform.position.x- des.position.x)<2)
        {
            i++;
            if (i > 1)
            {
                des = patrol[1];
                transform.localScale = new Vector3(-8, 8, 1);
                i = 0;
            }
            else
            {
                des = patrol[0];
                transform.localScale = new Vector3(8, 8, 1);
            }
        }
        if(isDead())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Animator.SetBool("isHitted", true);

        if (collision.gameObject.tag == "aHit")
        {

            TakeDamage(20);

        }
    }

        public void Attack()
    {

    }
}
