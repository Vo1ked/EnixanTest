using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public System.Action<int> SetBorder;
    public ASceneManager Manager;

    #region Singelton

    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameController").AddComponent<GameController>();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    #endregion


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Game");
        SceneManager.sceneLoaded += InitGameScene;
    }

    private void InitGameScene(Scene arg0, LoadSceneMode arg1)
    {
        Manager.InitScene();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetBorder?.Invoke(0);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetBorder?.Invoke(2);
        }
    }
}
