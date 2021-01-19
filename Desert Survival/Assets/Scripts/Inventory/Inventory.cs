using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;   //singleton

    public List<Item> items = new List<Item>(); //container for items

    public GameObject inventoryUI;
    public GameObject inventorySlotsParent;
    public InventorySlot[] slots;
    public bool isOpen;

    private void Awake()
    {
        if(instance != null) { return; }

        instance = this;
    }

    private void Start()
    {
        slots = inventoryUI.GetComponentsInChildren<InventorySlot>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            foreach(InventorySlot slot in slots)
            {
                slot.OnCursorExit();
            }

            inventoryUI.SetActive(!inventoryUI.activeSelf);
            this.isOpen = !this.isOpen;
        }
    }

    public void Add(Item item)
    {
        InventorySlot freeSlot = GetFreeSlot();         //first free slot in inventory

        if(item is StackItem)
        {
            foreach(InventorySlot slot in slots)        //find if this type of item is already in inventory
            {
                if(!slot.isFull && slot.isOccupied)
                {
                    if(slot.item.name == item.name)
                    {
                        slot.AddItemToStack(1);         //add item to stack with same item already in inventory
                        return;
                    }
                }
            }
            if(freeSlot != null)
                freeSlot.AddItem(item);                     //add item to a first free slot, because similiar item wasn't found
        }
        else
        {
            if(freeSlot != null)
                freeSlot.AddItem(item);                     //add regular item to a first free slot
        }
    }

    //check, if item can be placed in inventory
    public bool CanPlaceItem(Item item)
    {
        InventorySlot freeSlot = GetFreeSlot();


        if (item is StackItem)
        {
            foreach (InventorySlot slot in slots)
            {
                if (!slot.isFull && slot.isOccupied)
                {
                    if (slot.item.name == item.name)
                    {
                        return true;
                    }
                }
            }
            if (freeSlot != null) { return true; }
            else return false;
        }
        else
        {
            if (freeSlot != null) { return true; }
            else return false;
        }

    }

    public void Remove(Item item)
    {
        //items.Remove(item);
    }

    private InventorySlot GetFreeSlot()
    {
        foreach (InventorySlot slot in slots)
        {
            if (!slot.isFull && !slot.isOccupied)
            {
                return slot;
            }
        }
        return null;
    }
}
