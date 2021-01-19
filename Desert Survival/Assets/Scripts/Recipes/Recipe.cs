using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public Item result;
    [Range(1,10)]
    public int resultAmount = 1;
    public Item[] items;
}
