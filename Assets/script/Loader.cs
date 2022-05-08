using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        Loading,
        Start,
        Level2,
    }

    private static Action onLoaderCallback;

    public static void Load(int scene)
    {
        onLoaderCallback = () =>
          {
              SceneManager.LoadScene(scene);
          };
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallBack()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
