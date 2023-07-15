using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{

    public List<Transform> snapPoints;
    public List<DragableObject> gameplayObj;
    public float snapRange = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        foreach (DragableObject dragable in gameplayObj) {
            dragable.DragEndedCallback = OnDragEnded;
        }

        
    }

    void OnDragEnded(DragableObject dragable)
    {
        float ClosestDistance = -1;
        Transform ClosestSnapPoint = null;

        foreach(Transform snapPoint in snapPoints)
        {
            float CurrentDistance = Vector2.Distance(dragable.transform.localPosition, snapPoint.localPosition);
            if (ClosestSnapPoint == null || CurrentDistance < ClosestDistance)
            {
                ClosestSnapPoint = snapPoint;
                ClosestDistance = CurrentDistance;
            }
        }

        if (ClosestSnapPoint != null && ClosestDistance <= snapRange)
        {
            dragable.transform.localPosition = ClosestSnapPoint.localPosition;
            //get value from dragable object
        }
    }
}
