using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class LyricsToken : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    string lyrics = "Generals gathered in their masses!\nJust like witches at black masses.";
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    private Canvas canvas;
    private Canvas originalCanvas;
    private Transform originalParent;
    private Vector3 originalPosition;

    [SerializeField] private TextMeshProUGUI tmp_comp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvas = GetComponentInParent<Canvas>();

        SetWord("Witches");
    }

    public void SetWord(string word)
    {
        if (tmp_comp != null)
        {
            tmp_comp.text = word;
        }
    }

    public string GetWord()
    {
        if (tmp_comp != null)
        {
            return tmp_comp.text;
        }
        return "";

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        

        // Move to root canvas so it renders on top
        
        transform.SetParent(canvas.transform);
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Check if dropped on a valid target
        /*GameObject dropTarget = eventData.pointerEnter;

        if (dropTarget != null)
        {
            LyricDropZone dropZone = dropTarget.GetComponent<LyricDropZone>();
            if (dropZone != null)
            {
                dropZone.ReceiveToken(this);
                return;
            }
        }*/

        ReturnToOriginalPosition();

    }

    public void ReturnToOriginalPosition()
    {
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
