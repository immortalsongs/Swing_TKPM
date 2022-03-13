using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject map1;
    Vector3 pla;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject pl = GameObject.Find("player");
        pla = pl.transform.position;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("1");
        transform.position = pla + (pla - transform.position);
        Instantiate(map1, transform.position, Quaternion.identity);
    }


}
