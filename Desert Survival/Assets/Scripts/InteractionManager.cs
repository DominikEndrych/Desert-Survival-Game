using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactionNameText = null;

    private GameObject player;

    private GameObject selectedObject = null;
    private Interactable item;
    private Outline outline;

    private KeyCode interactionKey = KeyCode.E;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Inventory.instance.isOpen) { return; }      //don't interact with world if inventory is open

        selectedObject = null;

        this.ClearOutlines();
        interactionNameText.text = "";

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if(Physics.Raycast(ray, out rayHit))
        {
            selectedObject = rayHit.transform.gameObject;

            //if object is interactable
            if (selectedObject.CompareTag("Interactable"))
            {
                item = selectedObject.GetComponent<Interactable>();     //get interactable component

                //manage interaction with item
                if(item != null)
                {
                    float distanceFromObejct = Vector3.Distance(item.transform.position, player.transform.position);

                    if (item.IsClose(distanceFromObejct))
                    {
                        this.OutlineObject();                       //dispaly outline
                        interactionNameText.text = item.ItemName;   //display name of the object

                        //is action key pressed?
                        if (this.CheckForAction())
                        {
                            item.Interact();
                        }
                    }
                }
 
            }
        }
    }

    private bool CheckForAction()
    {
        return Input.GetKeyDown(interactionKey) ? true : false;
    }

    private void OutlineObject()
    {
        outline = selectedObject.GetComponent<Outline>();

        if (outline != null)
        {
            outline.OutlineWidth = 8;
        }
    }

    private void ClearOutlines()
    {
        if (outline != null)
        {
            outline.OutlineWidth = 0;
        }
    }
}
