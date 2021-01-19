using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float interactionRadius = 5f;

    public virtual string ItemName { get; }

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + transform.name);
    }


    //is item close enough?
    public bool IsClose(float distance)
    {
        return distance <= interactionRadius ? true : false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

}
