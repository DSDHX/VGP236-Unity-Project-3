using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolleAI : MonoBehaviour
{
    [Header("Route")]
    public Transform[] waypoints;
    public float patrolSpeed = 3f;
    public float waypointWaitTime = 1.5f;

    [Header("Chaser Setting")]
    public float chaseSpeed = 5f;
    public float chaseDistance = 5f;
    public float giveUpDistance = 10f;

    private NavMeshAgent agent;
    private Transform player;
    private int currentWaypointIndex = 0;
    private bool isChasing = false;
    private float waitTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameManager.Instance.RegisterAI(agent);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = patrolSpeed;
        SetNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isChasing && distanceToPlayer <= giveUpDistance)
        {
            isChasing = true;
            agent.speed = chaseSpeed;
        }
        if (isChasing && distanceToPlayer > giveUpDistance)
        {
            isChasing = false;
            agent.speed = patrolSpeed;
            SetNextWaypoint();
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTime += Time.deltaTime;
            if (waitTime >= waypointWaitTime)
            {
                SetNextWaypoint();
                waitTime = 0f;
            }
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void SetNextWaypoint()
    {
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        if (waypoints.Length == 0)
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
