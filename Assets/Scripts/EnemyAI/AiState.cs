using UnityEngine;

/** 
* Enemy enum states
* @author: Yunseo Jeon
* @since: 2025-05-29
*/ 
public enum AiStateID
{
    ChasePlayer,
    Idle,
    RoamState
}

/**
* Interface for the enemy states. 
* @author: Yunseo Jeon
* @since: 2025-05-29
*/
public interface AiState
{
    AiStateID getID();

    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
