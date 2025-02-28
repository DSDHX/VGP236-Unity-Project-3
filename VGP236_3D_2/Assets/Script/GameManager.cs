using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isGameOver = false;
    private List<NavMeshAgent> allEnemies = new List<NavMeshAgent>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;

        ReloadScene();
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isGameOver = false;
    }

    public void RegisterAI(NavMeshAgent agent)
    {
        if (!allEnemies.Contains(agent))
        {
            allEnemies.Add(agent);
        }
    }

    public void PauseAllAI()
    {
        foreach (var agent in allEnemies)
        {
            agent.isStopped = true;
        }
    }

    public void ResumeAllAI()
    {
        foreach (var agent in allEnemies)
        {
            agent.isStopped = false;
        }
    }
}
