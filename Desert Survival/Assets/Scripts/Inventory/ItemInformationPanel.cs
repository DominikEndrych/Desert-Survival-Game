using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInformationPanel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI headerTextObject = null;
    [SerializeField] TextMeshProUGUI descriptionTextObject = null;
    [SerializeField] InventorySlot parentSlot = null;

    private string header;
    private string body;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Change content of header and body
    /// </summary>
    public void ChangeContent()
    {
        //change the item header and description if parent slot is occupied
        if (parentSlot.isOccupied)
        {
            header = parentSlot.item.itemName;
            body = parentSlot.item.BuildItemInformationString();
        }

        headerTextObject.text = header;
        descriptionTextObject.text = body;
    }


}
