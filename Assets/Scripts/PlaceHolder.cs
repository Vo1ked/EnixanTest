using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    public Vector2 gridSize;
    SpawnObject _objectToMove;
    public System.Action<SpawnObject> OnSpawnObject;
    public System.Action OnObjectPlaced;
    int objectCounter = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SpawnItem(SpawnObject item)
    {
        _objectToMove = Instantiate(item.gameObject, transform.position, Quaternion.identity).GetComponent<SpawnObject>();
        _objectToMove.transform.localScale = item.transform.localScale;
        Cell.OnCellClick += SetObject;
        OnSpawnObject?.Invoke(_objectToMove);

    }

    void SetObject(Cell cell)
    {
        if (cell.placeEmpty)
        {
            _objectToMove.transform.SetParent(cell.transform);
            _objectToMove.transform.localPosition = Vector3.zero;
            _objectToMove.id = objectCounter;
            cell.objectOnCell = _objectToMove;
            objectCounter++;
            OnObjectPlaced?.Invoke();
            Cell.OnCellClick -= SetObject;
            _objectToMove = null;
        }
    }

}
