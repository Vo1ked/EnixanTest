using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : ASceneManager
{
    public GridGenerator gridGenerator;
    public BorderTerrainGenerator borderTerrainGenerator;

    private void Awake()
    {
        GameController.Instance.manager = this;
    }

    private void Start()
    {
        gridGenerator.Init();
        borderTerrainGenerator.gridSize = new Vector2(gridGenerator.heght, gridGenerator.width);
        borderTerrainGenerator.Init();
    }

    public override void InitScene()
    {
        gridGenerator.Init();
        borderTerrainGenerator.Init();
    }

}
