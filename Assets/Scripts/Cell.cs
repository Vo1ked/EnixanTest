using cakeslice;
using UnityEngine;

public class Cell : MonoBehaviour
{
    Outline _outline;
    public float Heght { get; private set; }
    public float Width { get; private set; }
    public Vector2 Center { get; private set; }

    private void OnValidate()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Heght = mesh.bounds.size.x;
        Width = mesh.bounds.size.z;
        Center = new Vector2(mesh.bounds.extents.x, mesh.bounds.extents.z);

    }

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        if(_outline == null)
        {
            Debug.LogError("Outline class is missing!");
        }
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Heght = mesh.bounds.size.x;
        Width = mesh.bounds.size.z;
        Center = new Vector2(mesh.bounds.extents.x, mesh.bounds.extents.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.SetBorder += SetOutline;
    }

    void SetOutline(int colorIndex)
    {
        if(colorIndex > 2 || colorIndex < 0)
        {
            Debug.LogError("color invex out of range!");
            return;
        }
        _outline.color = colorIndex;
    }

}
