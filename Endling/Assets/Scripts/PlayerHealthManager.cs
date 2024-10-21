using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, ITrapDamagable, IDamagable
{
    public int PlayerHealth { get; private set; }

    private void Start()
    {
        PlayerHealth = 120;
    }
    public void TakeTrapDamage(int? damageAmount, bool isFatal) 
    {
        if (isFatal) 
        {
            PlayerDeath();
        }
        else if (damageAmount.HasValue)
        {
            PlayerHealth -= damageAmount.Value;
            HealthCheck();
        }
    }

    public void TakeDamage(int damageAmount) 
    {
        PlayerHealth -= damageAmount;
        HealthCheck();
    }
    private void HealthCheck()
    {
        PlayerHealth = Mathf.Max(0, PlayerHealth);
        if (PlayerHealth <= 0)
        {
            PlayerDeath();
        }
    }
    private void HealDamage(int healAmount) 
    {
        PlayerHealth += healAmount;
    }
    private void PlayerDeath() 
    {
        // death logic here
    }
}
