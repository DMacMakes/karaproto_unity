using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TimeBar_Grabby : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    internal RectTransform rectXform;
    internal float minWidth = 100.0f;
    //internal Vector2 localPoint;
    public RectTransform LeftHandle;
    public RectTransform RightHandle;
    Dimensions dims;

    // A struct to contain the left and right screenspace x positions of the bar, as well as its width.
    struct Dimensions
    {
        public float left_x;
        //float _right_x;
        public float width;

        //public float left_x { get; set; }
        public float right_x
        {
            get { return (left_x + width); }
            /*set {
                // Allow setting of the right_x to calculate the new width
                // rather than requiring both to be set (redundant)
                //_right_x = value; 
                width = right_x - left_x;
            }*/
        }

        /*public float width
        {
            get { return (_width); }
            set
            {
                // Allow setting of the width to update the right_x,
                // rather than requiring both to be set (redundant)
                _width = value;
                _right_x = left_x + value;
            }
        }*/

        public override string ToString()
        {
            return $"Dims: left x: {left_x} .. width: {width} .. right x: {right_x}";
        }
        
    }

    void Start()
    {
        // get child bars
        Debug.Log("TimeBar_Grabby reporting.");
        rectXform = GetComponent<RectTransform>();

        //float newWidth = 400;
        //rectXform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        //Debug.Log($"TimeBar_Grabby resized to {newWidth} wide. Is it that big, and are the rectangles snapped to each end?");

        //rectXform.
        dims = new Dimensions();
        //dims.left_x = 
        
        
        dims.left_x = rectXform.anchoredPosition.x;
        dims.width = rectXform.rect.width;
        Debug.Log(dims);
        //Debug.Log($"Event x: {eventData.position.x}");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    internal void EndTarget_BeginDrag(string whichEnd)
    {
        Debug.Log($"User has grabbed the {whichEnd} end.");
        // Capture initial start, end and width info for timebar.
    }

    internal void EndTarget_UpdateDrag(string whichEnd, PointerEventData eventData)
    {
        // Figure out the local location of the mouse click.
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
             rectXform,
             eventData.position,
             eventData.pressEventCamera,
             out localPoint);
        Debug.Log($"Local x: {localPoint.x} .. Event x: {eventData.position.x}");
        //Debug.Log($"Event x: {eventData.position.x}");


        // If it's the left end, we need to move the rectangle's position left and right, while changing the width to keep the right end static.
        // Right end is easy, just changing width. Let's do that first.
        //float newWidth = 0.0f;
        //float newX = 0.0f;
        //float rectLastWidth = rectXform.rect.width;
        if (whichEnd == "right")
        {
            float newWidth = Math.Max(localPoint.x, minWidth); // can't take anything smaller than minimum width.
            dims.width = newWidth;
            rectXform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        } else if (whichEnd == "left")
        {
            // Gather current size and right end x location
            float oldWidth = rectXform.rect.width;
            float rightExtent = rectXform.anchoredPosition.x + oldWidth;

            // set the click position to the new left extent, if it doesn't violate minimum width.
            float newLeftExtent = Math.Min(eventData.position.x, rightExtent - minWidth); 

            // calculate new width to retain right extent
            float newWidth = rightExtent - newLeftExtent;
            // move the whole rectangle relative to whatever ui container it's in, changing width to keep right end pinned.
            rectXform.anchoredPosition = new Vector2(newLeftExtent, rectXform.anchoredPosition.y);
            rectXform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
            
            // Update now pointless dimensions struct.
            dims.left_x = newLeftExtent;
            dims.width = newWidth;

        }
    }

    internal void EndTarget_EndDrag(string whichEnd)
    {
        Debug.Log($"User has dropped the {whichEnd} end.");
        Debug.Log(dims);
    }
}
