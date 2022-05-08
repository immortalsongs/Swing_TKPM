using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour
{

    public GameObject speechBaloon;
    public Text text;
    int count;
    BoxCollider2D stop;
    public Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        stop = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject player= GameObject.Find("player");
        //if (player.transform.position.x < transform.position.x)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else transform.rotation = Quaternion.Euler(0, 180, 0);




    }
    IEnumerator Speech(int count)
    {
        text.text = "";
        if (count == 0)
        {
            string[] temp= new string[5];
            temp[0] = "Hey you, you finally awake";
            temp[1] = "You have been gone for a while";
            temp[2] = "Don't remember me?";
            temp[3] = "In that case";
            temp[4] = "I will let you go";
            for(int i=0;i<5;i++)
            {
                text.text = "";
                foreach (char cha in temp[i])
                {
                    yield return new WaitForSeconds(0.05f);
                    text.text += cha;
                }
                
                yield return new WaitForSeconds(2);
                
            }
            stop.isTrigger = true;
        }
        if (count==1)
        {
            string temp = "Use left mouse to swing";    
            foreach (char cha in temp)
            {
                yield return new WaitForSeconds(0.05f);
                text.text += cha;
            }
            yield return new WaitForSeconds(2);
            text.text = "";
        }
        if (count == 2)
        {
            string[] temp = new string[2];
            temp[0] = "Don't touch the traps";
            temp[1] = "I will meet you up there";
            for (int i = 0; i < 2; i++)
            {
                text.text = "";
                foreach (char cha in temp[i])
                {
                    yield return new WaitForSeconds(0.05f);
                    text.text += cha;
                }
                yield return new WaitForSeconds(2);
            }
            
        }

        if(count == 3)
        {
            string[] temp = new string[6];
            temp[0] = "Good job getting up here";
            temp[1] = "I guess it's impressive";
            temp[2] = "...";
            temp[3] = "Oh, and";
            temp[4] = "...";
            temp[5] = "Left click to shoot";
            for (int i = 0; i < 6; i++)
            {
                text.text = "";
                foreach (char cha in temp[i])
                {
                    yield return new WaitForSeconds(0.05f);
                    text.text += cha;
                }
                yield return new WaitForSeconds(2.5f);
            }
        }
        if (count == 4)
        {
            string[] temp = new string[3];
            temp[0] = "The blue one is safe";
            temp[1] = "But you can't stand still on it";
            temp[2] = "Yes you can wall jump";

            for (int i = 0; i < 2; i++)
            {
                text.text = "";
                foreach (char cha in temp[i])
                {
                    yield return new WaitForSeconds(0.05f);
                    text.text += cha;
                }
                yield return new WaitForSeconds(2);
            }

        }
    }

    private void OnBecameInvisible()
    {
        if (count == 1)
        {
            transform.position = new Vector3(-48.1f, -112.3f, 2);
            speechBaloon.SetActive(false);
            
        }

        if(count==2)
        {
            transform.position = new Vector3(-5, -114, 2);
            
        }

        if (count == 3)
        {
            transform.position = new Vector3(172.4f, 106.8f, 2);
            
        }
        if (count == 4)
        {
            transform.position = new Vector3(275, 137, 2);

        }
    }
    private void OnBecameVisible()
    {

        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        ani.SetBool("appear", true);
        yield return new WaitForSeconds(0.2f);
        speechBaloon.SetActive(true);
        StartCoroutine(Speech(count));
        count++;
    }
}
