using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public GameObject equippedObject;
    public float armor;
    public int revolverAmmo;
    public int rifleAmmo;

    [SerializeField] EquipmentSlot[] equipmentSlots;
    [SerializeField] GameObject[] slotObjects;
    [SerializeField] TextMeshProUGUI armorText;

    public void Start()
    {
        armor = 0f;
        revolverAmmo = 0;
        rifleAmmo = 0;
        armorText.text = "Armor: " + armor;
    }

    public void Update()
    {
        GetEquipmentInput();
    }

    public void ChangeArmor(float amount)
    {
        armor += amount;
        armorText.text = "Armor: " + armor;
    }

    public void AddWeapon(GameObject weapon, EquipmentSlot slot)
    {
        int index = Array.IndexOf(equipmentSlots, slot);
        Instantiate(weapon, slotObjects[index].gameObject.transform);
    }

    public void RemoveWeapon(EquipmentSlot slot)
    {
        int index = Array.IndexOf(equipmentSlots, slot);
        foreach(Transform obj in slotObjects[index].transform)
        {
            Destroy(obj.gameObject);
        }
        slotObjects[index].GetComponentInChildren<Weapon>().Unequip();
    }

    public void DecreaseAmmo(ammoType type)
    {
        if (type == ammoType.revolver) 
            revolverAmmo--;
        else 
            rifleAmmo--;
    }

    public int GetAmmo(ammoType type)
    {
        if (type == ammoType.revolver)
        {
            return revolverAmmo;
        }
        else
            return rifleAmmo;
    }

    private void GetEquipmentInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
            Debug.Log("Equiped 1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
            Debug.Log("Equiped 2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(2);
            Debug.Log("Equiped 3");
        }
    }

    private void EquipWeapon(int slot)
    {
        for(int i = 0; i < slotObjects.Length; i++)
        {
            Weapon weapon = slotObjects[i].GetComponentInChildren<Weapon>();

            if (i == slot && equipmentSlots[i].isOccupied)
            {
                if(weapon != null) 
                { 
                    weapon.Equip();
                }
            }
            else
            {
                if(weapon != null) 
                { 
                    weapon.Unequip();
                }
            }
        }
    }
}
