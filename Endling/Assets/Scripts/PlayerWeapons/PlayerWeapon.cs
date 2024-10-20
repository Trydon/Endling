using UnityEngine;

public abstract class PlayerWeapon
{
    public abstract int CalculateDamageValue();
    public virtual float? GetAttackRange() 
    {
        return null;
    }
    
}

