using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Update_active : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {

            }
        }
    }
}
