using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UIManager.Instance.UpdateHealth(currentHealth, maxHealth);
        UIManager.Instance.ResetKills();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = currentHealth <= 0 ? 0 : currentHealth;
        Debug.Log($"{gameObject.name} took {amount} damage, remaining: {currentHealth}");
        UIManager.Instance.UpdateHealth(currentHealth, maxHealth);
        if (currentHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        UIManager.Instance.OpenJumpScare();
    }
}
