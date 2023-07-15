using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    private bool IsDragged = false;
    private Vector3 MouseDragStartPosition;
    private Vector3 ObjDragStartPosition;
    public Vector3 OriginalPosition;

    public delegate void DragEndedDelagate(DragableObject dragableObject);
    public DragEndedDelagate DragEndedCallback;

    private void Awake()
    {
        OriginalPosition = transform.localPosition;
    }


    private void OnMouseDown()
    {
        IsDragged = true;
        MouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ObjDragStartPosition = transform.localPosition;

    }

    private void OnMouseDrag()
    {
        if (IsDragged = true)
        {
            transform.localPosition = ObjDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - MouseDragStartPosition) * 50;
        }
    }

    private void OnMouseUp()
    {
        IsDragged = false;
        DragEndedCallback(this);
    }
}
