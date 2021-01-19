using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CraftingResultSlot : InventorySlot
{
    [SerializeField] InventorySlot[] craftingSlots = null;
    [SerializeField] Button craftButton = null;
    [SerializeField] TextMeshProUGUI errorText = null;
    [SerializeField] Recipe[] recipes = null;

    private List<Item> itemList = new List<Item>();

    public void UpdateResult()
    {
        RemoveItem();
        UpdateItemList();

        foreach(Recipe recipe in recipes)
        {
            //find recipes that feature same amount of items
            if(itemList.Count == recipe.items.Length)
            {
                if (CheckRecipeItems(recipe))
                {
                    AddItem(recipe.result);
                    if (Inventory.instance.CanPlaceItem(recipe.result))
                    {
                        errorText.gameObject.SetActive(false);
                        craftButton.gameObject.SetActive(true);
                        craftButton.onClick.RemoveAllListeners();
                        craftButton.onClick.AddListener(() => CraftItem(recipe.result));
                    }
                    else
                    {
                        errorText.text = "Inventory is full!";
                        errorText.gameObject.SetActive(true);
                        craftButton.gameObject.SetActive(false);
                    }
                    
                }
                else
                {
                    craftButton.gameObject.SetActive(false);
                    errorText.gameObject.SetActive(false);
                }
            }
            else
            {
                craftButton.gameObject.SetActive(false);
                errorText.gameObject.SetActive(false);
            }
        }


    }

    public void CraftItem(Item result)
    {

        Inventory.instance.Add(result);

        //removing items after new item is crafted
        foreach(InventorySlot slot in craftingSlots)
        {
            if (slot.isOccupied)
            {
                if (slot.count == 1)
                {
                    slot.RemoveItem();
                }
                else
                {
                    slot.AddItemToStack(-1);
                }
            } 
        }

        UpdateResult();
    }

    //updates list of items in crafting slots
    private void UpdateItemList()
    {
        itemList.Clear();
        foreach (InventorySlot slot in craftingSlots)
        {
            if (slot.item != null)
            {
                itemList.Add(slot.item);
            }
        }
    }

    //check if recipe is correct
    private bool CheckRecipeItems(Recipe recipe)
    {
        foreach(Item item in recipe.items)
        {
            if (!itemList.Contains(item))
            {
                return false;
            }
        }

        return true;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        return;
    }
}
