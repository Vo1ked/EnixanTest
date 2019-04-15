using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTerrainGenerator : MonoBehaviour
{
    [SerializeField] List<SpawnObject> _terreinPrefabs;
    [SerializeField] Transform _tereinParent;
    public Vector2 gridSize;
    [SerializeField] float _gridDelay;
    [SerializeField] float _borderSize;
    [SerializeField] float _objectsIntensivity;

    public void Init()
    {
        if (!PropertiesValidate()) return;
        SpawnBorder();
    }

    bool PropertiesValidate()
    {
        if (_terreinPrefabs == null || _terreinPrefabs.Count < 1)
        {
            Debug.LogError("prefabs list null or empty!");
            return false;
        }
        if (gridSize.x < 0 || gridSize.y < 0)
        {
            Debug.LogError("Grid size must contain only positive value!");
            return false;
        }
        if (_gridDelay < 0)
        {
            Debug.LogError("GridDelay must contain only positive value!");
            return false;
        }
        if (_borderSize < 0)
        {
            Debug.LogError("BorderSize must contain only positive value!");
            return false;
        }
        return true;
    }

    void SpawnBorder()
    {
        float leftToSpawn = _objectsIntensivity;
        float TrySpawnLeft = _objectsIntensivity * 3;
        float BorderHeght = gridSize.x / 2 + _gridDelay + _borderSize;
        float BorderWidth = gridSize.y / 2+ _gridDelay + _borderSize;
        while (leftToSpawn > 0 || TrySpawnLeft > 0)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - BorderHeght, transform.position.x + BorderHeght),
                                    transform.position.y,
                                    Random.Range(transform.position.z - BorderWidth, transform.position.z + BorderWidth));
            if (PositionInGrid(spawnPosition)) continue;
            SpawnObject prefab = _terreinPrefabs[Random.Range(0, _terreinPrefabs.Count)];
            if (CanSpawnObject(prefab, spawnPosition))
            {
                SpawnObject clone = Instantiate(prefab, spawnPosition, Quaternion.identity, _tereinParent);
                clone.transform.localScale = prefab.transform.localScale;
                leftToSpawn--;
            }

            TrySpawnLeft--;
        }
    }

    bool PositionInGrid(Vector3 position)
    {

        Vector2 gridRangeX = new Vector2(transform.position.x - (gridSize.x / 2 + _gridDelay), transform.position.x + (gridSize.x / 2 + _gridDelay));
        Vector2 gridRangeZ = new Vector2(transform.position.z - (gridSize.y / 2 + _gridDelay), transform.position.z + (gridSize.y / 2 + _gridDelay));

        return (position.x > gridRangeX.x && position.x < gridRangeX.y) &&
                    (position.z > gridRangeZ.x && position.z < gridRangeZ.y);


    }


    bool CanSpawnObject(SpawnObject prefab, Vector3 spawnPoint)
    {

        if (PositionInGrid(new Vector3(spawnPoint.x + prefab.size.x, spawnPoint.y, spawnPoint.z + prefab.size.z)) ||
                            PositionInGrid(new Vector3(spawnPoint.x - prefab.size.x, spawnPoint.y, spawnPoint.z - prefab.size.z)))
        {
            return false;
        }

        if(Physics.BoxCast(spawnPoint + Vector3.up * 50,prefab.size,Vector3.down,out RaycastHit hit))
        {
            return false;
        }

        return true;
    }

}
