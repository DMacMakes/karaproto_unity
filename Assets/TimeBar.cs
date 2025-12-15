using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    internal RectTransform rectXform;
    internal float minWidth = 20.0f;
    //internal Vector2 localPoint;
    public RectTransform LeftHandle;
    public RectTransform RightHandle; 

    void Start()
    {
        // get child bars
        rectXform = GetComponent<RectTransform>();
        Debug.Log("TimeBar reporting. Waiting to hear back from them click targets.");
        //rectXform.
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    internal void EndTarget_BeginDrag(string whichEnd)
    {
        Debug.Log($"User has grabbed the {whichEnd} end.");
    }

    internal void EndTarget_UpdateDrag(string whichEnd, PointerEventData eventData)
    {
        Vector2 localPoint;
        //Debug.Log($"User is dragging {whichEnd} end.");
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectXform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        Debug.Log($"{localPoint.x}");

        // Resize the TimeBar
        if (whichEnd == "left")
        {
            float newWidth = 0;
            float newHandleX = 0;

            // Really I should be accounting for the offset of the mouse position from the dragrect's origin,
            // because (i think) the pointer drag is causing an initial snap of the rects to match the mouse position.
            if (localPoint.x <= -20.0f)
            {
                newWidth = (Math.Abs(localPoint.x) * 2.0f);
                newHandleX = localPoint.x - 2.0f; // adding 2 to the negative position
            } else
            {
                newWidth = 40.0f; // magic number for minimum 20 units either side of pivot.
                newHandleX = -22.0f;
            }
            // Resize the whole time bar (appearance)
            rectXform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
            // Position the drag handle
            LeftHandle.anchoredPosition = new Vector2(newHandleX, LeftHandle.anchoredPosition.y);
        } else if (whichEnd == "right")
        {
            float newWidth = 0;
            float newHandleX = 0;

            // Really I should be accounting for the offset of the mouse position from the dragrect's origin,
            // because (i think) the pointer drag is causing an initial snap of the rects to match the mouse position.
            if (localPoint.x >= 20.0f)
            {
                newWidth = (Math.Abs(localPoint.x) * 2.0f);
                newHandleX = localPoint.x + 2.0f; // adding 2 to the negative position
            }
            else
            {
                newWidth = 40.0f; // magic number for minimum 20 units either side of pivot.
                newHandleX = 22.0f;
            }
            // Resize the whole time bar (appearance)
            rectXform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
            // Position the drag handle
            RightHandle.anchoredPosition = new Vector2(newHandleX, RightHandle.anchoredPosition.y);
        }


        // move associated end of timer_bar image rectangle to wherever the pivot of the rectangle is, plus or minus 2 unit difference
        // (or however big that becomes to allow a nice click collider zone for each end of rect.
    }

    internal void EndTarget_EndDrag(string whichEnd)
    {
        Debug.Log($"User has dropped the {whichEnd} end.");
    }
}
