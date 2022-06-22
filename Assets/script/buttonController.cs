using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    public Animator buttonator;
    public Animator gateator;
    public GameObject zoomin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            buttonator.SetBool("pressed", true);
            StartCoroutine(gateControll());
            collision.gameObject.transform.position = new Vector3(-5.3f, -2.33f, 0);
        }
    }
    IEnumerator gateControll()
    {
        gateator.SetBool("open", true);
        yield return new WaitForSeconds(120);
        gateator.SetBool("open", false);
    }    
}
