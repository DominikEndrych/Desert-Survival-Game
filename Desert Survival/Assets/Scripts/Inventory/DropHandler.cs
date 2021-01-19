using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

enum ItemTypes
{
    All, Consumable, Equipment, Clothing_body, Clothing_pants, Clothing_shoes
}

public class DropHandler : MonoBehaviour, IDropHandler
{
    private Item newItem;
    private InventorySlot originalSlot;
    private InventorySlot slot;

    [SerializeField] ItemTypes accepting;       //only accept this type of items

    private void Awake()
    {
        slot = gameObject.GetComponent<InventorySlot>();    //slot
    }

    public void OnDrop(PointerEventData eventData)
    {

        originalSlot = eventData.pointerDrag.GetComponentInParent<InventorySlot>();     //original slot
        newItem = originalSlot.item;                                                    //item to be dropped

        //check if newItem can be placed on this slot
        switch (accepting)
        {
            case ItemTypes.All:     
                Drop(originalSlot, newItem, eventData);    
                break;

            case ItemTypes.Consumable:
                if(newItem is ConsumableItem) { Drop(originalSlot, newItem, eventData); }
                break;

            case ItemTypes.Equipment:
                if (newItem is EquipmentItem) { Drop(originalSlot, newItem, eventData); }
                break;

            case ItemTypes.Clothing_body:
                if (newItem is ClothingItem && newItem.name.ToLower().Contains("shirt")) { Drop(originalSlot, newItem, eventData); }
                break;

            case ItemTypes.Clothing_pants:
                if (newItem is ClothingItem && 
                    newItem.name.ToLower().Contains("pants") ||
                    newItem.name.ToLower().Contains("shorts") ||
                    newItem.name.ToLower().Contains("trousers")) 
                { Drop(originalSlot, newItem, eventData); }
                break;

            case ItemTypes.Clothing_shoes:
                if (newItem is ClothingItem && newItem.name.ToLower().Contains("shoes")) { Drop(originalSlot, newItem, eventData); }
                break;
        }
    }

    private void Drop(InventorySlot originalSlot, Item newItem, PointerEventData eventData)
    {
        
        if(originalSlot is CraftingResultSlot) { return; }

        if (eventData.pointerDrag != null && originalSlot != slot)
        {
            if (!slot.isFull)
            {
                if (newItem is StackItem)
                {
                    if (slot.isOccupied)
                    {
                        if(newItem.name == slot.item.name)                  //check, if there is a similiar item type on the slot
                        {
                            if(slot.count + originalSlot.count > slot.item.MaxStack)    //check if slot has enough room and if not, add only right amount
                            {
                                int amountToAdd = slot.item.MaxStack - slot.count;      //max amount to add
                                slot.AddItemToStack(amountToAdd);                       //adding amount
                                originalSlot.AddItemToStack(-amountToAdd);              //changing count in original slot
                            }
                            else
                            {
                                slot.AddItemToStack(originalSlot.count);
                                originalSlot.RemoveItem();
                            }
                            
                        }
                    }
                    else
                    {
                        slot.AddItem(newItem);

                        if(originalSlot.count > 1)
                        {
                            slot.AddItemToStack(originalSlot.count - 1);
                        }
                        originalSlot.RemoveItem();
                    }
                }
                //non-stack items
                else
                {
                    if (!slot.isOccupied)
                    {
                        slot.AddItem(newItem);
                        originalSlot.RemoveItem();
                    }
                }
            }     
        }

    }
}
