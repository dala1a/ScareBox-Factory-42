using UnityEngine;

public class AirRoamState : AiState
{

    int currentWaypoint = 0;

    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.speed = 1.658919f;
        agent.navMeshAgent.stoppingDistance = 0;
    }

    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = 4;
        agent.navMeshAgent.speed = 3f;
    }

    public AiStateID getID()
    {
        return AiStateID.RoamState;
    }

    public void Update(AiAgent agent)
    {   
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;

        if (playerDirection.magnitude > agent.config.maxSightDistance)
        {
            if (agent.navMeshAgent.remainingDistance <= 0.75f)
            {
                currentWaypoint++;
                if (currentWaypoint >= agent.waypoints.childCount)
                {
                    currentWaypoint = 0;
                }
            }
        }
        else
        { 
            Vector3 agentDirection = agent.transform.forward;

            playerDirection.Normalize();

            float dotProduct = Vector3.Dot(playerDirection, agentDirection);
            if (dotProduct > 0.0f)
            {
                agent.stateMachine.changeState(AiStateID.ChasePlayer);
                return;
            }

        }

        agent.navMeshAgent.SetDestination(agent.waypoints.GetChild(currentWaypoint).position);
    }

}
