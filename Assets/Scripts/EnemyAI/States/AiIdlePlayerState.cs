using UnityEngine;

public class AiIdlePlayerState : AiState
{
    public void Enter(AiAgent agent)
    {
    }

    public void Exit(AiAgent agent)
    {
    }

    public AiStateID getID()
    {
        return AiStateID.Idle;
    }

    public void Update(AiAgent agent)
    {
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if (playerDirection.magnitude > agent.config.maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;

        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if (dotProduct > 0.0f)
        {
            agent.stateMachine.changeState(AiStateID.ChasePlayer); 
        }
    }
}
