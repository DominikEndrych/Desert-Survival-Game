using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{

    public Item item;

    public override string ItemName { get => item.itemName; }

    //pickup logic
    public override void Interact()
    {
        Inventory.instance.Add(item);
        this.RemoveFromWorld();
    }

    //removes item from scene
    private void RemoveFromWorld()
    {
        Destroy(this.gameObject);
    }
}
