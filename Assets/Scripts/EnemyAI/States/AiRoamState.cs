using UnityEngine;

public class AirRoamState : AiState
{

    int currentWaypoint = 0;
    private float footstepTimer = 0;
    private float footstepTimerEnd = 4; 



    /**
    * Runs when entering the state. setting the enemy attributes.  
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    * @param AiAgent: A reference to the enemy. 
    */
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.speed = 1.658919f;
        agent.navMeshAgent.stoppingDistance = 0;
    }

    /**
    * Runs when exiting the state. Resetting the enemy attributes.  
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    * @param AiAgent: A reference to the enemy. 
    */
    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = 4;
        agent.navMeshAgent.speed = 3f;
    }

    /** 
    * Gets ID for the Roam enum.  
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    * @return AiStateID: The Roam player enum state. 
    */
    public AiStateID getID()
    {
        return AiStateID.RoamState;
    }

    /**
    * The main functionality of the roam enemy state.
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    * @param AiAgent: A reference to the enemy. 
    */
    public void Update(AiAgent agent)
    {   
        // Get player position relative to the enemy. 
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        
        // If player is within the sight distance of the enemy. 
        if (!(playerDirection.magnitude > agent.config.maxSightDistance))
        {
            Vector3 agentDirection = agent.transform.forward; // Get the enemy's front direction. 

            playerDirection.Normalize(); 

            float dotProduct = Vector3.Dot(playerDirection, agentDirection); // Get the dot product to check if the player is in front of the enemy. 

            // If the player is in front of the enemy change the enemy state. 
            if (dotProduct > 0.0f)
            {
                agent.stateMachine.changeState(AiStateID.ChasePlayer);
                return;
            }
        }

        // If agent is within the player footstep hitbox
        if (agent.footstepsTrigger)
        {
            agent.navMeshAgent.SetDestination(agent.footstepPosition); // Set the enemy's destination to the center of that footstep hitbox

            // Check if the enemy has reached the end of it's path
            if (!agent.navMeshAgent.pathPending)
            {
                if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance)
                {
                    if (!agent.navMeshAgent.hasPath || agent.navMeshAgent.velocity.sqrMagnitude == 0f)
                    {   
                        // If the enemy has reached the end wait until the timer runs out before returning back to its original state. 
                        footstepTimer += Time.deltaTime;
                        if (footstepTimer >= footstepTimerEnd)
                        {
                            // reset
                            agent.footstepsTrigger = false; 
                            footstepTimer = 0;
                        }
                    }
                }
            }
        }
        else // If footsteps haven't been detected and enemy can't see player
        {
            if (playerDirection.magnitude > agent.config.maxSightDistance)
            {
                if (agent.navMeshAgent.remainingDistance <= 0.75f)
                {   
                    // Follow waypoints
                    currentWaypoint++;
                    if (currentWaypoint >= agent.waypoints.childCount)
                    {
                        currentWaypoint = 0;
                    }
                }
            }
            agent.navMeshAgent.SetDestination(agent.waypoints.GetChild(currentWaypoint).position); // Set position to the next waypoint
        }


    }


}
