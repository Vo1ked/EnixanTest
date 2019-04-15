using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public new string name = "GameSceneManager";
    [SerializeField] GridGenerator _gridGenerator;
    public GridGenerator gridGenerator { get => _gridGenerator; }

    [SerializeField] BorderTerrainGenerator _borderTerrainGenerator;
    public BorderTerrainGenerator borderTerrainGenerator { get => _borderTerrainGenerator; }

    [SerializeField] PlaceHolder _placeHolder;
    public PlaceHolder placeHolder { get => _placeHolder; }

    [SerializeField]  UiManager _uiManager;
    public UiManager uiManager { get => _uiManager; }

    private void Awake()
    {
        GameController.Instance.manager = this;
    }


    public void InitScene()
    {
        _gridGenerator.Init();
        _borderTerrainGenerator.gridSize = _placeHolder.gridSize = new Vector2(_gridGenerator.heght, _gridGenerator.width);
        _borderTerrainGenerator.Init();
    }

}
