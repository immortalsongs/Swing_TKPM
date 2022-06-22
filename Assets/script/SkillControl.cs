using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillControl : MonoBehaviour
{
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
        transform.parent.parent.GetComponent<grappling>().OnTriggerEnterChild();
        if (collision.gameObject.tag=="death")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "aHit")
        {
            Enemies enemy = collision.GetComponent<Enemies>();
            enemy.TakeDamage(25);
        }
    }
}
