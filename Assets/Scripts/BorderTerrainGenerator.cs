using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTerrainGenerator : MonoBehaviour
{
    [SerializeField] List<SpawnObject> _terreinPrefabs;
    [SerializeField] Transform _tereinParent;
    [SerializeField] Vector2 GridSize;
    [SerializeField] float GridDelay;
    [SerializeField] float BorderSize;
    [SerializeField] float objectsIntensivity;

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
        if (GridSize.x < 0 || GridSize.y < 0)
        {
            Debug.LogError("Grid size must contain only positive value!");
            return false;
        }
        if (GridDelay < 0)
        {
            Debug.LogError("GridDelay must contain only positive value!");
            return false;
        }
        if (BorderSize < 0)
        {
            Debug.LogError("BorderSize must contain only positive value!");
            return false;
        }
        return true;
    }

    void SpawnBorder()
    {
        float leftToSpawn = objectsIntensivity;
        float TrySpawnLeft = objectsIntensivity * 3;
        float BorderHeght = GridSize.x + GridDelay + BorderSize;
        float BorderWidth = GridSize.y + GridDelay + BorderSize;
        while (leftToSpawn > 0 || TrySpawnLeft > 0)
        {
            Vector2 spawnPosition = new Vector3(Random.Range(transform.position.x - BorderHeght, transform.position.x + BorderHeght),
                                    transform.position.y,
                                    Random.Range(transform.position.z - BorderWidth, transform.position.z + BorderWidth));
            if (PositionInGrid(spawnPosition)) continue;
            SpawnObject prefab = _terreinPrefabs[Random.Range(0, _terreinPrefabs.Count)];
            if (CanSpawnObject(prefab, spawnPosition))
            {
                Instantiate(prefab, spawnPosition, Quaternion.identity, _tereinParent);
                leftToSpawn--;
            }

            TrySpawnLeft--;
        }
    }

    bool PositionInGrid(Vector3 position)
    {

        return (position.x < transform.position.x + GridSize.x + GridDelay && position.x > transform.position.x - GridSize.x + GridDelay) ||
                    (position.z < transform.position.z + GridSize.y + GridDelay && position.z > transform.position.z - GridSize.y + GridDelay);


    }


    bool CanSpawnObject(SpawnObject prefab, Vector3 spawnPoint)
    {

        if (PositionInGrid(new Vector3(spawnPoint.x + prefab.size.x, spawnPoint.y, spawnPoint.z + prefab.size.z)) ||
                            PositionInGrid(new Vector3(spawnPoint.x - prefab.size.x, spawnPoint.y, spawnPoint.z - prefab.size.z)))
        {
            return false;
        }

        if(Physics.BoxCast(spawnPoint,prefab.size,Vector3.down,out RaycastHit hit))
        {
            return false;
        }

        return true;
    }

}
