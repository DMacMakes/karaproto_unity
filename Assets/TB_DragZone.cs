using UnityEngine;
using UnityEngine.EventSystems;

public class DragZone : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TimeBubble timeBar;
    public string zone;
    public Texture2D cursorEastWest;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        //hotSpot = new Vector2(25, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer entered..");
        if (zone == "left" || zone == "right")
        {
            Cursor.SetCursor(cursorEastWest, hotSpot, cursorMode);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Pass 'null' to the texture parameter to use the default system cursor.
        if (zone == "left" || zone == "right")
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Called when drag starts
        timeBar.EndTarget_BeginDrag(zone, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Called every frame while dragging
        timeBar.EndTarget_UpdateDrag(zone, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Called when mouse is released or focus is lost
        timeBar.EndTarget_EndDrag(zone);
    }
}