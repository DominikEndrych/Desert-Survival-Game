using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDropArea : MonoBehaviour, IDropHandler
{
    [SerializeField] Transform playerTransform = null;
    [SerializeField] InputNumberPanel numberInput = null;

    private InventorySlot originalSlot;
    private GameObject prefab;

    private bool readyToDrop;

    public void Update()
    {
        
        if(prefab != null && originalSlot.item is StackItem && readyToDrop && numberInput.gotInput)
        {
            int amount = numberInput.Number;

            if(originalSlot.count - amount == 0)
            {
                originalSlot.RemoveItem();
            }
            else
            {
                originalSlot.AddItemToStack(-amount);
            }

            DropStackItem(amount);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        originalSlot = eventData.pointerDrag.GetComponentInParent<InventorySlot>();
        prefab = originalSlot.item.prefab;

        if(prefab != null)
        {

            if(originalSlot.item is StackItem && originalSlot.count > 1)
            {
                numberInput.gameObject.SetActive(true);
                numberInput.SetMaxValue(originalSlot.count);
                readyToDrop = true;
            }
            else
            {
                DropItem();
            }
        }

    }

    public void DropStackItem(int amount)
    {
        
        for (int i = 0; i < amount; i++)
        {
            Vector3 direction = playerTransform.position + (playerTransform.forward * 1 + playerTransform.up * (1 * i));
            GameObject.Instantiate(prefab, direction, new Quaternion(Random.rotation.x, Random.rotation.y, Random.rotation.z, 0));
        }       
    }

    public void DropItem()
    {
        originalSlot.RemoveItem();

        Vector3 direction = playerTransform.position + (playerTransform.forward * 1);
        GameObject.Instantiate(prefab, direction, new Quaternion(Random.rotation.x, Random.rotation.y, Random.rotation.z, 0));
    }
}
