using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnapController : MonoBehaviour
{

    public List<Transform> snapPoints;
    public List<DragableObject> gameplayObj;
    public float snapRange = 0.5f;
    public List<Transform> activePoints;
    public Dictionary<Transform, string> valueDict = new Dictionary<Transform, string>();
    public List<Transform> occupiedSnapPoints = new List<Transform>();
    int offset = 20;

    // Start is called before the first frame update
    public void SetDragables(ArrayList activeSnaps)
    {
        //set which snaps are active
        foreach (GameObject snaps in activeSnaps)
        {
            activePoints.Add(snaps.transform);

        }
        //create delegate for letting go of objects
        foreach (DragableObject dragable in gameplayObj) {
            dragable.DragEndedCallback = OnDragEnded;
            dragable.DragStartCallback = OnDragStart;
        }
    }

    //remove snaps from occupied list if moved
    private void OnDragStart(DragableObject dragable)
    {
        Transform CurrentSnapPoint = null;
        if(dragable.OriginalPosition != dragable.transform.localPosition)
        {
            foreach(Transform snapPoint in activePoints)
            {
                Vector3 snap = snapPoint.transform.localPosition;
                snap.x += offset;
                if (dragable.ObjDragStartPosition == snap)
                {
                    CurrentSnapPoint = snapPoint;
                }
            }
        }
        if (CurrentSnapPoint != null)
        {
            valueDict.Remove(CurrentSnapPoint);
            occupiedSnapPoints.Remove(CurrentSnapPoint);
        }
    }

    void OnDragEnded(DragableObject dragable)
    {
        float ClosestDistance = -1;
        Transform ClosestSnapPoint = null;

        //set snap points
        foreach(Transform snapPoint in activePoints)
        {
            float CurrentDistance = Vector2.Distance(dragable.transform.localPosition, snapPoint.localPosition);
            if (ClosestSnapPoint == null || CurrentDistance < ClosestDistance)
            {
                ClosestSnapPoint = snapPoint;
                ClosestDistance = CurrentDistance;
            }
        }

        if (ClosestSnapPoint != null && ClosestDistance <= snapRange && !occupiedSnapPoints.Contains(ClosestSnapPoint))
        {
            Vector3 snapTo = ClosestSnapPoint.localPosition;
            snapTo.x += offset;
            dragable.transform.localPosition = snapTo;

            TMP_Text childText = dragable.GetComponentInChildren<TMP_Text>();
            if (childText != null)
            {
                string snapPointValue = childText.text;
                valueDict.Add(ClosestSnapPoint, snapPointValue);
                //Debug.Log(ClosestSnapPoint + "," + snapPointValue);
            }
            //Update occupied status
            occupiedSnapPoints.Add(ClosestSnapPoint);

            //get value from dragable object
        } else
        {
            dragable.transform.localPosition = dragable.OriginalPosition;
        }
    }

    public void Reset()
    {

        occupiedSnapPoints.Clear();
        activePoints.Clear();
        valueDict.Clear();
    }
}
