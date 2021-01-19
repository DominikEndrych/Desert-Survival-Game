using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ammoType
{
    revolver,
    rifle,
    none
}

public class Ammo : Interactable
{
    public override string ItemName => "Ammo";
    public ammoType type;

    private PlayerEquipment equipment;

    private void Start()
    {
        equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
    }

    public override void Interact()
    {
        if(type == ammoType.revolver)
        {
            equipment.revolverAmmo++;
        }
        else if(type == ammoType.rifle)
        {
            equipment.rifleAmmo++;
        }

        Destroy(gameObject);
    }

}
