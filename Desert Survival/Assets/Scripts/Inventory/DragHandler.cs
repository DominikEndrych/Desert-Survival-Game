using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public GameObject dragObject;                               //object representing drag

    [SerializeField] Image dragObjectImage = null;              //image to see when draging
    [SerializeField] TextMeshProUGUI dragObjectText = null;     //stack text
    [SerializeField] Canvas canvas = null;                      //canvas

    private InventorySlot parentSlot;
    private RectTransform rectTransform;                        //recttransform of original item
    private RectTransform dragObjectRectTransform;              //recttransofrm of drag object
    private Vector3 dragObectOrigin;                            //original position of drag object

    private CanvasGroup canvasGruop;                            //canvas group of drag object

    private void Awake()
    {
        parentSlot = GetComponentInParent<InventorySlot>();
        rectTransform = GetComponent<RectTransform>();
        dragObjectRectTransform = dragObject.GetComponent<RectTransform>();
        dragObectOrigin = dragObjectRectTransform.position;     //origin setting
        canvasGruop = dragObject.GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (parentSlot.isOccupied)
        {
            canvasGruop.alpha = 1f;                                                                 //making drag object visible
            //dragObjectImage.sprite = eventData.pointerDrag.GetComponentInChildren<Image>().sprite;  //set correct image to drag object
            dragObjectImage.sprite = parentSlot.item.icon;
            dragObjectText.text = eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().text; //number
            dragObjectRectTransform.position = rectTransform.position;                              //set drag object on correct position
            canvasGruop.blocksRaycasts = false;                                                     //making sure that drag object doesnt react with raycast (needed for drop event)
        }
        else
        {
            eventData.pointerDrag = null;
        }
        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (parentSlot.isOccupied)
        {
            dragObjectRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;       //moving drag object
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
            canvasGruop.alpha = 0f;                                 //making drag object invisible again
            dragObjectRectTransform.position = dragObectOrigin;     //reseting drag object position
    }
}
