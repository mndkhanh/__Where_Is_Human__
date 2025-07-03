using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float maxTravelDistance = 10f;
    public float damage;
    public float autoDestroyTime = 5f;

    private Vector3 startPosition;

    void Start()
    {
        damage = Random.Range(5f, 20f);
        startPosition = transform.position;
        Destroy(gameObject, autoDestroyTime);
    }

    void Update()
    {
        float traveled = Vector3.Distance(startPosition, transform.position);
        if (traveled >= maxTravelDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
