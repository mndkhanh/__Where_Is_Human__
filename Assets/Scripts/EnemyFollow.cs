using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float detectionRange = 10f;
    public float attackRange = 2f;

    private Transform player;
    private NavMeshAgent agent;
    private float timer;

    private enum State { WANDER, CHASE }
    private State currentState = State.WANDER;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = wanderTimer;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Chuyển trạng thái
        if (distanceToPlayer <= detectionRange)
            currentState = State.CHASE;
        else
            currentState = State.WANDER;

        switch (currentState)
        {
            case State.WANDER:
                Wander();
                break;

            case State.CHASE:
                ChasePlayer(distanceToPlayer);
                break;
        }
    }

    void Wander()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    void ChasePlayer(float distance)
    {
        if (distance > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath(); // Đứng lại để tấn công
            Debug.Log("Tấn công người chơi!");
            // TODO: Gọi animation hoặc logic tấn công tại đây
        }
    }

    // Tìm vị trí random trong phạm vi NavMesh
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);
        return navHit.position;
    }
}
