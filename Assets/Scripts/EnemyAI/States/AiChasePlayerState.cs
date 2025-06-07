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
        float shortestDistance = 0;

        if (timer <= 0.0f)
        {
            float distance = (agent.playerTransform.position - agent.navMeshAgent.destination).sqrMagnitude;
            shortestDistance = Vector3.Distance(agent.playerTransform.position, agent.navMeshAgent.nextPosition);

            Debug.Log(shortestDistance);


            if (shortestDistance > agent.config.maxAgroRange)
            {
                agent.stateMachine.changeState(AiStateID.RoamState);
            }
            else
            {            
                if (distance > agent.config.maxDist * agent.config.maxDist)
                {
                    agent.navMeshAgent.destination = agent.playerTransform.position;
                }
            }

            timer = agent.config.maxTime;
        }
        

    }

}
