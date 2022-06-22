using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateZone : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject e;
    public Animator UpdateTable;

    int inRange=0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&& inRange==1)
        {
            UpdateTable.SetBool("pressE", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            e.SetActive(true);
            inRange = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            e.SetActive(false);
            inRange = 0;
        }
    }
    public void PressClose()
    {
        UpdateTable.SetBool("pressE", false);
    }
}
