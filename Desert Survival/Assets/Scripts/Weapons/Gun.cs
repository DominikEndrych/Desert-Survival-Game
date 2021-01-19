using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    //[SerializeField] ammoType type;


    //private AudioSource audioSource;
    //private PlayerEquipment equipment;


    /*
    private void Awake()
    {
       playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        Unequip();
    }
    */

    [SerializeField] ParticleSystem muzzleFlash;

    protected override void Attack()
    {

        int ammo = equipment.GetAmmo(type);

        Debug.Log(ammo);

        if (ammo > 0)
        {
            if (audioSource != null) 
            { 
                audioSource.PlayOneShot(attackSound);           //play attack sound
                if(attackTailSound != null)
                    audioSource.PlayOneShot(attackTailSound);   //play tail sound after attack (eg. reverb)
            }

            animator.SetTrigger("attack");

            /*
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();  //get Enemy component

                if (enemy != null)
                {
                    enemy.GetDamage(damage);                        //hit enemy
                }
            }
            */

            RaycastAttack();
            muzzleFlash.Play();                                     //pla muzzle flash effect

            equipment.DecreaseAmmo(type);                           //decrease ammo
            ChangeAmmoText();
        } 
    }
}
