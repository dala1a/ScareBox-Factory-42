using UnityEngine;

public enum AiStateID
{
    ChasePlayer, 
    Idle
}

public interface AiState
{
    AiStateID getID();

    void Enter(AiAgent agent);
    void Update(AiAgent agent); 
    void Exit(AiAgent agent);
}
