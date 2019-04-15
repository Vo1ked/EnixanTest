using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : ASceneManager
{
    public new string name = "GameSceneManager";
    public GridGenerator gridGenerator;
    public BorderTerrainGenerator borderTerrainGenerator;
    public UiManager UiManager;

    private void Awake()
    {
        GameController.Instance.manager = this;
    }


    public override void InitScene()
    {
        gridGenerator.Init();
        borderTerrainGenerator.gridSize = new Vector2(gridGenerator.heght, gridGenerator.width);
        borderTerrainGenerator.Init();
    }

}
