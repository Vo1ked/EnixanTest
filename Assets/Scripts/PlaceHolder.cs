using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    public Vector2 gridSize;
    SpawnObject _objectToMove;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SpawnItem(SpawnObject item)
    {
        _objectToMove = Instantiate(item.gameObject, transform.position, Quaternion.identity).GetComponent<SpawnObject>();
        _objectToMove.transform.localScale = item.transform.localScale;
    }
}
