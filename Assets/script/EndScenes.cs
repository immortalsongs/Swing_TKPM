using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScenes : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(End());
    }
    IEnumerator End()
    {
        yield return new WaitForSeconds(25f);
        GameManager.gm.LoadMainMenu();
    }
}
