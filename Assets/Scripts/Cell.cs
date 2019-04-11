using cakeslice;
using UnityEngine;

public class Cell : MonoBehaviour
{
    Outline _outline;
    public float CellSquereSize = 1;
    private void Awake()
    {
        _outline = GetComponent<Outline>();
        if(_outline == null)
        {
            Debug.LogError("Outline class is missing!");
        }
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
