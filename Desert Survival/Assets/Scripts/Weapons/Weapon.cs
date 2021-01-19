using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float range;
    public float attackRate;
    public ammoType type;

    public float NextTimeToFire { get; set; }
    public bool IsEquipped { get; set; }

    public AudioClip attackSound;
    public AudioClip attackTailSound;
    public AudioSource audioSource;

    public Animator animator;

    [HideInInspector]
    public Camera playerCamera;

    private TextMeshProUGUI ammoText;

    [HideInInspector]
    public PlayerEquipment equipment;


    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        ammoText = GameObject.FindGameObjectWithTag("Ammo Panel").GetComponentInChildren<TextMeshProUGUI>();
        equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        Unequip();
    }

    private void Update()
    {
        ChangeAmmoText();

        if(IsEquipped && !Inventory.instance.isOpen && !ReadingPanel.instance.isOpen && Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + 1f / attackRate;
            Attack();
        }

    }

    protected virtual void Attack()
    {
        animator.SetTrigger("attack");
        RaycastAttack();
    }

    public void Equip()
    {
        if (!IsEquipped)
        {
            animator.SetTrigger("equip");
            IsEquipped = true;
            ChangeAmmoText();
        }
        
    }

    public void Unequip()
    {
        if (IsEquipped)
        {
            animator.SetTrigger("unequip");
            IsEquipped = false;
            ammoText.text = "";
        }
        
    }

    protected void ChangeAmmoText()
    {
        if (IsEquipped)
        {
            if (type != ammoType.none && ammoText != null)
            {
                ammoText.text = "Ammo: " + equipment.GetAmmo(type);
            }
            else
            {
                ammoText.text = "";
            }
        }
        
    }

    protected void RaycastAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();  //get Enemy component

            if (enemy != null)
            {
                enemy.GetDamage(damage);                        //hit enemy
                Debug.Log("Enemy hit");
            }
        }
    }
}
