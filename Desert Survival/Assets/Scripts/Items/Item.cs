using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Single Item/Generic")]
public class Item : ScriptableObject
{
    public string itemName = "";
    public string description = "";

    public Sprite icon;         //icon in UI
    public GameObject prefab;   //object in scene

    public string ItemInformation { get; set; }

    public void Awake()
    {
        ItemInformation = BuildItemInformationString();
    }

    public virtual int MaxStack
    {
        get
        {
            return 1;
        }

        set
        {
            return;
        }
    }

    public virtual bool Use()
    {
        return false;
    }

    /// <summary>
    /// Makes description string based on type of item and its values
    /// </summary>
    /// <returns>Full description of item</returns>
    public virtual string BuildItemInformationString()
    {
        return description;

    }

    /// <summary>
    /// If item has effect on player statistics, this function will determine how many "+" or "-" should be next to chosen parameter. 
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="name"></param>
    /// <param name="firstPeek"></param>
    /// <param name="secondPeek"></param>
    /// <returns></returns>
    protected virtual string GetAddedAmount(float stat, string name, float firstPeek, float secondPeek)
    {
        if (stat <= firstPeek && stat > 0)
        {
            return name + "\t+";
        }
        else if (stat > firstPeek && stat <= secondPeek)
        {
            return name + "\t++";
        }
        else if (stat > secondPeek)
        {
            return name + "\t+++";
        }
        else if (stat >= -firstPeek && stat < 0)
        {
            return name + "\t-";
        }
        else if (stat < -firstPeek && stat > -secondPeek)
        {
            return name + "\t--";
        }
        else if (stat < -secondPeek)
        {
            return name + "\t---";
        }
        else
        {
            return null;
        }

    }

}
