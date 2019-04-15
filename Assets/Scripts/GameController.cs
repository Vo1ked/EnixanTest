using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public System.Action<bool> setCellLine;
    public GameSceneManager manager;
    bool _gridEnabled = true;

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

    public void Start()
    {
        InitGameScene();
    }

    private void InitGameScene()
    {
        manager.InitScene();
        manager.uiManager.gridButton.onClick.AddListener(SwicthGrid);

    }

    void SwicthGrid()
    {
        _gridEnabled = !_gridEnabled;
        setCellLine?.Invoke(_gridEnabled);
    }

}
