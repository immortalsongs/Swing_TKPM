using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> SoundList;
    public Dictionary<string, AudioClip> SoundDic;
    public AudioSource audioSource;
    AudioSource playerSource;
    public static SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {

        playerSource = GameObject.Find("player").GetComponent<AudioSource>();
    
        SoundDic = new Dictionary<string, AudioClip>();
        soundManager = this;
        SoundDic.Add("start", SoundList[0]);
        SoundDic.Add("danger", SoundList[1]);
        SoundDic.Add("alter", SoundList[2]);
        SoundDic.Add("jump", SoundList[3]);
        SoundDic.Add("hit", SoundList[4]);
        SoundDic.Add("chill", SoundList[5]);
        SoundDic.Add("swing", SoundList[6]);
        SoundDic.Add("jump2", SoundList[7]);
        SoundDic.Add("respawn", SoundList[8]);
        SoundDic.Add("endswing", SoundList[9]);
        SoundDic.Add("ahit", SoundList[10]);
        SoundDic.Add("impact", SoundList[11]);
        SoundDic.Add("playerdie", SoundList[11]);


    }

    public void PlaySound(string key)
    {
        audioSource.PlayOneShot(SoundDic[key]);
    }
    public void PlayerSound(string key)
    {
        playerSource.PlayOneShot(SoundDic[key]);
    }
}
