using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : PlayerWeapon
{
    private int Damage { get; set; } = 50;
    private float AttackRange { get; set; } = 1.5f;

    public override int CalculateDamageValue()
    {
        return Damage;
    }

    public override float? GetAttackRange()
    {
        return AttackRange;
    }
}
