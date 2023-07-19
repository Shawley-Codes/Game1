using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    private bool IsDragged = false;
    private Vector3 MouseDragStartPosition;
    public Vector3 ObjDragStartPosition;
    public Vector3 OriginalPosition;

    public delegate void DragEndedDelagate(DragableObject dragableObject);
    public delegate void DragStartDelagate(DragableObject dragableObject);
    public DragEndedDelagate DragEndedCallback;
    public DragStartDelagate DragStartCallback;
    public int DragSpeedMultiplier = 50;

    private void Awake()
    {
        OriginalPosition = transform.localPosition;
    }


    private void OnMouseDown()
    {
        IsDragged = true;
        MouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ObjDragStartPosition = transform.localPosition;
        DragStartCallback(this);
    }

    private void OnMouseDrag()
    {
        if (IsDragged = true)
        {
            transform.localPosition = ObjDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - MouseDragStartPosition) * DragSpeedMultiplier;
        }
    }

    private void OnMouseUp()
    {
        IsDragged = false;
        DragEndedCallback(this);
    }
}
