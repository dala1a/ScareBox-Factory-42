using UnityEngine;

/**
* Mechanics for enemy's Idle state. 
* @author: Yunseo Jeon
* @since: 2025-05-29
*/ 
public class AiIdlePlayerState : AiState
{
    public void Enter(AiAgent agent)
    {
    }

    public void Exit(AiAgent agent)
    {
    }
    
    /** 
    * Gets ID for the Idle enum.  
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    * @return AiStateID: The Idle player enum state. 
    */
    public AiStateID getID()
    {
        return AiStateID.Idle;
    }

    /**
    * The main functionality of the Idle enemy state.
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    * @param AiAgent: A reference to the enemy. 
    */ 
    public void Update(AiAgent agent)
    {
        // Get the player's position relative to the enemy
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;

        // if the player is not within the enemy's range stop. 
        if (playerDirection.magnitude > agent.config.maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward; // get the enemy's front direction.

        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection); // Dot product to find if the player is in front. 
        if (dotProduct > 0.0f)
        {
            agent.stateMachine.changeState(AiStateID.ChasePlayer); // switch state. 
        }
    }
}
