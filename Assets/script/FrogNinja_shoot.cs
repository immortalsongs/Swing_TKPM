using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogNinja_shoot : MonoBehaviour
{
    ninja_frog ninja_;
    // Start is called before the first frame update
    void Start()
    {
        ninja_ = this.GetComponentInChildren<ninja_frog>();
    }
    private void Update()
    {
        //Debug.Log(ninja_.Hp);
        if(ninja_.isDead())
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            ninja_.Shoot();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ninja_.count = 4;
        }
    }
}
