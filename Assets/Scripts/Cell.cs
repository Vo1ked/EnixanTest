﻿using UnityEngine;

public class Cell : MonoBehaviour
{
    public float Heght { get; private set; }
    public float Width { get; private set; }
    public Vector2 Center { get; private set; }
    Material _material;
    LineRenderer _lineRenderer;
    private void OnValidate()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        _material = mesh.sharedMaterial;
        Heght = mesh.bounds.size.x;
        Width = mesh.bounds.size.z;
        Center = new Vector2(mesh.bounds.extents.x, mesh.bounds.extents.z);

    }

    private void Awake()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();

        _material = mesh.sharedMaterial;
        Heght = mesh.bounds.size.x;
        Width = mesh.bounds.size.z;
        Center = new Vector2(mesh.bounds.extents.x, mesh.bounds.extents.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.SetCellLine += SetOutline;
    }

    void SetOutline(bool isActive)
    {
        _lineRenderer.enabled = isActive;
    }

}
