using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float minHealth = 40f;
    private float currentHealth;

    void Start()
    {
        currentHealth = Random.Range(minHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage, remaining: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Optional: chơi animation, hiệu ứng, âm thanh
        Destroy(gameObject); // Xoá enemy khỏi scene
    }
}
