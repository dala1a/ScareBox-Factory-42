using UnityEngine;

public class AiStateMachine
{

    public AiState[] states;
    public AiAgent agent;
    public AiStateID currentState;

    public AiStateMachine(AiAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AiStateID)).Length;
        states = new AiState[numStates];
    }

    public void registerState(AiState state)
    {
        int index = (int)state.getID();
        states[index] = state;
    }

    public void update()
    {
        getState(currentState)?.Update(agent);
    }

    public AiState getState(AiStateID stateID)
    {
        int index = (int)stateID;
        return states[index];
    }

    public void changeState(AiStateID newState)
    {
        getState(currentState)?.Exit(agent);
        currentState = newState; 
        getState(currentState)?.Enter(agent);
    }

}
