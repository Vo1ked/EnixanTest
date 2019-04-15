using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] List<Cell> _cellPrefabs;

    [SerializeField]
    [Tooltip("can be empty")]
    Transform _cellsContainer;

    public float heght = 1;
    public float width = 1;
    float _heghtLeft;
    float _widthLeft;
    Vector2 _spawnposition;
    public void Init()
    {
        if (!PropertyValidation()) return;
        GenerateGrid();
        _cellPrefabs.Sort(new PerimeterSort());
    }



    bool PropertyValidation()
    {
        if (_cellPrefabs == null || _cellPrefabs.Count < 1)
        {
            Debug.LogError("cell Prefabs is null or empty!");
            return false;
        }
        if (!_cellPrefabs.Any(x => x.heght < heght))
        {
            Debug.LogError("Heght too low for selected prefabs!");
            return false;
        }
        if (!_cellPrefabs.Any(x => x.width < width))
        {
            Debug.LogError("Width too low for selected prefabs!");
            return false;
        }
        return true;
    }

    void GenerateGrid()
    {
        _spawnposition = new Vector2(transform.position.x - heght / 2, transform.position.z - width / 2);
        _heghtLeft = heght;
        _widthLeft = width;
        while (_widthLeft > 0)
        {
            GenerateLine();
            _spawnposition += Vector2.up * _cellPrefabs[0].width;
            _spawnposition = new Vector2(transform.position.x - heght / 2, _spawnposition.y);
            _widthLeft -= _cellPrefabs[0].width;
            _heghtLeft = heght;
        }

    }

    void GenerateLine()
    {
        Cell cell;
        while (_heghtLeft > 0)
        {
            cell = CreateRandomCell(out int index);
            while (!ValidateCell(cell))
            {
                if (index < 1)
                {
                    break;
                }
                cell = CreateRandomCell(out index, index);
            }
            if (_heghtLeft - cell.heght < 0) return;
            if (ValidateCell(cell))
            {
                Instantiate(cell.gameObject, GetSpawnPosition(cell), Quaternion.identity, _cellsContainer);
            }
            _heghtLeft -= cell.heght;
            _spawnposition += Vector2.right * cell.heght;
        }
    }

    Cell CreateRandomCell(out int index, int lowerIndex = int.MaxValue)
    {
        int randomToIndex = _cellPrefabs.Count;
        if (lowerIndex < _cellPrefabs.Count)
        {
            randomToIndex = lowerIndex;
        }
        index = Random.Range(0, randomToIndex);
        Cell cell = _cellPrefabs[index];
        return cell;
    }

    bool ValidateCell(Cell cell)
    {
        if (cell.heght > _heghtLeft || cell.width > _widthLeft) // border check
        {
            return false;
        }
        Vector3 center = GetSpawnPosition(cell) + Vector3.up;
        Vector3 checkbox = new Vector3(cell.heght / 2 - 0.01f, 0.1f, cell.width / 2 - 0.01f);
        if (Physics.BoxCast(center, checkbox, Vector3.down, out RaycastHit hit))
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// return cell position corrected by cell size
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    Vector3 GetSpawnPosition(Cell cell)
    {
        Vector3 center = new Vector3(_spawnposition.x + cell.center.x, transform.position.y, _spawnposition.y + cell.center.y);
        return center;

    }
}

public class PerimeterSort : IComparer<Cell>
{
    public int Compare(Cell x, Cell y)
    {
        float xPerimeter = x.width * 2 + x.heght * 2;
        float yPerimeter = y.width * 2 + y.heght * 2;

        if (xPerimeter > yPerimeter) return 1;
        if (xPerimeter < yPerimeter) return -1;
        return 0;

    }
}
