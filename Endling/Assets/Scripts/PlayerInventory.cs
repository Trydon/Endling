using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    public PlayerWeapon currentWeapon { get; private set; }

    private void Awake()
    {
        // Ensure that only one instance of the inventory exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes if necessary
        }

        currentWeapon = new Unarmed();


    }

    public void EquipWeapon(WeaponTypes weaponType) 
    {
        currentWeapon = GetWeaponByType(weaponType);
    }

    private PlayerWeapon GetWeaponByType(WeaponTypes weaponType) 
    {
        switch (weaponType)
        {
            case WeaponTypes.Unarmed:
                return new Unarmed();
            case WeaponTypes.Sword:
                return new Sword();
            case WeaponTypes.Bow:
                return new Bow();
            default:
                return null;
        }
    }
}

