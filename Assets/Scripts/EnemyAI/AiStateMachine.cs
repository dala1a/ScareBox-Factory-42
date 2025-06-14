using UnityEngine;

/**
* Class used to setup the states. 
* @author: Yunseo Jeon
* @since: 2025-05-29
*/
public class AiStateMachine
{

    public AiState[] states;
    public AiAgent agent;
    public AiStateID currentState;

    public AiStateMachine(AiAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AiStateID)).Length; // gets the number of enum states. 
        states = new AiState[numStates];
    }

    /** 
    * Add a state to the state array.  
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    * @param AiState: The enemy state being registered. 
    */ 
    public void registerState(AiState state)
    {
        int index = (int)state.getID();
        states[index] = state;
    }

    /** 
    * Containtly updates the state of the enemy. 
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    */
    public void update()
    {
        getState(currentState)?.Update(agent);
    }

    /**
    * Gets the registered state based on the state id. 
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    * @return AiState: The registered state. 
    */
    public AiState getState(AiStateID stateID)
    {
        int index = (int)stateID;
        return states[index];
    }

    /** 
    * Changing state and running the exit and enter methods. 
    * @author: Yunseo Jeon. 
    * @since: 2025-05-29
    * @param: The new state being switched too.  
    */
    public void changeState(AiStateID newState)
    {
        getState(currentState)?.Exit(agent);
        currentState = newState;
        getState(currentState)?.Enter(agent);
    }

}
