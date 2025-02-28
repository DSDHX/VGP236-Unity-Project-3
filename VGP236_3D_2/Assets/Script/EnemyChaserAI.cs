using UnityEngine;
using UnityEngine.AI;

public class EnemyChaserAI : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        GameManager.Instance.RegisterAI(agent);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
