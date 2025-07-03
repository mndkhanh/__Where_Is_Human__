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
        EnemyAI ai = GetComponent<EnemyAI>();
        ai.animator.SetTrigger("isHitted");
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage, remaining: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        UIManager.Instance.AddKill();

        EnemySpawnAfterDie spawner = FindObjectOfType<EnemySpawnAfterDie>();
        EnemyAI ai = GetComponent<EnemyAI>();

        if (spawner != null && ai != null)
        {
            Vector3 pos = ai.spawnPointPosition;
            Quaternion rot = ai.spawnPointRotation;
            spawner.StartCoroutine(spawner.RespawnAfterDelay(pos, rot));
        }

        Destroy(gameObject);
    }


}
