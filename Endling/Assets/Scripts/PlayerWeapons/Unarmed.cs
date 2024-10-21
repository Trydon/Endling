using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unarmed : PlayerWeapon
{
    private int Damage { get; set; } = 20;
    private float AttackRange { get; set; } = 1f; 

    public override int CalculateDamageValue()
    {
        return Damage;
    }
    public override float? GetAttackRange()
    {
        return AttackRange;
    }
}
