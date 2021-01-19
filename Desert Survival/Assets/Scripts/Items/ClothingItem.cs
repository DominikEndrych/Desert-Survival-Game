using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

[CreateAssetMenu(fileName = "New clothing item", menuName = "Items/Single Item/Clothing")]
public class ClothingItem : Item
{
    [Range(0,10)]
    public float heat = 0;
    [Range(0,10)]
    public float armor = 0;
    public float durability = 10;

    public override bool Use()
    {
        PlayerStats stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        PlayerEquipment equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();

        equipment.ChangeArmor(armor);

        return false;
    }

    public override string BuildItemInformationString()
    {
        string result = "";

        List<string> values = new List<string>();

        values.Add(GetAddedAmount(heat, "heat  ", 2, 6));
        values.Add(GetAddedAmount(armor, "armor  ", 2, 6));

        foreach (string value in values)
        {
            if (value != null)
            {
                result += value + "\n";
            }
        }

        return result;
    }

}
