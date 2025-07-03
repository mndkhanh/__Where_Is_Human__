using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    public Animator animator { get; private set; }
    public float wanderRange = 4f;
    public float wanderInterval = 10f;
    public float detectionRange = 10f;
    public float attackRange = 4f;

    public GameObject fireBallPrefab;
    public Transform firePoint;

    public float fireForce = 10f;
    public float fireInterval = 4f;

    private GameObject player;
    public Vector3 spawnPointPosition { get; private set; }
    public Quaternion spawnPointRotation { get; private set; }


    private NavMeshAgent agent;
    private float wanderTimer;
    private float fireTimer;


    private enum State { WANDER, CHASE }
    private State currentState = State.WANDER;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        spawnPointPosition = transform.position;
        spawnPointRotation = transform.rotation;

        wanderTimer = wanderInterval;
        fireTimer = fireInterval;
    }

    void Update()
    {
        float distanceFromSpawnPointToPlayer = Vector3.Distance(spawnPointPosition, player.transform.position);
        float distanceFromEnemyToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceFromSpawnPointToPlayer <= detectionRange)
            currentState = State.CHASE;
        else
            currentState = State.WANDER;

        switch (currentState)
        {
            case State.WANDER:
                Wander();
                break;

            case State.CHASE:
                ChasePlayer(distanceFromEnemyToPlayer);
                break;
        }
    }

    void Wander()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderInterval)
        {
            Vector3 newPos = RandomNavSphere(spawnPointPosition, wanderRange, -1);
            agent.SetDestination(newPos);
            wanderTimer = 0;
        }
    }

    void ChasePlayer(float distance)
    {
        if (distance > attackRange)
        {
            agent.SetDestination(player.transform.position);
            fireTimer = fireInterval;
        }
        else
        {
            agent.ResetPath();

            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            lookDirection.y = 0f;
            if (lookDirection != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(lookDirection);


            fireTimer += Time.deltaTime;
            if (fireTimer >= fireInterval)
            {
                Fire();
                fireTimer = 0f;
            }
        }
    }


    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);
        return navHit.position;
    }

    void Fire()
    {
        animator.SetTrigger("isAttacking");
        GameObject fireBall = Instantiate(fireBallPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = fireBall.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
    }

}
