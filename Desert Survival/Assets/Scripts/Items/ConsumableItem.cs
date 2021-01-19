using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New consumable item", menuName = "Items/Single Item/Consumable")]
public class ConsumableItem : Item
{
    [Range(-10, 10)]
    public float water = 0;
    [Range(-10, 10)]
    public float food = 0;
    [Range(-10, 10)]
    public float health = 0;

    public override bool Use()
    {
        Debug.Log("Item consumed");

        PlayerStats stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        stats.ChangeValues(water, food, health);

        return true;
        
    }

    public override string BuildItemInformationString()
    {
        string result = "";

        List<string> values = new List<string>();

        values.Add(GetAddedAmount(water, "water", 2, 6));
        values.Add(GetAddedAmount(food, "food  ", 2, 6));
        values.Add(GetAddedAmount(health, "health", 2, 6));

        foreach(string value in values)
        {
            if(value != null)
            {
                result += value + "\n";
            }
        }

        return result;
    }

    

}
