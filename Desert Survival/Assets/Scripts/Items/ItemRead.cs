using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRead : Interactable
{
    [TextArea]
    public string text;

    public override string ItemName => "Dopis";


    public override void Interact()
    {
        GameObject.FindGameObjectWithTag("Reading Panel").GetComponent<ReadingPanel>().Open(text);
    }
}
