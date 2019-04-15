using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public new string name;
    public Vector3 center;
    public Vector3 size;



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + center, size);        
    }

}
