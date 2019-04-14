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

    [SerializeField] float _heght = 1;
    [SerializeField] float _width = 1;
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
        if (!_cellPrefabs.Any(x => x.Heght < _heght))
        {
            Debug.LogError("Heght too low for selected prefabs!");
            return false;
        }
        if (!_cellPrefabs.Any(x => x.Width < _width))
        {
            Debug.LogError("Width too low for selected prefabs!");
            return false;
        }
        return true;
    }

    void GenerateGrid()
    {
        _spawnposition = new Vector2(transform.position.x - _heght / 2, transform.position.z - _width / 2);
        _heghtLeft = _heght;
        _widthLeft = _width;
        while (_widthLeft > 0)
        {
            GenerateLine();
            _spawnposition += Vector2.up * _cellPrefabs[0].Width;
            _spawnposition = new Vector2(transform.position.x - _heght / 2, _spawnposition.y);
            _widthLeft -= _cellPrefabs[0].Width;
            _heghtLeft = _heght;
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
            if (_heghtLeft - cell.Heght < 0) return;
            if (ValidateCell(cell))
            {
                Instantiate(cell.gameObject, GetSpawnPosition(cell), Quaternion.identity, _cellsContainer);
            }
            _heghtLeft -= cell.Heght;
            _spawnposition += Vector2.right * cell.Heght;
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
        if (cell.Heght > _heghtLeft || cell.Width > _widthLeft) // border check
        {
            return false;
        }
        Vector3 center = GetSpawnPosition(cell) + Vector3.up;
        Vector3 checkbox = new Vector3(cell.Heght / 2 - 0.01f, 0.1f, cell.Width / 2 - 0.01f);
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
        Vector3 center = new Vector3(_spawnposition.x + cell.Center.x, transform.position.y, _spawnposition.y + cell.Center.y);
        return center;

    }
}

public class PerimeterSort : IComparer<Cell>
{
    public int Compare(Cell x, Cell y)
    {
        float xPerimeter = x.Width * 2 + x.Heght * 2;
        float yPerimeter = y.Width * 2 + y.Heght * 2;

        if (xPerimeter > yPerimeter) return 1;
        if (xPerimeter < yPerimeter) return -1;
        return 0;

    }
}
