using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingSlot : InventorySlot
{
    private float armor = 0;
    public override void OnItemChange()
    {
        if (isOccupied)
        {
            armor = ((ClothingItem)item).armor;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>().ChangeArmor(armor);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>().ChangeArmor(-armor);
            armor = 0;
        }
    }
}
