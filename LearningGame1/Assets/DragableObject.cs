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
    public AudioSource src;

    public AudioClip chalk1;
    public AudioClip chalk2;
    public AudioClip chalk3;
    public AudioClip chalk4;
    public AudioClip chalk5;

    private void Awake()
    {
        OriginalPosition = transform.localPosition;
    }

    private void OnMouseDown()
    {
        IsDragged = true;
        MouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ObjDragStartPosition = transform.localPosition;
        int rng = UnityEngine.Random.Range(1, 5);
        switch (rng)
        {
            case 1:
                src.clip = chalk1;
                src.Play();
                break;
            case 2:
                src.clip = chalk2;
                src.Play();
                break;
            case 3:
                src.clip = chalk3;
                src.Play();
                break;
            case 4:
                src.clip = chalk4;
                src.Play();
                break;
            case 5:
                src.clip = chalk5;
                src.Play();
                break;
            default:
                break;
        }
        DragStartCallback(this);
    }

    private void OnMouseDrag()
    {
        if (IsDragged == true)
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
