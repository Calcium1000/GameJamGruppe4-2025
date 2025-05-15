using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject goal;
    private NavMeshAgent agent;
    [SerializeField] private bool hasStopped = false; // Flag to check if the enemy has stopped moving


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Disable automatic rotation of the NavMeshAgent
        agent.destination = goal.transform.position; // Set the destination to the goal's position
        agent.enabled = true; // Enable the NavMeshAgent component
    }

    // Update is called once per frame
    private void Update()
    {
        agent.destination = goal.transform.position; // Update the destination to the goal's position
        //when they stop
        if ((agent.velocity.magnitude == 0) & !hasStopped) hasStopped = true;
        //when they start moving again
        if ((agent.velocity.magnitude != 0) & hasStopped) hasStopped = false;
        //rotate while stopped
        if (hasStopped) agent.gameObject.transform.Rotate(0, 35, 0);
    }
}