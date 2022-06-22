using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    int scene=0;
    public int Acceleration;
    public bool isNebula, isFireBall;
    public static GameManager gm;

    public GameObject Warning;
    public GameObject Pause;
    int countPause = 0;

    private void Awake()
    {
        countPause = 0;
        DontDestroyOnLoad(this.gameObject);
        gm = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (countPause == 0)
            {
                countPause++;
                Time.timeScale = 0;
                Pause.SetActive(true);
            }
            else
            {
                PressResume();
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        scene++;
        Loader.Load(scene);
    }
    public void LoadCredit()
    {
        Loader.Load(6);
    }
    public void LoadUpdate()
    {
        SaveData();
        Loader.Load(5);
    }
    public void IncreaseAccele()
    {
        Acceleration++;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("scene", scene);
        PlayerPrefs.SetFloat("Acceleration", Acceleration);
        if(isNebula)
        {
            PlayerPrefs.SetInt("isNebula", 1);
        } else PlayerPrefs.SetInt("isNebula", 0);
        if (isFireBall)
        {
            PlayerPrefs.SetInt("isFireBall", 1);
        }
        else PlayerPrefs.SetInt("isFireBall", 0);
    }
    public void LoadData()
    {
        scene = PlayerPrefs.GetInt("scene");
        Acceleration = (int)PlayerPrefs.GetFloat("Acceleration");
        if (PlayerPrefs.GetInt("isNebula") == 1)
        {
            isNebula = true;
        }
        else isNebula = false;
        if (PlayerPrefs.GetInt("isFireBall") == 1)
        {
            isFireBall = true;
        }
        else isFireBall = false;
    }

    public void LoadContinue()
    {
        if (scene == 3)
        {
            scene = 0;
        }
        LoadData();
        LoadUpdate();
    }

    public void LoadSetting()
    {

    }

    public void LoadStart()
    {
        Warning.SetActive(true);
    }
    public void LoadMainMenu()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
        Loader.Load(0);
    }

    public void PressResume()
    {
        Time.timeScale = 1;
        countPause = 0;
        Pause.SetActive(false);
    }
}
