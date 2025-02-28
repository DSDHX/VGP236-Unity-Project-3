using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderer : MonoBehaviour
{
    [Header("Move")]
    public float moveRadius = 50f;
    public float moveInterval = 3f;
    public float moveSpeed = 4f;

    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameManager.Instance.RegisterAI(agent);
        agent.speed = moveSpeed;
        StartCoroutine(WanderRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WanderRoutine()
    {
        while (true)
        {
            if (!isMoving)
            {
                targetPosition = GetRandomPoint(transform.position, moveRadius);
                agent.SetDestination(targetPosition);
                isMoving = true;
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                isMoving = false;
                yield return new WaitForSeconds(moveInterval);
            }

            yield return null;
        }
    }

    Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * radius;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return center;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
