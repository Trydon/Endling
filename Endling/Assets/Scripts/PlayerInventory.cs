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

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
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

