using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : ASceneManager
{
    public GridGenerator GridGenerator;

    private void Awake()
    {
        GameController.Instance.Manager = this;
    }

    private void Start()
    {
        GridGenerator.Init();
    }

    public override void InitScene()
    {
        GridGenerator.Init();
    }

}
