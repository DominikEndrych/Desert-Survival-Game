using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlot : InventorySlot
{
    [SerializeField] CraftingResultSlot resultSlot;

    public override void OnItemChange()
    {
        resultSlot.UpdateResult();
    }
}
