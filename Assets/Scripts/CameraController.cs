using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    [SerializeField] float sensity = 1f;
    [SerializeField] Vector2 borderCenter = new Vector2(0, 0);
    [SerializeField] Vector2 cameraBorder = new Vector2(1, 1);
    [Space]
    [SerializeField] float zoomSensity = 1f;
    [SerializeField] Vector2 zoom = new Vector2(1, 5);
    [SerializeField] Vector2 angle = new Vector2(25, 60);
    float _currentZoomPosition;

    float _presstime = 0;
    Vector3 _pointerPosition;
    Vector2 _scrollPosition = Vector2.zero;
    Camera _camera;

    private void Awake()
    {
        _currentZoomPosition = transform.position.y / zoom.y;
        Mathf.Lerp(angle.x, angle.y, _currentZoomPosition);
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseMove();
        MouseZoom();
    }

    private void MouseMove()
    {
        if (Input.GetMouseButton(0))
        {
            _presstime += Time.deltaTime;
            if (_presstime > 0.15f && _pointerPosition != Input.mousePosition )
            {
                Vector3 direction = Input.mousePosition - _pointerPosition;
                direction.Normalize();
                Vector3 nextPosition = transform.position + new Vector3(direction.x, 0, direction.y) * sensity;
                if (PositionInBorder(nextPosition))
                {
                    transform.Translate(new Vector3(direction.x, 0, direction.y) * sensity, Space.World);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _presstime = 0;
        }
        _pointerPosition = Input.mousePosition;
    }

    void MouseZoom()
    {
        if (_scrollPosition != Input.mouseScrollDelta)
        {
            _currentZoomPosition = Mathf.Clamp(_currentZoomPosition + Input.mouseScrollDelta.y * zoomSensity, -1, 1);
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(zoom.x, zoom.y, _currentZoomPosition),transform.position.z);
            transform.eulerAngles = new Vector3( Mathf.Lerp(angle.x, angle.y, _currentZoomPosition), transform.eulerAngles.y, transform.eulerAngles.z);
        }
        _scrollPosition = Input.mouseScrollDelta;
    }

    bool PositionInBorder(Vector3 position)
    {
        Vector2 gridRangeX = new Vector2(borderCenter.x - cameraBorder.x, borderCenter.x + cameraBorder.x);
        Vector2 gridRangeZ = new Vector2(borderCenter.y - cameraBorder.y, borderCenter.y + cameraBorder.y);

        return (position.x > gridRangeX.x && position.x < gridRangeX.y) &&
                    (position.z > gridRangeZ.x && position.z < gridRangeZ.y);
    }

}
