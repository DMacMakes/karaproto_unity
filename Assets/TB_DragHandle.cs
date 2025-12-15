using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TimeBar timeBar;
    public string whichEnd;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Called when drag starts
        timeBar.EndTarget_BeginDrag(whichEnd);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Called every frame while dragging
        timeBar.EndTarget_UpdateDrag(whichEnd, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Called when mouse is released or focus is lost
        timeBar.EndTarget_EndDrag(whichEnd);
    }
}