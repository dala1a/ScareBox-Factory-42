using UnityEngine;

public class AiChasePlayerState : AiState
{

    float timer = 0.0f;


    public void Enter(AiAgent agent)
    {

    }

    public void Exit(AiAgent agent)
    {

    }

    public AiStateID getID()
    {
        return AiStateID.ChasePlayer;
    }

    public void Update(AiAgent agent)
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            float distance = (agent.playerTransform.position - agent.navMeshAgent.destination).sqrMagnitude;
            if (distance > agent.config.maxDist * agent.config.maxDist)
            {
                agent.navMeshAgent.destination = agent.playerTransform.position;
            }
            timer = agent.config.maxTime;
        }
        
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        // Vector3 agentDirection = agent.transform.forward;

        if (playerDirection.magnitude > agent.config.maxSightDistance)
        {
            agent.stateMachine.changeState(AiStateID.Idle) ;
        }
    }

}
