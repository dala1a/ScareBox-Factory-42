using UnityEngine;

/**
* Mechanics for enemy's chase state. 
* @author: Yunseo Jeon
* @since: 2025-05-29
*/ 
public class AiChasePlayerState : AiState
{

    float timer = 0.0f;

    
    public void Enter(AiAgent agent)
    {

    }

    public void Exit(AiAgent agent)
    {

    }

    /** 
    * Gets ID for the ChasePlayer enum.  
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    * @return AiStateID: The chase player enum state. 
    *
    */ 
    public AiStateID getID()
    {
        return AiStateID.ChasePlayer;
    }

    /**
    * The main functionality of the chase enemy state. (Movement, detection etc)
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    * @param AiAgent: A reference to the enemy. 
    */ 
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
