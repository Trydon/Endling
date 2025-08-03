using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController
{
    private PlayerInventory _playerInventory;

    private WeaponTypes _currentWeapon;

    public PlayerInventoryController(PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
    }

    public WeaponTypes NextWeapon() 
    {
        _currentWeapon++;
        if ((int)_currentWeapon >= Enum.GetValues(typeof(WeaponTypes)).Length)
        {
            _currentWeapon = 0;
        }
        _playerInventory.EquipWeapon(_currentWeapon);
        return _currentWeapon;
    }

    public WeaponTypes PrevWeapon() 
    {
        _currentWeapon--;
        if ((int)_currentWeapon < 0)
        {
            _currentWeapon = (WeaponTypes)Enum.GetValues(typeof(WeaponTypes)).Length - 1;
        }
        _playerInventory.EquipWeapon(_currentWeapon);
        return _currentWeapon;
    }
}
