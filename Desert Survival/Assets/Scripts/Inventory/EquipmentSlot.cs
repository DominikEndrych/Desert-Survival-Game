using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : InventorySlot
{
    public override void OnItemChange()
    {
        if (isOccupied)
        {
            GameObject weapon = ((EquipmentItem)item).equipModel;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>().AddWeapon(weapon, this);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>().RemoveWeapon(this);
        }
    }
}
