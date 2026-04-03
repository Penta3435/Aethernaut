using UnityEngine;
using UnityEngine.AI;

public class Agent1 : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform a;
    void Update()
    {
        agent.SetDestination(a.position);
    }
}
