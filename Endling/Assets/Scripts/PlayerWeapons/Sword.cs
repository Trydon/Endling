using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : PlayerWeapon
{
    private int Damage { get; set; } = 50;

    public override int CalculateDamageValue()
    {
        return Damage;
    }

    public override float? GetAttackRange()
    {
        return 1.5f;
    }
}
