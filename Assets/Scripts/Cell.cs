using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public static System.Action<Cell> OnCellClick;
    public float heght { get; private set; }
    public float width { get; private set; }
    public Vector2 center { get; private set; }
    public SpawnObject objectOnCell;
    public bool placeEmpty {
        get
        {
            return _placementStage && _lineRenderer.enabled;
        }
    }

    Material _material;
    LineRenderer _lineRenderer;
    bool _placementStage;
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
        GameController.Instance.manager.placeHolder.OnSpawnObject += ShowPlaceMentCell;
        GameController.Instance.manager.placeHolder.OnObjectPlaced += PlacementStageEnd;

    }

    void SetOutline(bool isActive)
    {
        if (_placementStage) return;
        _lineRenderer.enabled = isActive;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_placementStage)
        {
            ShowCellInfo();
        }
        OnCellClick?.Invoke(this);
    }

    void ShowPlaceMentCell(SpawnObject item)
    {
        _placementStage = true;
        _lineRenderer.enabled = false;
        if (objectOnCell != null || item.size.x > heght || item.size.z > width) return;
        _lineRenderer.enabled = true;
        _lineRenderer.material.color = Color.green;

    }

    void PlacementStageEnd()
    {
        _placementStage = false;
        _lineRenderer.material.color = Color.red;
        _lineRenderer.enabled = true;
    }

    void ShowCellInfo()
    {
        Debug.LogFormat("Cell position = {0}\nCell Size = {1} x {2}",transform.localPosition,heght,width);
        if(objectOnCell != null)
        {
            Debug.LogFormat("Object info:\nname = {0}\nobject Id = {1}\nobject size = {2}",objectOnCell.name,objectOnCell.id,objectOnCell.size);
        }
    }
}
