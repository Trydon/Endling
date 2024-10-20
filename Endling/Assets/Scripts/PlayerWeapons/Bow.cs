using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : PlayerWeapon
{
    private int Damage { get; set; } = 40;

    public override int CalculateDamageValue()
    {
        return Damage;
    }


}
