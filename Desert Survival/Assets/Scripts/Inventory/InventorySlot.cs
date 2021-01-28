using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public int count = 0;
    public bool isFull = false;
    public bool isOccupied = false;

    public int buttonClicks = 0;

    [SerializeField] Image icon = null;
    [SerializeField] TextMeshProUGUI textNumber = null;
    [SerializeField] ItemInformationPanel informationPanel = null;

    public void AddItem(Item newItem)
    {
        item = newItem;
        count++;

        icon.sprite = newItem.icon;
        icon.enabled = true;

        isOccupied = true;

        if(!(item is StackItem))
        {
            isFull = true;
        }

        informationPanel.ChangeContent();
        //this.ReloadStackNumber();
        ReloadStackNumber();
        OnItemChange();
    }

    public void AddItemToStack(int amount)
    {
        count += amount;

        if (count == item.MaxStack) isFull = true;

        this.ReloadStackNumber();
        OnItemChange();
    }

    public void ChangeStackNumber(int amount)
    {
        textNumber.text = amount.ToString();
    }

    public void RemoveItem()
    {
        item = null;
        count = 0;
        icon.sprite = null;
        icon.enabled = false;
        isOccupied = false;
        isFull = false;

        this.ReloadStackNumber();
        OnItemChange();
    }

    private void ReloadStackNumber()
    {
        if (count >= 2)
        {
            ChangeStackNumber(count);
        }
        else
            textNumber.text = "";

        //change if slot is full or not
        if (isOccupied)
        {
            if (count == item.MaxStack) { isFull = true; }
            else { isFull = false; }
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {

        if(eventData.clickCount >= 2 && isOccupied)
        {
            //was item used?
            if (item.Use())
            {
                RemoveItem();
            }
        }
    }

    public void OnCursorEnter()
    {
        Debug.Log("hello");
        if (isOccupied)
        {
            informationPanel.gameObject.SetActive(true);
        }
        
    }

    public void OnCursorExit()
    {
        informationPanel.gameObject.SetActive(false);
    }

    public virtual void OnItemChange()
    {
        return;
    }

}
