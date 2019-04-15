using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public float heght { get; private set; }
    public float width { get; private set; }
    public Vector2 center { get; private set; }
    Material _material;
    LineRenderer _lineRenderer;
    bool colorChanged;

    private void OnValidate()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        _material = mesh.sharedMaterial;
        heght = mesh.bounds.size.x;
        width = mesh.bounds.size.z;
        center = new Vector2(mesh.bounds.extents.x, mesh.bounds.extents.z);

    }

    private void Awake()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();

        _material = mesh.sharedMaterial;
        heght = mesh.bounds.size.x;
        width = mesh.bounds.size.z;
        center = new Vector2(mesh.bounds.extents.x, mesh.bounds.extents.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.setCellLine += SetOutline;
    }

    void SetOutline(bool isActive)
    {
        _lineRenderer.enabled = isActive;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }
}
