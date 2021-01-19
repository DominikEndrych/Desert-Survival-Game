using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WeaponRanges
{
    Short, Long
}

[CreateAssetMenu(fileName = "New equipment item", menuName = "Items/Single Item/Equipment")]
public class EquipmentItem : Item
{
    public GameObject equipModel;
    [SerializeField] WeaponRanges range;

    public void Equip()
    {
        return;
    }
}
