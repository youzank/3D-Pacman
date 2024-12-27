using UnityEngine;
using UnityEngine.AI;

public class GhostPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // B�lgede rastgele dola��lacak noktalar
    private NavMeshAgent agent;
    private int currentPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.destination = patrolPoints[currentPoint].position;
        currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}
